using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json;

namespace NemesisSystem
{
	// Token: 0x02000065 RID: 101
	public static class SafeFile
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x0012192C File Offset: 0x0011FB2C
		public static void WriteLater(string path, Func<string> make, bool keepBak, Action<Exception> done)
		{
			object qLock = SafeFile._qLock;
			lock (qLock)
			{
				SafeFile._queue.Enqueue(delegate
				{
					Exception obj = null;
					try
					{
						SafeFile.Write(path, make(), keepBak);
					}
					catch (Exception obj)
					{
					}
					try
					{
						Action<Exception> done2 = done;
						if (done2 != null)
						{
							done2(obj);
						}
					}
					catch
					{
					}
				});
				if (SafeFile._writer == null)
				{
					ThreadStart start;
					if ((start = SafeFile.<>O.<0>__WriterLoop) == null)
					{
						start = (SafeFile.<>O.<0>__WriterLoop = new ThreadStart(SafeFile.WriterLoop));
					}
					SafeFile._writer = new Thread(start)
					{
						IsBackground = true,
						Name = "enRAGE writer",
						Priority = ThreadPriority.BelowNormal
					};
					SafeFile._writer.Start();
				}
				Monitor.PulseAll(SafeFile._qLock);
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x001219F8 File Offset: 0x0011FBF8
		public static void WriteLater(string path, string text, bool keepBak, Action<Exception> done)
		{
			object qLock = SafeFile._qLock;
			lock (qLock)
			{
				SafeFile._queue.Enqueue(delegate
				{
					Exception obj = null;
					try
					{
						SafeFile.Write(path, text, keepBak);
					}
					catch (Exception obj)
					{
					}
					try
					{
						Action<Exception> done2 = done;
						if (done2 != null)
						{
							done2(obj);
						}
					}
					catch
					{
					}
				});
				if (SafeFile._writer == null)
				{
					ThreadStart start;
					if ((start = SafeFile.<>O.<0>__WriterLoop) == null)
					{
						start = (SafeFile.<>O.<0>__WriterLoop = new ThreadStart(SafeFile.WriterLoop));
					}
					SafeFile._writer = new Thread(start)
					{
						IsBackground = true,
						Name = "enRAGE writer",
						Priority = ThreadPriority.BelowNormal
					};
					SafeFile._writer.Start();
				}
				Monitor.PulseAll(SafeFile._qLock);
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00121AC4 File Offset: 0x0011FCC4
		private static void WriterLoop()
		{
			for (;;)
			{
				object qLock = SafeFile._qLock;
				Action action;
				lock (qLock)
				{
					while (SafeFile._queue.Count == 0)
					{
						Monitor.Wait(SafeFile._qLock);
					}
					action = SafeFile._queue.Dequeue();
					SafeFile._busy++;
				}
				try
				{
					action();
				}
				catch
				{
				}
				qLock = SafeFile._qLock;
				lock (qLock)
				{
					SafeFile._busy--;
					Monitor.PulseAll(SafeFile._qLock);
				}
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00121B84 File Offset: 0x0011FD84
		public static bool BeginWaitBudget(int ms)
		{
			if (SafeFile._budgetOn)
			{
				return false;
			}
			SafeFile._budgetOn = true;
			SafeFile._budgetUntil = DateTime.UtcNow.AddMilliseconds((double)ms);
			return true;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00121BB5 File Offset: 0x0011FDB5
		public static void EndWaitBudget(bool started)
		{
			if (started)
			{
				SafeFile._budgetOn = false;
			}
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00121BC0 File Offset: 0x0011FDC0
		public static int BudgetMs(int ms)
		{
			if (SafeFile._budgetOn)
			{
				return Math.Max(0, Math.Min(ms, (int)(SafeFile._budgetUntil - DateTime.UtcNow).TotalMilliseconds));
			}
			return ms;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00121BFC File Offset: 0x0011FDFC
		public static bool Idle
		{
			get
			{
				object qLock = SafeFile._qLock;
				bool result;
				lock (qLock)
				{
					result = (SafeFile._queue.Count == 0 && SafeFile._busy == 0);
				}
				return result;
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00121C50 File Offset: 0x0011FE50
		public static bool WaitWrites(int ms = 3000)
		{
			DateTime dateTime = DateTime.UtcNow.AddMilliseconds((double)ms);
			if (SafeFile._budgetOn && SafeFile._budgetUntil < dateTime)
			{
				dateTime = SafeFile._budgetUntil;
			}
			object qLock = SafeFile._qLock;
			lock (qLock)
			{
				while (SafeFile._queue.Count > 0 || SafeFile._busy > 0)
				{
					TimeSpan timeSpan = dateTime - DateTime.UtcNow;
					if (timeSpan <= TimeSpan.Zero)
					{
						return false;
					}
					Monitor.Wait(SafeFile._qLock, timeSpan);
				}
			}
			return true;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00121CFC File Offset: 0x0011FEFC
		public static bool IsBlocked(string path)
		{
			HashSet<string> blocked = SafeFile._blocked;
			bool result;
			lock (blocked)
			{
				result = SafeFile._blocked.Contains(path);
			}
			return result;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00121D44 File Offset: 0x0011FF44
		public static void UnblockAll()
		{
			HashSet<string> blocked = SafeFile._blocked;
			lock (blocked)
			{
				SafeFile._blocked.Clear();
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00121D88 File Offset: 0x0011FF88
		private static void Block(string path)
		{
			HashSet<string> blocked = SafeFile._blocked;
			lock (blocked)
			{
				if (!SafeFile._blocked.Add(path))
				{
					return;
				}
			}
			Action<string> log = SafeFile.Log;
			if (log != null)
			{
				log("LOAD " + Path.GetFileName(path) + ": no copy could be read - running on an empty one, and it won't be saved over this session (the bad copies are kept as .unreadable-*)");
			}
			SafeFile.PendingNotice = Path.GetFileName(path) + " couldn't be read - the mod won't save over it this session. The copies are kept beside it (.unreadable-*).";
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00121E0C File Offset: 0x0012000C
		public static void Write(string path, string text, bool keepBak = false)
		{
			if (SafeFile.IsBlocked(path))
			{
				return;
			}
			string text2 = path + ".tmp";
			File.WriteAllText(text2, text);
			if (File.Exists(path))
			{
				File.Replace(text2, path, keepBak ? (path + ".bak") : null);
				return;
			}
			File.Move(text2, path);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00121E60 File Offset: 0x00120060
		public static T Read<T>(string path, Action<string> log, out bool found, out string from) where T : class
		{
			found = false;
			from = null;
			foreach (string text in new string[]
			{
				path,
				path + ".bak",
				path + ".tmp"
			})
			{
				if (File.Exists(text))
				{
					found = true;
					try
					{
						T t = JsonConvert.DeserializeObject<T>(File.ReadAllText(text));
						if (t == null)
						{
							throw new Exception("empty");
						}
						from = text;
						if (text != path && log != null)
						{
							log(string.Concat(new string[]
							{
								"LOAD ",
								Path.GetFileName(path),
								" unreadable or missing - read ",
								Path.GetFileName(text),
								" instead"
							}));
						}
						return t;
					}
					catch (Exception ex)
					{
						try
						{
							long len = new FileInfo(text).Length;
							if (!Directory.GetFiles(Path.GetDirectoryName(text) ?? ".", Path.GetFileName(text) + ".unreadable-*").Any((string f) => new FileInfo(f).Length == len))
							{
								File.Copy(text, text + string.Format(".unreadable-{0:yyyyMMdd-HHmmss}", DateTime.Now), true);
							}
						}
						catch
						{
						}
						if (log != null)
						{
							log(string.Concat(new string[]
							{
								"LOAD ",
								Path.GetFileName(text),
								" unreadable (",
								ex.Message,
								") - kept as .unreadable-*, trying the next copy"
							}));
						}
					}
				}
			}
			if (found)
			{
				SafeFile.Block(path);
			}
			return default(T);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00122020 File Offset: 0x00120220
		public static T Read<T>(string path, Action<string> log = null) where T : class
		{
			bool flag;
			string text;
			return SafeFile.Read<T>(path, log, out flag, out text);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00122038 File Offset: 0x00120238
		public static void RunLater(Action job)
		{
			object qLock = SafeFile._qLock;
			lock (qLock)
			{
				SafeFile._queue.Enqueue(delegate
				{
					try
					{
						job();
					}
					catch
					{
					}
				});
				if (SafeFile._writer == null)
				{
					ThreadStart start;
					if ((start = SafeFile.<>O.<0>__WriterLoop) == null)
					{
						start = (SafeFile.<>O.<0>__WriterLoop = new ThreadStart(SafeFile.WriterLoop));
					}
					SafeFile._writer = new Thread(start)
					{
						IsBackground = true,
						Name = "enRAGE writer",
						Priority = ThreadPriority.BelowNormal
					};
					SafeFile._writer.Start();
				}
				Monitor.PulseAll(SafeFile._qLock);
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x001220F0 File Offset: 0x001202F0
		public static void AppendLater(string path, string text)
		{
			object qLock = SafeFile._qLock;
			lock (qLock)
			{
				SafeFile._queue.Enqueue(delegate
				{
					try
					{
						SafeFile.Append(path, text);
					}
					catch
					{
					}
				});
				if (SafeFile._writer == null)
				{
					ThreadStart start;
					if ((start = SafeFile.<>O.<0>__WriterLoop) == null)
					{
						start = (SafeFile.<>O.<0>__WriterLoop = new ThreadStart(SafeFile.WriterLoop));
					}
					SafeFile._writer = new Thread(start)
					{
						IsBackground = true,
						Name = "enRAGE writer",
						Priority = ThreadPriority.BelowNormal
					};
					SafeFile._writer.Start();
				}
				Monitor.PulseAll(SafeFile._qLock);
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x001221AC File Offset: 0x001203AC
		public static void Append(string path, string text)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(path);
				if (fileInfo.Exists && fileInfo.Length > 5242880L)
				{
					SafeFile.Rotate(path);
				}
			}
			catch
			{
			}
			File.AppendAllText(path, text);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x001221F8 File Offset: 0x001203F8
		private static void Rotate(string path)
		{
			SafeFile.<>c__DisplayClass30_0 CS$<>8__locals1;
			CS$<>8__locals1.path = path;
			if (File.Exists(SafeFile.<Rotate>g__Old|30_0(3, ref CS$<>8__locals1)))
			{
				File.Delete(SafeFile.<Rotate>g__Old|30_0(3, ref CS$<>8__locals1));
			}
			for (int i = 2; i >= 1; i--)
			{
				if (File.Exists(SafeFile.<Rotate>g__Old|30_0(i, ref CS$<>8__locals1)))
				{
					File.Move(SafeFile.<Rotate>g__Old|30_0(i, ref CS$<>8__locals1), SafeFile.<Rotate>g__Old|30_0(i + 1, ref CS$<>8__locals1));
				}
			}
			File.Move(CS$<>8__locals1.path, SafeFile.<Rotate>g__Old|30_0(1, ref CS$<>8__locals1));
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00122270 File Offset: 0x00120470
		public static bool Daily(string path, string backupDir)
		{
			bool result;
			try
			{
				if (!File.Exists(path))
				{
					result = false;
				}
				else
				{
					Directory.CreateDirectory(backupDir);
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
					string extension = Path.GetExtension(path);
					string text = Path.Combine(backupDir, string.Format("{0}-{1:yyyyMMdd}{2}", fileNameWithoutExtension, DateTime.Now, extension));
					if (File.Exists(text))
					{
						result = false;
					}
					else
					{
						File.Copy(path, text);
						foreach (string path2 in (from f in Directory.GetFiles(backupDir, fileNameWithoutExtension + "-????????" + extension)
						orderby f descending
						select f).Skip(7))
						{
							File.Delete(path2);
						}
						result = true;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00122385 File Offset: 0x00120585
		[CompilerGenerated]
		internal static string <Rotate>g__Old|30_0(int i, ref SafeFile.<>c__DisplayClass30_0 A_1)
		{
			return string.Format("{0}.{1}", A_1.path, i);
		}

		// Token: 0x04000C29 RID: 3113
		public const long MaxLogBytes = 5242880L;

		// Token: 0x04000C2A RID: 3114
		public const int KeepLogs = 3;

		// Token: 0x04000C2B RID: 3115
		public const int KeepDaily = 7;

		// Token: 0x04000C2C RID: 3116
		private static readonly object _qLock = new object();

		// Token: 0x04000C2D RID: 3117
		private static readonly Queue<Action> _queue = new Queue<Action>();

		// Token: 0x04000C2E RID: 3118
		private static Thread _writer;

		// Token: 0x04000C2F RID: 3119
		private static int _busy;

		// Token: 0x04000C30 RID: 3120
		[ThreadStatic]
		private static DateTime _budgetUntil;

		// Token: 0x04000C31 RID: 3121
		[ThreadStatic]
		private static bool _budgetOn;

		// Token: 0x04000C32 RID: 3122
		private static readonly HashSet<string> _blocked = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000C33 RID: 3123
		public static Action<string> Log;

		// Token: 0x04000C34 RID: 3124
		public static string PendingNotice;

		// Token: 0x02000448 RID: 1096
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001FA7 RID: 8103
			public static ThreadStart <0>__WriterLoop;
		}
	}
}
