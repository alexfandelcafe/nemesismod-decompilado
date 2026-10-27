using System;
using System.IO;

namespace NemesisSystem
{
	// Token: 0x0200006D RID: 109
	internal static class StartupTrace
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x00122BB0 File Offset: 0x00120DB0
		private static string SafeDir(string file)
		{
			string result;
			try
			{
				result = (string.IsNullOrEmpty(file) ? null : Path.GetDirectoryName(file));
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00122BE8 File Offset: 0x00120DE8
		internal static void Retarget(string dir)
		{
			try
			{
				if (StartupTrace._path != null)
				{
					StartupTrace._path = Path.Combine(dir, Path.GetFileName(StartupTrace._path));
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00122C28 File Offset: 0x00120E28
		private static string DocsDir()
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Rockstar Games", "Red Dead Redemption 2", "enRAGE");
			Directory.CreateDirectory(text);
			return text;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00122C4C File Offset: 0x00120E4C
		internal static void Write(string step)
		{
			if (StartupTrace._failed)
			{
				return;
			}
			try
			{
				if (StartupTrace._path == null)
				{
					string text;
					try
					{
						text = StartupTrace.DocsDir();
					}
					catch
					{
						string text2 = Path.Combine(Directory.GetCurrentDirectory(), "scripts");
						if (!Directory.Exists(text2))
						{
							StartupTrace._failed = true;
							return;
						}
						text = Path.Combine(text2, "enRAGE");
						Directory.CreateDirectory(text);
					}
					StartupTrace._path = Path.Combine(text, "startup.log");
					try
					{
						File.WriteAllText(StartupTrace._path, string.Format("{0:yyyy-MM-dd HH:mm:ss.fff} startup trace begins{1}", DateTime.Now, Environment.NewLine));
					}
					catch
					{
						StartupTrace._failed = true;
						return;
					}
				}
				File.AppendAllText(StartupTrace._path, string.Format("{0:HH:mm:ss.fff} {1}{2}", DateTime.Now, step, Environment.NewLine));
			}
			catch
			{
				StartupTrace._failed = true;
			}
		}

		// Token: 0x04000C42 RID: 3138
		private static string _path;

		// Token: 0x04000C43 RID: 3139
		private static bool _failed;

		// Token: 0x04000C44 RID: 3140
		internal static int TicksLeft = 2;
	}
}
