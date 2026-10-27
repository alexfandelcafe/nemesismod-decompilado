using System;

namespace NemesisSystem
{
	// Token: 0x0200005C RID: 92
	internal sealed class Setting
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x00111214 File Offset: 0x0010F414
		public static Setting Key<T>(string section, string key, Func<T> get, Action<T> set, Func<string> write)
		{
			return new Setting
			{
				Read = delegate(IniFile s)
				{
					set(s.GetValue<T>(section, key, get()));
				},
				Write = (() => key + "=" + write())
			};
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00111274 File Offset: 0x0010F474
		public static Setting ReadOnly<T>(string section, string key, Func<T> get, Action<T> set)
		{
			return new Setting
			{
				Read = delegate(IniFile s)
				{
					set(s.GetValue<T>(section, key, get()));
				}
			};
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x001112BC File Offset: 0x0010F4BC
		public static Setting StateKey<T>(string section, string key, Func<T> get, Action<T> set, Func<string> write)
		{
			return new Setting
			{
				Read = delegate(IniFile s)
				{
					set(s.GetValue<T>(section, key, get()));
				},
				Write = (() => key + "=" + write()),
				State = true,
				Section = section
			};
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0011132F File Offset: 0x0010F52F
		public static Setting StateLine(string section, Func<string> text)
		{
			return new Setting
			{
				Write = text,
				State = true,
				Section = section
			};
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0011134C File Offset: 0x0010F54C
		public static Setting Line(string text)
		{
			return new Setting
			{
				Write = (() => text)
			};
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0011137D File Offset: 0x0010F57D
		public static Setting Line(Func<string> text)
		{
			return new Setting
			{
				Write = text
			};
		}

		// Token: 0x04000BA7 RID: 2983
		public Action<IniFile> Read;

		// Token: 0x04000BA8 RID: 2984
		public Func<string> Write;

		// Token: 0x04000BA9 RID: 2985
		public bool State;

		// Token: 0x04000BAA RID: 2986
		public string Section;
	}
}
