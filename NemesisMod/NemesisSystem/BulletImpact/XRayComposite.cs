using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x020000A1 RID: 161
	public sealed class XRayComposite
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x00149A2C File Offset: 0x00147C2C
		public static XRayComposite ForEntry(XRayEntry e)
		{
			if (e.Analysis == null)
			{
				e.Analysis = XRayAnalysis.Build(e);
			}
			XRayComposite xrayComposite = new XRayComposite();
			xrayComposite.Body = e.Analysis.Body;
			xrayComposite.AddAnalysis(e.Analysis, null);
			return xrayComposite;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00149A68 File Offset: 0x00147C68
		public static XRayComposite ForShots(IList<XRayEntry> shots)
		{
			foreach (XRayEntry xrayEntry in shots)
			{
				if (xrayEntry.Analysis == null)
				{
					xrayEntry.Analysis = XRayAnalysis.Build(xrayEntry);
				}
			}
			XRayComposite xrayComposite = new XRayComposite
			{
				Body = shots[0].Analysis.Body
			};
			foreach (XRayEntry xrayEntry2 in shots)
			{
				xrayComposite.AddAnalysis(xrayEntry2.Analysis, (xrayEntry2.Analysis.Body == xrayComposite.Body) ? null : xrayEntry2.Analysis.Body);
			}
			return xrayComposite;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00149B38 File Offset: 0x00147D38
		private void AddAnalysis(XRayAnalysis a, Anatomy.Body from)
		{
			if (a.Main != null)
			{
				this.Paths.Add(new ValueTuple<PathResult, bool>(this.Remap(a.Main, from), false));
			}
			foreach (PathResult p in a.Pellets)
			{
				this.Paths.Add(new ValueTuple<PathResult, bool>(this.Remap(p, from), true));
			}
			foreach (DamageItem damageItem in a.AllDamage)
			{
				if (this.Keys.Add(damageItem.Key))
				{
					this.Damage.Add(damageItem);
				}
			}
			this.Damage.Sort((DamageItem x, DamageItem y) => y.Severity.CompareTo(x.Severity));
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00149C44 File Offset: 0x00147E44
		private PathResult Remap(PathResult p, Anatomy.Body from)
		{
			if (from == null)
			{
				return p;
			}
			PathResult pathResult = new PathResult
			{
				Start = this.Map(p.Start, from),
				Entry = this.Map(p.Entry, from),
				End = this.Map(p.End, from),
				HasEntry = p.HasEntry,
				Exited = p.Exited,
				Estimated = p.Estimated,
				Blunt = p.Blunt,
				EntryText = p.EntryText,
				EndText = p.EndText
			};
			pathResult.Damage.AddRange(p.Damage);
			foreach (string item in p.HitKeys)
			{
				pathResult.HitKeys.Add(item);
			}
			return pathResult;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00149D3C File Offset: 0x00147F3C
		private V3 Map(V3 q, Anatomy.Body src)
		{
			int num = -1;
			float num2 = float.MaxValue;
			foreach (KeyValuePair<int, V3> keyValuePair in src.Bones)
			{
				if (this.Body.Has(keyValuePair.Key))
				{
					float num3 = (q - keyValuePair.Value).Length();
					if (num3 < num2)
					{
						num2 = num3;
						num = keyValuePair.Key;
					}
				}
			}
			if (num >= 0)
			{
				return this.Body.Get(num) + (q - src.Get(num));
			}
			return q;
		}

		// Token: 0x04000E82 RID: 3714
		public Anatomy.Body Body;

		// Token: 0x04000E83 RID: 3715
		[TupleElementNames(new string[]
		{
			"path",
			"thin"
		})]
		public readonly List<ValueTuple<PathResult, bool>> Paths = new List<ValueTuple<PathResult, bool>>();

		// Token: 0x04000E84 RID: 3716
		public readonly HashSet<string> Keys = new HashSet<string>();

		// Token: 0x04000E85 RID: 3717
		public readonly List<DamageItem> Damage = new List<DamageItem>();
	}
}
