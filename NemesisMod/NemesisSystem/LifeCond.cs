using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace NemesisSystem
{
	// Token: 0x02000086 RID: 134
	internal static class LifeCond
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x00138C80 File Offset: 0x00136E80
		public static string Slug(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in (s ?? "").ToLowerInvariant())
			{
				if (char.IsLetterOrDigit(c))
				{
					stringBuilder.Append(c);
				}
				else if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '-')
				{
					stringBuilder.Append('-');
				}
			}
			return stringBuilder.ToString().Trim(new char[]
			{
				'-'
			});
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00138D08 File Offset: 0x00136F08
		public static bool Eval(string cond, NemesisRecord r)
		{
			if (string.IsNullOrWhiteSpace(cond))
			{
				return true;
			}
			string[] array = cond.Split(new char[]
			{
				'&'
			});
			Func<string, bool> <>9__0;
			for (int i = 0; i < array.Length; i++)
			{
				IEnumerable<string> source = array[i].Split(new char[]
				{
					'|'
				});
				Func<string, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((string t) => LifeCond.Term(t.Trim().ToLowerInvariant(), r)));
				}
				if (!source.Any(predicate))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00138D88 File Offset: 0x00136F88
		private static bool Term(string t, NemesisRecord r)
		{
			if (t.StartsWith("rank>="))
			{
				return r.Rank >= LifeCond.<Term>g__Num|2_0(t.Substring(6), 0);
			}
			if (t.StartsWith("rank<="))
			{
				return r.Rank <= LifeCond.<Term>g__Num|2_0(t.Substring(6), 99);
			}
			if (t.StartsWith("hate>="))
			{
				return r.Hate >= LifeCond.<Term>g__Num|2_0(t.Substring(6), 0);
			}
			if (t.StartsWith("fear>="))
			{
				return r.Fear >= LifeCond.<Term>g__Num|2_0(t.Substring(6), 0);
			}
			if (t.StartsWith("hate<"))
			{
				return r.Hate < LifeCond.<Term>g__Num|2_0(t.Substring(5), 101);
			}
			if (t.StartsWith("fear<"))
			{
				return r.Fear < LifeCond.<Term>g__Num|2_0(t.Substring(5), 101);
			}
			if (t.StartsWith("aggr<"))
			{
				return r.Aggression < LifeCond.<Term>g__Num|2_0(t.Substring(5), 100);
			}
			if (t.StartsWith("aggr>="))
			{
				return r.Aggression >= LifeCond.<Term>g__Num|2_0(t.Substring(6), 0);
			}
			if (t != null)
			{
				switch (t.Length)
				{
				case 3:
				{
					char c = t[0];
					if (c != 'd')
					{
						if (c == 'p')
						{
							if (t == "pet")
							{
								return r.Pet != null;
							}
						}
					}
					else if (t == "corto")
					{
						return r.Smarts == Smarts.Dim;
					}
					break;
				}
				case 4:
					switch (t[0])
					{
					case 'p':
						if (t == "poor")
						{
							return r.Wealth == Wealth.Poor;
						}
						break;
					case 'r':
						if (t == "rich")
						{
							return r.Wealth == Wealth.Rich;
						}
						break;
					case 't':
						if (t == "true")
						{
							return true;
						}
						break;
					}
					break;
				case 5:
				{
					char c = t[1];
					if (c <= 'n')
					{
						if (c != 'h')
						{
							if (c == 'n')
							{
								if (t == "knows")
								{
									return r.YourNotoriety >= 10 || r.Origin != GrudgeOrigin.GangMember || r.TimesAmbushed > 0;
								}
							}
						}
						else if (t == "astuto")
						{
							return r.Smarts == Smarts.Sharp;
						}
					}
					else if (c != 'o')
					{
						if (c == 'u')
						{
							if (t == "hosco")
							{
								return r.Disposition == Disposition.Surly;
							}
						}
					}
					else if (t == "nopet")
					{
						return r.Pet == null;
					}
					break;
				}
				case 6:
				{
					char c = t[0];
					if (c != 'd')
					{
						if (c == 's')
						{
							if (t == "single")
							{
								return r.TraitsRolled && r.Family == "single";
							}
						}
					}
					else if (t == "devout")
					{
						return r.Devotion == Devotion.Devout;
					}
					break;
				}
				case 7:
				{
					char c = t[0];
					if (c != 'g')
					{
						if (c != 'm')
						{
							if (c == 'n')
							{
								if (t == "notarmy")
								{
									Gang gang = Gangs.Of(r);
									return gang == null || gang.GangLifeEvents;
								}
							}
						}
						else if (t == "married")
						{
							return r.TraitsRolled && r.Family == "married";
						}
					}
					else if (t == "godless")
					{
						return r.Devotion == Devotion.Godless;
					}
					break;
				}
				case 8:
				{
					char c = t[1];
					if (c != 'a')
					{
						if (c != 'h')
						{
							if (c == 'r')
							{
								if (t == "amistoso")
								{
									return r.Disposition == Disposition.Friendly;
								}
							}
						}
						else if (t == "children")
						{
							return r.Children > 0;
						}
					}
					else if (t == "catholic")
					{
						return r.Faith == "Católico";
					}
					break;
				}
				case 10:
					if (t == "notgodless")
					{
						return r.Devotion != Devotion.Godless;
					}
					break;
				}
			}
			if (Quirks.All.Contains(t))
			{
				return Quirks.Has(r, t);
			}
			Personality personality;
			if (Enum.TryParse<Personality>(t, true, out personality))
			{
				Personality? personality2 = r.Personality;
				Personality personality3 = personality;
				return personality2.GetValueOrDefault() == personality3 & personality2 != null;
			}
			return false;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00139290 File Offset: 0x00137490
		[CompilerGenerated]
		internal static int <Term>g__Num|2_0(string s, int def)
		{
			if (s == "vengativo")
			{
				return Hate.VengefulAt;
			}
			if (s == "sanguinario")
			{
				return Hate.MurderousAt;
			}
			int result;
			if (!int.TryParse(s, out result))
			{
				return def;
			}
			return result;
		}
	}
}
