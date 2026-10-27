using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x0200009F RID: 159
	public sealed class XRayEntry
	{
		// Token: 0x0600147C RID: 5244 RVA: 0x00147C18 File Offset: 0x00145E18
		public XRayEntry Copy()
		{
			XRayEntry xrayEntry = (XRayEntry)base.MemberwiseClone();
			xrayEntry.Analysis = null;
			return xrayEntry;
		}

		// Token: 0x04000E5E RID: 3678
		public int Id;

		// Token: 0x04000E5F RID: 3679
		public int ShootoutId;

		// Token: 0x04000E60 RID: 3680
		public DateTime Time;

		// Token: 0x04000E61 RID: 3681
		public long GameMin;

		// Token: 0x04000E62 RID: 3682
		public string Location;

		// Token: 0x04000E63 RID: 3683
		public string Victim;

		// Token: 0x04000E64 RID: 3684
		public uint VictimModel;

		// Token: 0x04000E65 RID: 3685
		public int VictimId;

		// Token: 0x04000E66 RID: 3686
		public string NemesisId;

		// Token: 0x04000E67 RID: 3687
		public string NemesisName;

		// Token: 0x04000E68 RID: 3688
		public string FollowerOf;

		// Token: 0x04000E69 RID: 3689
		public uint WeaponHash;

		// Token: 0x04000E6A RID: 3690
		public string WeaponName;

		// Token: 0x04000E6B RID: 3691
		[JsonConverter(typeof(StringEnumConverter))]
		public WeaponClass WeaponClass;

		// Token: 0x04000E6C RID: 3692
		public int Pellets;

		// Token: 0x04000E6D RID: 3693
		[JsonConverter(typeof(StringEnumConverter))]
		public AmmoKind Ammo;

		// Token: 0x04000E6E RID: 3694
		public float Distance;

		// Token: 0x04000E6F RID: 3695
		public int RecordedBoneId;

		// Token: 0x04000E70 RID: 3696
		public bool ImpactOnVictim;

		// Token: 0x04000E71 RID: 3697
		public V3 Entry;

		// Token: 0x04000E72 RID: 3698
		public V3 Dir;

		// Token: 0x04000E73 RID: 3699
		public V3 Muzzle;

		// Token: 0x04000E74 RID: 3700
		public List<BonePoint> Bones = new List<BonePoint>();

		// Token: 0x04000E75 RID: 3701
		public string Effect;

		// Token: 0x04000E76 RID: 3702
		public bool Killed;

		// Token: 0x04000E77 RID: 3703
		public bool FatalShot;

		// Token: 0x04000E78 RID: 3704
		public bool Animal;

		// Token: 0x04000E79 RID: 3705
		public List<Blow> Blows;

		// Token: 0x04000E7A RID: 3706
		public bool KnockedOut;

		// Token: 0x04000E7B RID: 3707
		public bool LeftForDead;

		// Token: 0x04000E7C RID: 3708
		[JsonIgnore]
		public int PedHandle;

		// Token: 0x04000E7D RID: 3709
		[JsonIgnore]
		public XRayAnalysis Analysis;
	}
}
