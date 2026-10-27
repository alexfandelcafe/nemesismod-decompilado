using System;
using System.Text.RegularExpressions;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x02000069 RID: 105
	public static class Pron
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x001225A8 File Offset: 0x001207A8
		public static string Her(string s, bool female)
		{
			if (!female || string.IsNullOrEmpty(s))
			{
				return s;
			}
			return Pron.Words.Replace(s, delegate(Match m)
			{
				string value = m.Value;
				string a = value.ToLowerInvariant();
				string text = (a == "he") ? "she" : ((a == "him") ? "her" : ((a == "his") ? "her" : ((a == "himself") ? "herself" : ((a == "he's") ? "she's" : ((a == "he'll") ? "she'll" : "she'd")))));
				if (value.ToUpperInvariant() == value)
				{
					return text.ToUpperInvariant();
				}
				if (char.IsUpper(value[0]))
				{
					return char.ToUpperInvariant(text[0]).ToString() + text.Substring(1);
				}
				return text;
			});
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x001225E1 File Offset: 0x001207E1
		public static bool FemaleAt(int handle)
		{
			return handle != 0 && ENTITY.DOES_ENTITY_EXIST(handle) && ENTITY.IS_ENTITY_A_PED(handle) && !PED.IS_PED_MALE(handle);
		}

		// Token: 0x04000C37 RID: 3127
		private static readonly Regex Words = new Regex("\\b(he|him|his|himself|he's|he'll|he'd)\\b", RegexOptions.IgnoreCase);
	}
}
