using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace NemesisSystem
{
	// Token: 0x02000022 RID: 34
	internal sealed class IniFile
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x000F89B4 File Offset: 0x000F6BB4
		public static IniFile Load(string path)
		{
			IniFile iniFile = new IniFile();
			if (!File.Exists(path))
			{
				return iniFile;
			}
			string str = "";
			string[] array = File.ReadAllLines(path);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length != 0 && !text.StartsWith(";") && !text.StartsWith("//"))
				{
					if (text.StartsWith("[") && text.Contains("]"))
					{
						str = text.Substring(1, text.IndexOf(']') - 1).Trim();
					}
					else
					{
						int num = text.IndexOf('=');
						if (num > 0)
						{
							string key = str + "/" + text.Substring(0, num).Trim();
							if (!iniFile._values.ContainsKey(key))
							{
								iniFile._values[key] = text.Substring(num + 1).Trim();
							}
						}
					}
				}
			}
			return iniFile;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000F8ABC File Offset: 0x000F6CBC
		public T GetValue<T>(string section, string name, T defaultValue)
		{
			string text;
			if (!this._values.TryGetValue(section + "/" + name, out text))
			{
				return defaultValue;
			}
			T result;
			try
			{
				if (typeof(T).IsEnum)
				{
					result = (T)((object)Enum.Parse(typeof(T), (typeof(T) == typeof(Keys)) ? KeyCombo.ForParse(text) : text, true));
				}
				else
				{
					result = (T)((object)Convert.ChangeType(text, typeof(T), CultureInfo.InvariantCulture));
				}
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x040007C1 RID: 1985
		private readonly Dictionary<string, string> _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}
