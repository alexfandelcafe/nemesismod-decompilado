using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RDR2.Math;

namespace NemesisSystem
{
	// Token: 0x02000021 RID: 33
	internal sealed class Holdings
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x000F5958 File Offset: 0x000F3B58
		internal Holdings(IHoldingsWorld world)
		{
			this._w = world;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x000F5967 File Offset: 0x000F3B67
		private WorldState S
		{
			get
			{
				return this._w.State;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x000F5974 File Offset: 0x000F3B74
		internal Dictionary<string, string> SupplyCamps
		{
			get
			{
				Dictionary<string, string> result;
				if ((result = this.S.SupplyCamps) == null)
				{
					result = (this.S.SupplyCamps = new Dictionary<string, string>());
				}
				return result;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000F59A4 File Offset: 0x000F3BA4
		internal Dictionary<string, long> SupplyCaches
		{
			get
			{
				Dictionary<string, long> result;
				if ((result = this.S.SupplyCaches) == null)
				{
					result = (this.S.SupplyCaches = new Dictionary<string, long>());
				}
				return result;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x000F59D4 File Offset: 0x000F3BD4
		internal Dictionary<string, string> SupplyKnownAt
		{
			get
			{
				Dictionary<string, string> result;
				if ((result = this.S.SupplyKnownAt) == null)
				{
					result = (this.S.SupplyKnownAt = new Dictionary<string, string>());
				}
				return result;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000F5A04 File Offset: 0x000F3C04
		internal Dictionary<string, long> SupplyCampHit
		{
			get
			{
				Dictionary<string, long> result;
				if ((result = this.S.SupplyCampHitAt) == null)
				{
					result = (this.S.SupplyCampHitAt = new Dictionary<string, long>());
				}
				return result;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000F5A34 File Offset: 0x000F3C34
		internal Dictionary<string, string> SupplyCampHitSite
		{
			get
			{
				Dictionary<string, string> result;
				if ((result = this.S.SupplyCampHitSite) == null)
				{
					result = (this.S.SupplyCampHitSite = new Dictionary<string, string>());
				}
				return result;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000F5A64 File Offset: 0x000F3C64
		internal Dictionary<string, string> Treasuries
		{
			get
			{
				Dictionary<string, string> result;
				if ((result = this.S.Treasuries) == null)
				{
					result = (this.S.Treasuries = new Dictionary<string, string>());
				}
				return result;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000F5A94 File Offset: 0x000F3C94
		internal List<string> TreasuryKnownList
		{
			get
			{
				List<string> result;
				if ((result = this.S.TreasuryKnown) == null)
				{
					result = (this.S.TreasuryKnown = new List<string>());
				}
				return result;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000F5AC4 File Offset: 0x000F3CC4
		internal Dictionary<string, long> TreasuryRobbed
		{
			get
			{
				Dictionary<string, long> result;
				if ((result = this.S.TreasuryRobbedAt) == null)
				{
					result = (this.S.TreasuryRobbedAt = new Dictionary<string, long>());
				}
				return result;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x000F5AF4 File Offset: 0x000F3CF4
		internal Dictionary<string, string> TreasuryRobbedSite
		{
			get
			{
				Dictionary<string, string> result;
				if ((result = this.S.TreasuryRobbedSite) == null)
				{
					result = (this.S.TreasuryRobbedSite = new Dictionary<string, string>());
				}
				return result;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x000F5B24 File Offset: 0x000F3D24
		internal List<string> StashList
		{
			get
			{
				List<string> result;
				if ((result = this.S.TreasuryStash) == null)
				{
					result = (this.S.TreasuryStash = new List<string>());
				}
				return result;
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000F5B53 File Offset: 0x000F3D53
		internal static bool HoldsCamps(Gang g)
		{
			return g != null && !g.OrdersNotFeelings && !g.StoryOnly;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000F5B6C File Offset: 0x000F3D6C
		internal CampSite SupplyCampOf(Gang g)
		{
			string id;
			if (g == null || !Holdings.HoldsCamps(g) || !this.SupplyCamps.TryGetValue(g.Id, out id))
			{
				return null;
			}
			return this._w.Site(id);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000F5BA7 File Offset: 0x000F3DA7
		internal bool IsSupplyCamp(CampSite s)
		{
			return s != null && this.SupplyCamps.Values.Contains(s.Id);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000F5BC4 File Offset: 0x000F3DC4
		internal Gang SupplyGangOf(CampSite s)
		{
			if (s != null)
			{
				return Gangs.ById(this.SupplyCamps.FirstOrDefault((KeyValuePair<string, string> kv) => kv.Value == s.Id).Key);
			}
			return null;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000F5C0C File Offset: 0x000F3E0C
		internal bool SupplyRaidedRecently(Gang g)
		{
			long num;
			return g != null && this.SupplyCampHit.TryGetValue(g.Id, out num) && this._w.Now >= num && this._w.Now - num < 2880L;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000F5C56 File Offset: 0x000F3E56
		internal bool IsSupplyCache(Gang g)
		{
			return g != null && this.SupplyCaches.ContainsKey(g.Id);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000F5C70 File Offset: 0x000F3E70
		internal bool SupplyCacheKnown(Gang g)
		{
			if (g != null)
			{
				CampSite campSite = this.SupplyCampOf(g);
				string a;
				if (campSite != null && this.SupplyKnownAt.TryGetValue(g.Id, out a))
				{
					return a == campSite.Id;
				}
			}
			return false;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000F5CB0 File Offset: 0x000F3EB0
		internal bool CacheGuardsDue(Gang g)
		{
			long num;
			return g != null && this.SupplyCaches.TryGetValue(g.Id, out num) && (this._w.Now - num >= 360L || this._w.Now < num);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000F5CFC File Offset: 0x000F3EFC
		internal void KnowSupplies(Gang g, CampSite s)
		{
			if (g != null && s != null)
			{
				this.SupplyKnownAt[g.Id] = s.Id;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000F5D1C File Offset: 0x000F3F1C
		internal CampSite TreasuryOf(Gang g)
		{
			string id;
			if (g == null || !Holdings.HoldsCamps(g) || !this.Treasuries.TryGetValue(g.Id, out id))
			{
				return null;
			}
			return this._w.Site(id);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000F5D57 File Offset: 0x000F3F57
		internal bool IsTreasury(CampSite s)
		{
			return s != null && this.Treasuries.Values.Contains(s.Id);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000F5D74 File Offset: 0x000F3F74
		internal Gang TreasuryGangOf(CampSite s)
		{
			if (s != null)
			{
				return Gangs.ById(this.Treasuries.FirstOrDefault((KeyValuePair<string, string> kv) => kv.Value == s.Id).Key);
			}
			return null;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000F5DBC File Offset: 0x000F3FBC
		internal bool TreasuryKnown(Gang g)
		{
			return g != null && this.TreasuryKnownList.Contains(g.Id);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000F5DD4 File Offset: 0x000F3FD4
		internal bool TreasuryRobbedNow(Gang g)
		{
			return g != null && this.TreasuryRobbed.ContainsKey(g.Id);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000F5DEC File Offset: 0x000F3FEC
		internal bool IsStash(Gang g)
		{
			return g != null && this.StashList.Contains(g.Id);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000F5E04 File Offset: 0x000F4004
		internal bool IsStashSite(CampSite s)
		{
			return s != null && this.StashList.Any(delegate(string id)
			{
				CampSite campSite = this.TreasuryOf(Gangs.ById(id));
				return ((campSite != null) ? campSite.Id : null) == s.Id;
			});
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000F5E46 File Offset: 0x000F4046
		internal int MannedCamps(Gang g)
		{
			return this._w.SitesHeldBy(g.Id).Count((CampSite s) => this._w.Holder(s) != null && !this._w.Cleared(s));
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000F5E6A File Offset: 0x000F406A
		internal NemesisRecord StashKeeper(Gang g)
		{
			CampSite campSite = this.TreasuryOf(g);
			string ownSite = (campSite != null) ? campSite.Id : null;
			string otherSite;
			if (!this.IsSupplyCache(g))
			{
				otherSite = null;
			}
			else
			{
				CampSite campSite2 = this.SupplyCampOf(g);
				otherSite = ((campSite2 != null) ? campSite2.Id : null);
			}
			return this.Keeper(g, ownSite, otherSite, null);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000F5EA8 File Offset: 0x000F40A8
		internal NemesisRecord SupplyKeeper(Gang g)
		{
			CampSite campSite = this.SupplyCampOf(g);
			string ownSite = (campSite != null) ? campSite.Id : null;
			string otherSite;
			if (!this.IsStash(g))
			{
				otherSite = null;
			}
			else
			{
				CampSite campSite2 = this.TreasuryOf(g);
				otherSite = ((campSite2 != null) ? campSite2.Id : null);
			}
			return this.Keeper(g, ownSite, otherSite, this.IsStash(g) ? this.StashKeeperPlain(g) : null);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000F5F01 File Offset: 0x000F4101
		private NemesisRecord StashKeeperPlain(Gang g)
		{
			CampSite campSite = this.TreasuryOf(g);
			return this.Keeper(g, (campSite != null) ? campSite.Id : null, null, null);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000F5F20 File Offset: 0x000F4120
		private NemesisRecord Keeper(Gang g, string ownSite, string otherSite, NemesisRecord not)
		{
			NemesisRecord otherBoss = (otherSite != null) ? this._w.LiveBoss(otherSite) : null;
			return (from n in this._w.FreeMen(g)
			where n != not && n != otherBoss && !this.BossElsewhere(n, ownSite)
			orderby n.Rank descending, n.Id
			select n).FirstOrDefault<NemesisRecord>();
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000F5FCC File Offset: 0x000F41CC
		private bool BossElsewhere(NemesisRecord n, string ownSite)
		{
			string text = this._w.LiveCampOf(n);
			return text != null && text != ownSite;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000F5FF4 File Offset: 0x000F41F4
		internal CampSite PickStash(Gang g, CampSite except)
		{
			Holdings.<>c__DisplayClass53_0 CS$<>8__locals1 = new Holdings.<>c__DisplayClass53_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.except = except;
			CS$<>8__locals1.mannedAll = this._w.OfficerCamps.ToList<Vector3>();
			Holdings.<>c__DisplayClass53_0 CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<CampSite> source = from s in this._w.SitesHeldBy(g.Id)
			where CS$<>8__locals1.<>4__this._w.Holder(s) != null
			select s;
			Func<CampSite, Vector3> selector;
			if ((selector = Holdings.<>O.<0>__Pos) == null)
			{
				selector = (Holdings.<>O.<0>__Pos = new Func<CampSite, Vector3>(Holdings.Pos));
			}
			CS$<>8__locals2.own = source.Select(selector).ToList<Vector3>();
			List<CampSite> source2 = (from s in this._w.SitesHeldBy(g.Id)
			where (CS$<>8__locals1.except == null || s.Id != CS$<>8__locals1.except.Id) && !CS$<>8__locals1.<>4__this._w.Cleared(s) && !CS$<>8__locals1.<>4__this._w.TakeoverPending(s) && CS$<>8__locals1.<>4__this._w.Holder(s) == null && !CS$<>8__locals1.<>4__this._w.AnyoneCampsAt(s.Id) && CS$<>8__locals1.<>4__this._w.LiveBoss(s.Id) == null && !CS$<>8__locals1.<>4__this.IsSupplyCamp(s)
			select s).ToList<CampSite>();
			float[] array = new float[]
			{
				400f,
				250f
			};
			for (int i = 0; i < array.Length; i++)
			{
				float gap = array[i];
				IOrderedEnumerable<CampSite> source3 = from s in source2
				where CS$<>8__locals1.mannedAll.All((Vector3 m) => m.DistanceTo2D(Holdings.Pos(s)) >= gap)
				orderby s.Hideout descending
				select s;
				Func<CampSite, float> keySelector;
				if ((keySelector = CS$<>8__locals1.<>9__4) == null)
				{
					keySelector = (CS$<>8__locals1.<>9__4 = delegate(CampSite s)
					{
						if (CS$<>8__locals1.own.Count != 0)
						{
							return CS$<>8__locals1.own.Min((Vector3 o) => o.DistanceTo2D(Holdings.Pos(s)));
						}
						return 0f;
					});
				}
				CampSite campSite = source3.ThenBy(keySelector).FirstOrDefault<CampSite>();
				if (campSite != null)
				{
					return campSite;
				}
			}
			return null;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000F6162 File Offset: 0x000F4362
		private static Vector3 Pos(CampSite s)
		{
			return new Vector3(s.X, s.Y, s.Z);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000F617C File Offset: 0x000F437C
		internal void RecordRaid(Gang g, string siteId)
		{
			this.SupplyCampHit[g.Id] = this._w.Now;
			this.SupplyCampHitSite[g.Id] = siteId;
			this.KeeperGoneList.Remove("sup:" + g.Id);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000F61D4 File Offset: 0x000F43D4
		internal void ForgetRaid(Gang g)
		{
			this.SupplyCampHit.Remove(g.Id);
			this.SupplyCampHitSite.Remove(g.Id);
			this.KeeperGoneList.Remove("sup:" + g.Id);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000F6224 File Offset: 0x000F4424
		internal List<string> KeeperGoneList
		{
			get
			{
				List<string> result;
				if ((result = this.S.KeeperGone) == null)
				{
					result = (this.S.KeeperGone = new List<string>());
				}
				return result;
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000F6253 File Offset: 0x000F4453
		internal bool KeeperGone(Gang g, bool treasury)
		{
			return g != null && this.KeeperGoneList.Contains((treasury ? "tre:" : "sup:") + g.Id);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000F6280 File Offset: 0x000F4480
		internal void MarkKeeperGone(Gang g, bool treasury)
		{
			string item = (treasury ? "tre:" : "sup:") + ((g != null) ? g.Id : null);
			if (g != null && !this.KeeperGoneList.Contains(item))
			{
				this.KeeperGoneList.Add(item);
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000F62CC File Offset: 0x000F44CC
		internal void RecordRobbery(Gang g, string siteId)
		{
			this.TreasuryRobbed[g.Id] = this._w.Now;
			this.TreasuryRobbedSite[g.Id] = siteId;
			this.KnowTreasury(g);
			this.KeeperGoneList.Remove("tre:" + g.Id);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000F632B File Offset: 0x000F452B
		internal void ForgetRobbery(Gang g)
		{
			this.TreasuryRobbed.Remove(g.Id);
			this.TreasuryRobbedSite.Remove(g.Id);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000F6351 File Offset: 0x000F4551
		internal bool KnowTreasury(Gang g)
		{
			if (g == null || this.TreasuryKnownList.Contains(g.Id))
			{
				return false;
			}
			this.TreasuryKnownList.Add(g.Id);
			return true;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000F637D File Offset: 0x000F457D
		internal void ForgetTreasuryKnown(Gang g)
		{
			if (g != null)
			{
				this.TreasuryKnownList.Remove(g.Id);
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000F6394 File Offset: 0x000F4594
		internal void SetSupplyCamp(Gang g, CampSite s)
		{
			this.SupplyCamps[g.Id] = s.Id;
			this.SupplyCaches.Remove(g.Id);
			this.ForgetRaid(g);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000F63C8 File Offset: 0x000F45C8
		internal void SetTreasury(Gang g, CampSite s)
		{
			this.Treasuries[g.Id] = s.Id;
			this.StashList.Remove(g.Id);
			this.ForgetRobbery(g);
			this.TreasuryKnownList.Remove(g.Id);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000F6418 File Offset: 0x000F4618
		internal void ForgetSite(string siteId)
		{
			IEnumerable<KeyValuePair<string, string>> treasuries = this.Treasuries;
			Func<KeyValuePair<string, string>, bool> <>9__0;
			Func<KeyValuePair<string, string>, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((KeyValuePair<string, string> kv) => kv.Value == siteId));
			}
			foreach (string text in (from kv in treasuries.Where(predicate)
			select kv.Key).ToList<string>())
			{
				this.Treasuries.Remove(text);
				this.StashList.Remove(text);
				this.TreasuryKnownList.Remove(text);
				this.TreasuryRobbed.Remove(text);
				this.TreasuryRobbedSite.Remove(text);
			}
			IEnumerable<KeyValuePair<string, string>> supplyCamps = this.SupplyCamps;
			Func<KeyValuePair<string, string>, bool> <>9__2;
			Func<KeyValuePair<string, string>, bool> predicate2;
			if ((predicate2 = <>9__2) == null)
			{
				predicate2 = (<>9__2 = ((KeyValuePair<string, string> kv) => kv.Value == siteId));
			}
			foreach (string key in (from kv in supplyCamps.Where(predicate2)
			select kv.Key).ToList<string>())
			{
				this.SupplyCamps.Remove(key);
				this.SupplyCaches.Remove(key);
				this.SupplyKnownAt.Remove(key);
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000F65B4 File Offset: 0x000F47B4
		internal void Check(Action<string, string> fault)
		{
			IEnumerable<Gang> outfits = this._w.Outfits;
			Func<Gang, bool> predicate;
			if ((predicate = Holdings.<>O.<1>__HoldsCamps) == null)
			{
				predicate = (Holdings.<>O.<1>__HoldsCamps = new Func<Gang, bool>(Holdings.HoldsCamps));
			}
			foreach (Gang gang in outfits.Where(predicate))
			{
				string text;
				if (this.SupplyCamps.TryGetValue(gang.Id, out text))
				{
					CampSite campSite = this._w.Site(text);
					if (campSite == null)
					{
						fault("supplies:" + gang.Id, string.Concat(new string[]
						{
							"the ",
							gang.Name,
							" supplies are at a site that doesn't exist (",
							text,
							")"
						}));
					}
					else if (this._w.Holder(campSite) == null && !this.IsSupplyCache(gang) && !this._w.LiveSupplies(campSite.Id) && this._w.LiveBoss(campSite.Id) == null)
					{
						fault("supplies:" + gang.Id, string.Concat(new string[]
						{
							"the ",
							gang.Name,
							" supplies are at ",
							campSite.Label,
							", which nobody holds, and it isn't a cache"
						}));
					}
					else
					{
						if (campSite != null)
						{
							CampSite campSite2 = this.TreasuryOf(gang);
							if (((campSite2 != null) ? campSite2.Id : null) == campSite.Id)
							{
								fault("supplies-on-treasury:" + gang.Id, "the " + gang.Name + " supplies and money are both at " + campSite.Label);
								goto IL_1FE;
							}
						}
						if (campSite != null && this._w.InTown(campSite))
						{
							fault("supplies-in-town:" + gang.Id, string.Concat(new string[]
							{
								"the ",
								gang.Name,
								" supplies are at ",
								campSite.Label,
								", in town"
							}));
						}
					}
				}
				IL_1FE:
				string text2;
				if (this.Treasuries.TryGetValue(gang.Id, out text2))
				{
					CampSite campSite3 = this._w.Site(text2);
					if (campSite3 == null)
					{
						fault("treasury:" + gang.Id, string.Concat(new string[]
						{
							"the ",
							gang.Name,
							" money is at a site that doesn't exist (",
							text2,
							")"
						}));
					}
					else if (this._w.Holder(campSite3) == null && !this.IsStash(gang) && !this._w.LiveTreasury(campSite3.Id) && this._w.LiveBoss(campSite3.Id) == null)
					{
						fault("treasury:" + gang.Id, string.Concat(new string[]
						{
							"the ",
							gang.Name,
							" money is at ",
							campSite3.Label,
							", which nobody holds, and it isn't a stash"
						}));
					}
				}
				if (this.IsStash(gang) && this.IsSupplyCache(gang) && this.StashKeeper(gang) != null && this.StashKeeper(gang) == this.SupplyKeeper(gang))
				{
					fault("keepers:" + gang.Id, "the " + gang.Name + " stash and cache have the same keeper");
				}
			}
			foreach (string text3 in this.SupplyCaches.Keys)
			{
				if (this.SupplyCampOf(Gangs.ById(text3)) == null)
				{
					string arg = "cache:" + text3;
					string str = "a cache for the ";
					Gang gang2 = Gangs.ById(text3);
					fault(arg, str + (((gang2 != null) ? gang2.Name : null) ?? text3) + " with no supply site");
				}
			}
			foreach (string text4 in this.StashList)
			{
				if (this.TreasuryOf(Gangs.ById(text4)) == null)
				{
					string arg2 = "stash:" + text4;
					string str2 = "a stash for the ";
					Gang gang3 = Gangs.ById(text4);
					fault(arg2, str2 + (((gang3 != null) ? gang3.Name : null) ?? text4) + " with no treasury site");
				}
			}
			foreach (string text5 in this.SupplyKnownAt.Keys)
			{
				Gang gang4 = Gangs.ById(text5);
				if (gang4 != null && !Holdings.HoldsCamps(gang4))
				{
					fault("known:" + text5, "supplies known for the " + gang4.Name + ", who hold no camps");
				}
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.SupplyKnownAt)
			{
				Gang gang5 = Gangs.ById(keyValuePair.Key);
				if (gang5 != null)
				{
					CampSite campSite4 = this.SupplyCampOf(gang5);
					if (campSite4 != null && campSite4.Id != keyValuePair.Value)
					{
						fault("known-moved:" + keyValuePair.Key, string.Concat(new string[]
						{
							"the ",
							gang5.Name,
							" supplies moved to ",
							campSite4.Label,
							" but you still 'know' them at ",
							keyValuePair.Value
						}));
					}
				}
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000F6BDC File Offset: 0x000F4DDC
		internal List<HoldingsEvent> Pass(out bool changed)
		{
			List<HoldingsEvent> list = new List<HoldingsEvent>();
			bool flag = false;
			this.PlaceTreasuries(list, ref flag);
			this.PlaceSupplies(list, ref flag);
			foreach (string text in this.SupplyCampHit.Keys.ToList<string>())
			{
				if (!this.SupplyRaidedRecently(Gangs.ById(text)) || Gangs.ById(text) == null)
				{
					this.SupplyCampHit.Remove(text);
					this.SupplyCampHitSite.Remove(text);
					this.KeeperGoneList.Remove("sup:" + text);
					flag = true;
				}
			}
			foreach (string item in (from k in this.KeeperGoneList
			where k.StartsWith("tre:") && !this.TreasuryRobbed.ContainsKey(k.Substring(4))
			select k).ToList<string>())
			{
				this.KeeperGoneList.Remove(item);
				flag = true;
			}
			changed = flag;
			return list;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000F6CFC File Offset: 0x000F4EFC
		private static void Log(List<HoldingsEvent> ev, string text)
		{
			ev.Add(new HoldingsEvent
			{
				Act = HoldingsAct.Log,
				Text = text
			});
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000F6D17 File Offset: 0x000F4F17
		private static void Blip(List<HoldingsEvent> ev, string siteId)
		{
			if (siteId != null)
			{
				ev.Add(new HoldingsEvent
				{
					Act = HoldingsAct.RedoCampBlip,
					SiteId = siteId
				});
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000F6D35 File Offset: 0x000F4F35
		private static void StashBlips(List<HoldingsEvent> ev)
		{
			ev.Add(new HoldingsEvent
			{
				Act = HoldingsAct.StashBlips
			});
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000F6D49 File Offset: 0x000F4F49
		private static void CacheBlips(List<HoldingsEvent> ev)
		{
			ev.Add(new HoldingsEvent
			{
				Act = HoldingsAct.CacheBlips
			});
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000F6D5D File Offset: 0x000F4F5D
		private static void Notice(List<HoldingsEvent> ev, string text)
		{
			ev.Add(new HoldingsEvent
			{
				Act = HoldingsAct.Notice,
				Text = text
			});
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000F6D78 File Offset: 0x000F4F78
		private static void News(List<HoldingsEvent> ev, string text, bool big)
		{
			ev.Add(new HoldingsEvent
			{
				Act = HoldingsAct.WorldNews,
				Text = text,
				Big = big
			});
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000F6D9A File Offset: 0x000F4F9A
		private static string The(Gang g)
		{
			if (!g.Name.StartsWith("The "))
			{
				return "the " + g.Name;
			}
			return "the " + g.Name.Substring(4);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000F6DD8 File Offset: 0x000F4FD8
		private string From(Gang g, CampSite s)
		{
			string result;
			if ((result = s.Name) == null)
			{
				result = ((this._w.Owner(s) == g.Id) ? "their" : "the") + " camp in " + s.Region;
			}
			return result;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000F6E24 File Offset: 0x000F5024
		private static void Reveal(List<HoldingsEvent> ev, NemesisRecord who)
		{
			ev.Add(new HoldingsEvent
			{
				Act = HoldingsAct.RevealCamp,
				Who = who
			});
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000F6E3F File Offset: 0x000F503F
		private static void Taken(List<HoldingsEvent> ev, Gang from, Gang to, int amount, string place, bool stash)
		{
			ev.Add(new HoldingsEvent
			{
				Act = HoldingsAct.PurseTaken,
				From = from,
				To = to,
				Amount = amount,
				Place = place,
				Stash = stash
			});
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000F6E78 File Offset: 0x000F5078
		private bool Forget(Gang g)
		{
			return this.Treasuries.Remove(g.Id) | this.TreasuryKnownList.Remove(g.Id) | this.TreasuryRobbed.Remove(g.Id) | this.TreasuryRobbedSite.Remove(g.Id) | this.StashList.Remove(g.Id) | this.SupplyCamps.Remove(g.Id) | this.SupplyCaches.Remove(g.Id) | this.SupplyKnownAt.Remove(g.Id) | this.SupplyCampHit.Remove(g.Id) | this.SupplyCampHitSite.Remove(g.Id) | this.KeeperGoneList.Remove("sup:" + g.Id) | this.KeeperGoneList.Remove("tre:" + g.Id);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000F6F70 File Offset: 0x000F5170
		private void PlaceTreasuries(List<HoldingsEvent> ev, ref bool changed)
		{
			long now = this._w.Now;
			IEnumerable<Gang> outfits = this._w.Outfits;
			Func<Gang, bool> predicate;
			if ((predicate = Holdings.<>O.<1>__HoldsCamps) == null)
			{
				predicate = (Holdings.<>O.<1>__HoldsCamps = new Func<Gang, bool>(Holdings.HoldsCamps));
			}
			foreach (Gang gang in outfits.Where(predicate))
			{
				if (this._w.Destroyed(gang))
				{
					if (this.Forget(gang))
					{
						changed = true;
					}
				}
				else
				{
					CampSite site = this.TreasuryOf(gang);
					if ((site == null || !this._w.LiveTreasury(site.Id)) && !this.PlaceStash(gang, site, now, ev, ref changed))
					{
						long num;
						bool flag = this.TreasuryRobbed.TryGetValue(gang.Id, out num) && (now - num >= 1440L || now < num);
						string a;
						if (flag && site != null && this.TreasuryRobbedSite.TryGetValue(gang.Id, out a) && a != site.Id)
						{
							this.TreasuryRobbed.Remove(gang.Id);
							this.TreasuryRobbedSite.Remove(gang.Id);
							changed = true;
							Holdings.Log(ev, "TREASURY " + gang.Name + ": refilled at " + site.Label);
							flag = false;
						}
						string text = (site != null) ? this._w.Owner(site) : null;
						bool flag2 = site != null && text != gang.Id;
						bool flag3 = site != null && !flag2 && this._w.Holder(site) != null && !this._w.Cleared(site);
						string a2;
						bool flag4 = this.KeeperGone(gang, true) && this.TreasuryRobbedSite.TryGetValue(gang.Id, out a2) && site != null && a2 == site.Id;
						if (!flag3 || flag || flag4)
						{
							string a3;
							bool flag5 = this.TreasuryRobbedSite.TryGetValue(gang.Id, out a3) && site != null && a3 == site.Id && this.TreasuryRobbed.ContainsKey(gang.Id);
							if (site != null && !flag2 && !flag && !flag5 && !flag4 && this.TreasuryKnown(gang) && !this._w.Cleared(site))
							{
								if (!this.StashList.Contains(gang.Id))
								{
									this.StashList.Add(gang.Id);
								}
								changed = true;
								Holdings.Log(ev, string.Concat(new string[]
								{
									"TREASURY ",
									gang.Name,
									": nobody holds ",
									site.Label,
									" - el dinero que conoces se queda allí, ahora como un alijo"
								}));
								Holdings.Blip(ev, site.Id);
								Holdings.StashBlips(ev);
							}
							else
							{
								if (flag2)
								{
									Gang gang2 = Gangs.ById(text);
									int num2 = this.TreasuryRobbedNow(gang) ? 0 : ((int)((double)this._w.Purse(gang) * 0.4));
									if (gang2 != null && this._w.HasPurse(gang2) && num2 > 0)
									{
										Holdings.Taken(ev, gang, gang2, num2, site.Label, false);
									}
									Holdings.Log(ev, string.Concat(new string[]
									{
										"TREASURY ",
										gang.Name,
										": ",
										site.Label,
										" lost to the ",
										((gang2 != null) ? gang2.Name : null) ?? text,
										(num2 > 0) ? string.Format(" - they took ${0}", num2) : ""
									}));
								}
								CampSite campSite = (from s in this._w.SitesHeldBy(gang.Id)
								where (site == null || s.Id != site.Id) && !this._w.TakeoverPending(s) && this._w.Holder(s) != null
								orderby (this.IsSupplyCamp(s) > false) ? 1 : 0, s.Hideout descending, this._w.Holder(s).Rank descending
								select s).ThenBy((CampSite _) => this._w.Roll(1048576)).FirstOrDefault<CampSite>();
								if (campSite == null)
								{
									if (site != null && (flag2 || this._w.Holder(site) == null))
									{
										Holdings.News(ev, string.Concat(new string[]
										{
											"Word is ",
											Holdings.The(gang),
											" han trasladado su dinero fuera de ",
											this.From(gang, site),
											". Nadie sabe adónde lo llevaron."
										}), this.TreasuryKnown(gang) || this.TreasuryRobbed.ContainsKey(gang.Id) || this._w.Cleared(site));
										this.Treasuries.Remove(gang.Id);
										this.TreasuryKnownList.Remove(gang.Id);
										this.TreasuryRobbed.Remove(gang.Id);
										this.TreasuryRobbedSite.Remove(gang.Id);
										this.StashList.Remove(gang.Id);
										this.KeeperGoneList.Remove("tre:" + gang.Id);
										changed = true;
										Holdings.Log(ev, "TREASURY " + gang.Name + ": no manned camp to keep their money in - none for now");
										Holdings.Blip(ev, site.Id);
									}
								}
								else
								{
									string text2 = (site == null) ? "where they keep it" : (flag2 ? "moved - the old camp was lost" : (flag ? "moved after you robbed it" : "moved - nobody held the old camp"));
									if (site != null)
									{
										Holdings.News(ev, string.Concat(new string[]
										{
											"Word is ",
											Holdings.The(gang),
											" han trasladado su dinero fuera de ",
											this.From(gang, site),
											"."
										}), this.TreasuryKnown(gang) || flag || flag5 || this._w.Cleared(site));
									}
									this.Treasuries[gang.Id] = campSite.Id;
									this.TreasuryKnownList.Remove(gang.Id);
									this.KeeperGoneList.Remove("tre:" + gang.Id);
									changed = true;
									if (flag || !this.TreasuryRobbed.ContainsKey(gang.Id))
									{
										this.TreasuryRobbed.Remove(gang.Id);
										this.TreasuryRobbedSite.Remove(gang.Id);
									}
									else
									{
										text2 += " - still empty from your robbery";
									}
									string[] array = new string[8];
									array[0] = "TREASURY ";
									array[1] = gang.Name;
									array[2] = ": ";
									array[3] = campSite.Label;
									array[4] = " (";
									array[5] = text2;
									array[6] = ")";
									int num3 = 7;
									NemesisRecord nemesisRecord = this._w.Holder(campSite);
									array[num3] = ((nemesisRecord != null) ? (", held by " + nemesisRecord.Name) : "");
									Holdings.Log(ev, string.Concat(array));
									CampSite site2 = site;
									Holdings.Blip(ev, (site2 != null) ? site2.Id : null);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000F7750 File Offset: 0x000F5950
		private bool PlaceStash(Gang g, CampSite site, long now, List<HoldingsEvent> ev, ref bool changed)
		{
			int num = this.MannedCamps(g);
			if (site != null && this.IsStash(g))
			{
				NemesisRecord nemesisRecord = this._w.Holder(site);
				if (nemesisRecord != null)
				{
					this.StashList.Remove(g.Id);
					changed = true;
					if (this.TreasuryKnown(g) && !nemesisRecord.CampKnown)
					{
						Holdings.Reveal(ev, nemesisRecord);
					}
					Holdings.Log(ev, string.Concat(new string[]
					{
						"TREASURY ",
						g.Name,
						": their stash at ",
						site.Label,
						" is ",
						nemesisRecord.Name,
						"'s camp now - the money stays"
					}));
					Holdings.StashBlips(ev);
					Holdings.Blip(ev, site.Id);
					return true;
				}
				bool flag = this.TreasuryKnown(g);
				if (num >= 5 && !flag)
				{
					Holdings.News(ev, string.Concat(new string[]
					{
						"Word is ",
						Holdings.The(g),
						" han trasladado su dinero fuera de ",
						this.From(g, site),
						"."
					}), false);
					this.Treasuries.Remove(g.Id);
					this.StashList.Remove(g.Id);
					changed = true;
					Holdings.Log(ev, string.Format("TREASURY {0}{1}{2}", g.Name, num, site.Label));
					Holdings.StashBlips(ev);
					return true;
				}
				long num2;
				bool flag2 = this.TreasuryRobbed.TryGetValue(g.Id, out num2);
				bool flag3 = flag2 && (now - num2 >= 1440L || now < num2);
				string a;
				if (flag3 && this.TreasuryRobbedSite.TryGetValue(g.Id, out a) && a != site.Id)
				{
					this.TreasuryRobbed.Remove(g.Id);
					this.TreasuryRobbedSite.Remove(g.Id);
					changed = true;
					Holdings.Log(ev, string.Concat(new string[]
					{
						"TREASURY ",
						g.Name,
						": the stash at ",
						site.Label,
						" refilled"
					}));
					Holdings.StashBlips(ev);
					return true;
				}
				string text = this._w.Owner(site);
				bool flag4 = text != g.Id || this._w.Cleared(site);
				string a2;
				bool flag5 = flag2 && this.TreasuryRobbedSite.TryGetValue(g.Id, out a2) && a2 == site.Id && (this.KeeperGone(g, true) || this.StashKeeper(g) == null);
				if (!flag4 && !flag3 && !flag5)
				{
					return true;
				}
				if (flag4 && text != null && text != g.Id)
				{
					Gang gang = Gangs.ById(text);
					int num3 = flag2 ? 0 : ((int)((double)this._w.Purse(g) * 0.4));
					if (gang != null && this._w.HasPurse(gang) && num3 > 0)
					{
						Holdings.Taken(ev, g, gang, num3, site.Label, true);
					}
				}
				CampSite campSite = this.PickStash(g, site);
				Holdings.News(ev, string.Concat(new string[]
				{
					"Word is ",
					Holdings.The(g),
					" han trasladado su dinero fuera de ",
					this.From(g, site),
					"."
				}), flag || flag2 || this._w.Cleared(site));
				if (flag3)
				{
					this.TreasuryRobbed.Remove(g.Id);
					this.TreasuryRobbedSite.Remove(g.Id);
				}
				this.TreasuryKnownList.Remove(g.Id);
				this.KeeperGoneList.Remove("tre:" + g.Id);
				changed = true;
				if (campSite == null)
				{
					this.Treasuries.Remove(g.Id);
					this.StashList.Remove(g.Id);
					Holdings.Log(ev, string.Concat(new string[]
					{
						"TREASURY ",
						g.Name,
						": the stash at ",
						site.Label,
						" ",
						flag4 ? "is gone" : "was robbed",
						" - nowhere out of the way for another; a camp will do"
					}));
					Holdings.StashBlips(ev);
					return true;
				}
				this.Treasuries[g.Id] = campSite.Id;
				Holdings.Log(ev, string.Concat(new string[]
				{
					"TREASURY ",
					g.Name,
					": the stash moved from ",
					site.Label,
					" to ",
					campSite.Label,
					" (",
					flag4 ? "the old one was lost" : "after you robbed it",
					")"
				}));
				Holdings.StashBlips(ev);
				return true;
			}
			else
			{
				if (num >= 4)
				{
					return false;
				}
				if (site != null && (this.TreasuryRobbedNow(g) || this.TreasuryKnown(g)))
				{
					return false;
				}
				CampSite campSite2 = this.PickStash(g, site);
				if (campSite2 == null)
				{
					return false;
				}
				if (site != null)
				{
					Holdings.News(ev, string.Concat(new string[]
					{
						"Word is ",
						Holdings.The(g),
						" han trasladado su dinero fuera de ",
						this.From(g, site),
						"."
					}), false);
				}
				this.Treasuries[g.Id] = campSite2.Id;
				if (!this.StashList.Contains(g.Id))
				{
					this.StashList.Add(g.Id);
				}
				this.TreasuryKnownList.Remove(g.Id);
				changed = true;
				Holdings.Log(ev, string.Format("TREASURY {0}{1}{2}{3}{4}", new object[]
				{
					g.Name,
					num,
					(num == 1) ? "" : "s",
					campSite2.Label,
					(site != null) ? (" (was " + site.Label + ")") : ""
				}));
				Holdings.Blip(ev, (site != null) ? site.Id : null);
				Holdings.StashBlips(ev);
				return true;
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000F7D88 File Offset: 0x000F5F88
		private void PlaceSupplies(List<HoldingsEvent> ev, ref bool changed)
		{
			IEnumerable<Gang> outfits = this._w.Outfits;
			Func<Gang, bool> predicate;
			if ((predicate = Holdings.<>O.<1>__HoldsCamps) == null)
			{
				predicate = (Holdings.<>O.<1>__HoldsCamps = new Func<Gang, bool>(Holdings.HoldsCamps));
			}
			foreach (Gang gang in outfits.Where(predicate))
			{
				if (this._w.Destroyed(gang))
				{
					if (this.Forget(gang))
					{
						changed = true;
					}
				}
				else
				{
					CampSite site = this.SupplyCampOf(gang);
					if (site == null || !this._w.LiveSupplies(site.Id))
					{
						if (site != null && this._w.InTown(site))
						{
							string a;
							Holdings.News(ev, string.Concat(new string[]
							{
								"Word is ",
								Holdings.The(gang),
								" han trasladado sus suministros fuera de ",
								this.From(gang, site),
								"."
							}), this.SupplyKnownAt.TryGetValue(gang.Id, out a) && a == site.Id);
							this.SupplyCamps.Remove(gang.Id);
							this.SupplyCaches.Remove(gang.Id);
							this.SupplyKnownAt.Remove(gang.Id);
							changed = true;
							Holdings.Log(ev, string.Concat(new string[]
							{
								"SUPPLY ",
								gang.Name,
								": ",
								site.Label,
								" está en el pueblo; trasladan sus suministros fuera de allí"
							}));
							Holdings.Blip(ev, site.Id);
							site = null;
						}
						string text;
						string hit = (this.SupplyRaidedRecently(gang) && this.SupplyCampHitSite.TryGetValue(gang.Id, out text)) ? text : null;
						long num;
						bool flag = this.SupplyCampHit.TryGetValue(gang.Id, out num) && this._w.Now - num >= 1440L;
						bool flag2 = site != null && (this._w.Holder(site) == null || this._w.Cleared(site) || this.KeeperGone(gang, false) || this.SupplyCaches.ContainsKey(gang.Id));
						if (site != null && hit == site.Id && (flag || flag2))
						{
							if (!this.SupplyCacheKnown(gang))
							{
								NemesisRecord nemesisRecord = this._w.Holder(site);
								if (nemesisRecord != null)
								{
									bool campKnown = nemesisRecord.CampKnown;
								}
							}
							this.SupplyCamps.Remove(gang.Id);
							this.SupplyCaches.Remove(gang.Id);
							this.SupplyKnownAt.Remove(gang.Id);
							this.KeeperGoneList.Remove("sup:" + gang.Id);
							changed = true;
							Holdings.Log(ev, string.Concat(new string[]
							{
								"SUPPLY ",
								gang.Name,
								": destrozaste sus suministros en ",
								site.Label,
								" - lo que queda se traslada a otro campamento",
								flag ? "" : " (now: its keeper's gone / it was a cache / wiped out)"
							}));
							Holdings.News(ev, string.Concat(new string[]
							{
								"Word is ",
								Holdings.The(gang),
								" han llevado lo que quedaba de sus suministros fuera de ",
								this.From(gang, site),
								"."
							}), true);
							Holdings.Blip(ev, site.Id);
							Holdings.CacheBlips(ev);
							site = null;
						}
						string wiped = null;
						if (site != null && this._w.Cleared(site) && hit != site.Id)
						{
							string a2;
							bool flag3 = this.SupplyCacheKnown(gang) || (this.SupplyKnownAt.TryGetValue(gang.Id, out a2) && a2 == site.Id);
							this.SupplyCamps.Remove(gang.Id);
							this.SupplyCaches.Remove(gang.Id);
							this.SupplyKnownAt.Remove(gang.Id);
							changed = true;
							Holdings.Log(ev, string.Concat(new string[]
							{
								"SUPPLY ",
								gang.Name,
								": ",
								this._w.TakeoverPending(site) ? "otra banda está tomando" : "arrasaste",
								" ",
								site.Label,
								" - sus suministros van a otro campamento"
							}));
							Holdings.News(ev, string.Concat(new string[]
							{
								"Word is ",
								Holdings.The(gang),
								" han trasladado sus suministros fuera de ",
								this.From(gang, site),
								"."
							}), flag3 || !this._w.TakeoverPending(site));
							Holdings.Blip(ev, site.Id);
							Holdings.CacheBlips(ev);
							wiped = site.Id;
							site = null;
						}
						CampSite treasury = this.TreasuryOf(gang);
						bool noCamp = !this._w.SitesHeldBy(gang.Id).Any((CampSite s) => (treasury == null || s.Id != treasury.Id) && !this._w.TakeoverPending(s) && this._w.Holder(s) != null && !this._w.Wet(s));
						if (!this.PlaceSupplyCache(gang, site, noCamp, ev, ref changed) && (site == null || !(this._w.Owner(site) == gang.Id) || this._w.Holder(site) == null || this.IsTreasury(site)))
						{
							CampSite campSite = (from s in this._w.SitesHeldBy(gang.Id)
							where (site == null || s.Id != site.Id) && s.Id != hit && s.Id != wiped && (treasury == null || s.Id != treasury.Id) && !this._w.TakeoverPending(s) && this._w.Holder(s) != null && !this._w.Wet(s) && !this._w.InTown(s)
							orderby s.Hideout
							select s).ThenBy((CampSite _) => this._w.Roll(1048576)).FirstOrDefault<CampSite>();
							if (campSite == null)
							{
								if (site != null)
								{
									string a3;
									Holdings.News(ev, string.Concat(new string[]
									{
										"Word is ",
										Holdings.The(gang),
										" han retirado sus suministros de ",
										this.From(gang, site),
										"."
									}), this.SupplyKnownAt.TryGetValue(gang.Id, out a3) && a3 == site.Id);
									this.SupplyCamps.Remove(gang.Id);
									changed = true;
									Holdings.Log(ev, "SUPPLY " + gang.Name + ": no manned camp for their supplies - none for now");
									Holdings.Blip(ev, site.Id);
								}
							}
							else
							{
								if (site != null)
								{
									string a4;
									Holdings.News(ev, string.Concat(new string[]
									{
										"Word is ",
										Holdings.The(gang),
										" han trasladado sus suministros fuera de ",
										this.From(gang, site),
										"."
									}), this.SupplyKnownAt.TryGetValue(gang.Id, out a4) && a4 == site.Id);
								}
								this.SupplyCamps[gang.Id] = campSite.Id;
								changed = true;
								string a5;
								if (this.SupplyKnownAt.TryGetValue(gang.Id, out a5) && a5 != campSite.Id)
								{
									this.SupplyKnownAt.Remove(gang.Id);
								}
								string[] array = new string[6];
								array[0] = "SUPPLY ";
								array[1] = gang.Name;
								array[2] = ": their supplies at ";
								array[3] = campSite.Label;
								array[4] = ((site != null) ? " (moved)" : "");
								int num2 = 5;
								NemesisRecord nemesisRecord2 = this._w.Holder(campSite);
								array[num2] = ((nemesisRecord2 != null) ? (", held by " + nemesisRecord2.Name) : "");
								Holdings.Log(ev, string.Concat(array));
								CampSite site2 = site;
								Holdings.Blip(ev, (site2 != null) ? site2.Id : null);
								Holdings.Blip(ev, campSite.Id);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000F865C File Offset: 0x000F685C
		private bool PlaceSupplyCache(Gang g, CampSite site, bool noCamp, List<HoldingsEvent> ev, ref bool changed)
		{
			if (site == null)
			{
				return false;
			}
			if (!this.SupplyCaches.ContainsKey(g.Id))
			{
				if (this._w.Holder(site) != null || (!noCamp && !this.SupplyCacheKnown(g)) || this._w.Owner(site) != g.Id || this._w.TakeoverPending(site))
				{
					return false;
				}
				this.SupplyCaches[g.Id] = this._w.Now;
				changed = true;
				Holdings.Log(ev, string.Format("SUPPLY {0}: no manned camp left - their supplies stay at {1}, a cache (nobody keeps it for {2} hours){3}", new object[]
				{
					g.Name,
					site.Label,
					6L,
					this.SupplyCacheKnown(g) ? " - lo conoces" : ""
				}));
				Holdings.Blip(ev, site.Id);
				Holdings.CacheBlips(ev);
				return true;
			}
			else
			{
				NemesisRecord nemesisRecord = this._w.Holder(site);
				string a;
				bool flag = this.SupplyRaidedRecently(g) && this.SupplyCampHitSite.TryGetValue(g.Id, out a) && a == site.Id;
				if (nemesisRecord != null && !flag)
				{
					this.SupplyCaches.Remove(g.Id);
					changed = true;
					if (this.SupplyCacheKnown(g) && !nemesisRecord.CampKnown)
					{
						Holdings.Reveal(ev, nemesisRecord);
					}
					Holdings.Log(ev, string.Concat(new string[]
					{
						"SUPPLY ",
						g.Name,
						": the cache at ",
						site.Label,
						" is ",
						nemesisRecord.Name,
						"'s camp now - a supply camp again"
					}));
					Holdings.CacheBlips(ev);
					Holdings.Blip(ev, site.Id);
					return true;
				}
				if (this._w.Owner(site) != g.Id || this._w.TakeoverPending(site))
				{
					this.SupplyCaches.Remove(g.Id);
					this.SupplyCamps.Remove(g.Id);
					changed = true;
					Holdings.Log(ev, string.Concat(new string[]
					{
						"SUPPLY ",
						g.Name,
						": the cache at ",
						site.Label,
						" se perdió junto con el lugar"
					}));
					Holdings.CacheBlips(ev);
					return true;
				}
				if (this._w.LiveSupplies(site.Id))
				{
					return true;
				}
				if (!noCamp && !this.SupplyCacheKnown(g))
				{
					this.SupplyCaches.Remove(g.Id);
					this.SupplyCamps.Remove(g.Id);
					changed = true;
					Holdings.Log(ev, "SUPPLY " + g.Name + ": a manned camp again - the supplies leave the cache at " + site.Label);
					Holdings.CacheBlips(ev);
				}
				return true;
			}
		}

		// Token: 0x040007B9 RID: 1977
		internal const long SupplyRestock = 2880L;

		// Token: 0x040007BA RID: 1978
		internal const long TreasuryMoveAfter = 1440L;

		// Token: 0x040007BB RID: 1979
		internal const long CacheGuardDelay = 360L;

		// Token: 0x040007BC RID: 1980
		internal const long SuppliesMoveAfter = 1440L;

		// Token: 0x040007BD RID: 1981
		internal const double TreasuryShare = 0.4;

		// Token: 0x040007BE RID: 1982
		internal const int StashBelowCamps = 4;

		// Token: 0x040007BF RID: 1983
		internal const int StashBackAtCamps = 5;

		// Token: 0x040007C0 RID: 1984
		private readonly IHoldingsWorld _w;

		// Token: 0x020003EF RID: 1007
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001CEF RID: 7407
			public static Func<CampSite, Vector3> <0>__Pos;

			// Token: 0x04001CF0 RID: 7408
			public static Func<Gang, bool> <1>__HoldsCamps;
		}
	}
}
