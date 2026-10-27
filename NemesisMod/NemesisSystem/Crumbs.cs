using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace NemesisSystem
{
	// Token: 0x02000010 RID: 16
	internal static class Crumbs
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000BC60 File Offset: 0x00009E60
		public static void Init(string dir)
		{
			string path = Path.Combine(dir, "lastframe.txt");
			try
			{
				if (File.Exists(path))
				{
					string text = File.ReadAllText(path).Replace("\0", " ");
					List<string> list = (from i in Enumerable.Range(0, (text.Length + 128 - 1) / 128)
					select text.Substring(i * 128, Math.Min(128, text.Length - i * 128)).Trim()).ToList<string>();
					Crumbs.Previous = ((list.Count > 0) ? list[0] : text.Trim());
					List<ValueTuple<long, string>> list2 = new List<ValueTuple<long, string>>();
					foreach (string text2 in list.Skip(1))
					{
						int num = text2.IndexOf(' ');
						long item;
						if (num > 0 && long.TryParse(text2.Substring(0, num), out item))
						{
							list2.Add(new ValueTuple<long, string>(item, text2.Substring(num + 1)));
						}
					}
					if (list2.Count > 0)
					{
						Crumbs.PreviousCalls = string.Join("  |  ", from c in (from c in list2
						orderby c.Item1
						select c).Skip(Math.Max(0, list2.Count - 12))
						select c.Item2);
					}
				}
			}
			catch
			{
			}
			try
			{
				using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					fileStream.SetLength(8320L);
				}
				Crumbs._map = MemoryMappedFile.CreateFromFile(path, FileMode.Open, null, 8320L, MemoryMappedFileAccess.ReadWrite);
				Crumbs._view = Crumbs._map.CreateViewAccessor(0L, 8320L);
			}
			catch
			{
				Crumbs._view = null;
				Crumbs._map = null;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		private static void Put(int slot, string s)
		{
			if (Crumbs._view == null)
			{
				return;
			}
			try
			{
				for (int i = Encoding.ASCII.GetBytes((s.Length > 127) ? s.Substring(0, 127) : s, 0, Math.Min(s.Length, 127), Crumbs.Buf, 0); i < 127; i++)
				{
					Crumbs.Buf[i] = 32;
				}
				Crumbs.Buf[127] = 10;
				Crumbs._view.WriteArray<byte>((long)(slot * 128), Crumbs.Buf, 0, 128);
			}
			catch
			{
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public static void Mark(string what)
		{
			Crumbs.Put(0, string.Format("{0:HH:mm:ss.fff} {1}", DateTime.Now, what));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000BF6C File Offset: 0x0000A16C
		public static void Native(string what)
		{
			if (what == Crumbs._lastWhat && Crumbs._seq > 0L)
			{
				Crumbs._repeat++;
				Crumbs.Put(1 + (int)(Crumbs._seq % 64L), string.Format("{0} {1:HH:mm:ss.fff} {2} x{3}", new object[]
				{
					Crumbs._seq,
					DateTime.Now,
					what,
					Crumbs._repeat
				}));
				return;
			}
			Crumbs._lastWhat = what;
			Crumbs._repeat = 1;
			long num = Crumbs._seq += 1L;
			Crumbs.Put(1 + (int)(num % 64L), string.Format("{0} {1:HH:mm:ss.fff} {2}", num, DateTime.Now, what));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000C02C File Offset: 0x0000A22C
		public static void Close(string why)
		{
			if (Crumbs._view == null)
			{
				return;
			}
			Crumbs.Mark("CLEAN EXIT: " + why);
			try
			{
				MemoryMappedViewAccessor view = Crumbs._view;
				if (view != null)
				{
					view.Flush();
				}
				MemoryMappedViewAccessor view2 = Crumbs._view;
				if (view2 != null)
				{
					view2.Dispose();
				}
				MemoryMappedFile map = Crumbs._map;
				if (map != null)
				{
					map.Dispose();
				}
			}
			catch
			{
			}
			Crumbs._view = null;
			Crumbs._map = null;
		}

		// Token: 0x0400011C RID: 284
		private const int Line = 128;

		// Token: 0x0400011D RID: 285
		private const int RingSize = 64;

		// Token: 0x0400011E RID: 286
		private const int Size = 8320;

		// Token: 0x0400011F RID: 287
		private static MemoryMappedFile _map;

		// Token: 0x04000120 RID: 288
		private static MemoryMappedViewAccessor _view;

		// Token: 0x04000121 RID: 289
		private static readonly byte[] Buf = new byte[128];

		// Token: 0x04000122 RID: 290
		private static long _seq;

		// Token: 0x04000123 RID: 291
		public static string Previous;

		// Token: 0x04000124 RID: 292
		public static string PreviousCalls;

		// Token: 0x04000125 RID: 293
		private static string _lastWhat;

		// Token: 0x04000126 RID: 294
		private static int _repeat;
	}
}
