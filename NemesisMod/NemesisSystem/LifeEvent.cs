using System;

namespace NemesisSystem
{
	// Token: 0x02000083 RID: 131
	internal sealed class LifeEvent
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x001389FC File Offset: 0x00136BFC
		public LifeEvent(string who, string title, string thought, string mood, int weight, int hours, string when = null, int odds = 10, params string[] effects) : this(null, who, title, thought, mood, weight, hours, when, odds, effects)
		{
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00138A20 File Offset: 0x00136C20
		public LifeEvent(string id, string who, string title, string thought, string mood, int weight, int hours, string when, int odds, string[] effects)
		{
			this.Who = who;
			this.Title = title;
			this.Thought = thought;
			this.Mood = mood;
			this.Weight = weight;
			this.Hours = hours;
			this.When = when;
			this.Odds = odds;
			this.Effects = (effects ?? new string[0]);
			this.Id = (string.IsNullOrEmpty(id) ? LifeCond.Slug(who + " " + title) : id);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00138AA8 File Offset: 0x00136CA8
		public bool Fits(NemesisRecord r)
		{
			string who = this.Who;
			if (!(who == "any"))
			{
				if (!(who == "gang"))
				{
					if (!(who == "loner"))
					{
						if (!(who == "raider"))
						{
							if (r.Gang != this.Who)
							{
								return false;
							}
						}
						else if (r.Gang != "lemoyneraiders")
						{
							return false;
						}
					}
					else if (r.Gang != null)
					{
						return false;
					}
				}
				else
				{
					Gang gang = Gangs.Of(r);
					if (gang == null || !gang.GangLifeEvents)
					{
						return false;
					}
				}
			}
			else
			{
				Gang gang2 = Gangs.Of(r);
				if (gang2 != null && gang2.OwnLifeOnly)
				{
					return false;
				}
			}
			return LifeCond.Eval(this.When, r);
		}

		// Token: 0x04000D3B RID: 3387
		public readonly string Who;

		// Token: 0x04000D3C RID: 3388
		public readonly string Title;

		// Token: 0x04000D3D RID: 3389
		public readonly string Thought;

		// Token: 0x04000D3E RID: 3390
		public readonly string Mood;

		// Token: 0x04000D3F RID: 3391
		public readonly int Weight;

		// Token: 0x04000D40 RID: 3392
		public readonly int Hours;

		// Token: 0x04000D41 RID: 3393
		public readonly int Odds;

		// Token: 0x04000D42 RID: 3394
		public readonly string[] Effects;

		// Token: 0x04000D43 RID: 3395
		public readonly string When;

		// Token: 0x04000D44 RID: 3396
		public readonly string Id;
	}
}
