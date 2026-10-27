using System;
using System.Collections.Generic;
using RDR2.Math;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000099 RID: 153
	public sealed class ImpactRecord
	{
		// Token: 0x04000E23 RID: 3619
		public int PedHandle;

		// Token: 0x04000E24 RID: 3620
		public uint PedModel;

		// Token: 0x04000E25 RID: 3621
		public bool VictimDeadAfterHit;

		// Token: 0x04000E26 RID: 3622
		public bool Animal;

		// Token: 0x04000E27 RID: 3623
		public int BoneId = -1;

		// Token: 0x04000E28 RID: 3624
		public int BoneIndex = -1;

		// Token: 0x04000E29 RID: 3625
		public string BoneName;

		// Token: 0x04000E2A RID: 3626
		public bool HasImpact;

		// Token: 0x04000E2B RID: 3627
		public Vector3 Impact;

		// Token: 0x04000E2C RID: 3628
		public Vector3 Muzzle;

		// Token: 0x04000E2D RID: 3629
		public string MuzzleSource;

		// Token: 0x04000E2E RID: 3630
		public uint WeaponHash;

		// Token: 0x04000E2F RID: 3631
		public string WeaponName;

		// Token: 0x04000E30 RID: 3632
		public AmmoKind Ammo;

		// Token: 0x04000E31 RID: 3633
		public bool IsShotgun;

		// Token: 0x04000E32 RID: 3634
		public float Damage;

		// Token: 0x04000E33 RID: 3635
		public int PelletsOnVictim;

		// Token: 0x04000E34 RID: 3636
		public int ShotId;

		// Token: 0x04000E35 RID: 3637
		public int EventIndex;

		// Token: 0x04000E36 RID: 3638
		public Vector3 EventCoord;

		// Token: 0x04000E37 RID: 3639
		public float Distance;

		// Token: 0x04000E38 RID: 3640
		public int GameTime;

		// Token: 0x04000E39 RID: 3641
		public WeaponClass Class;

		// Token: 0x04000E3A RID: 3642
		public List<BonePoint> BonesLocal = new List<BonePoint>();

		// Token: 0x04000E3B RID: 3643
		public V3 EntryLocal;

		// Token: 0x04000E3C RID: 3644
		public V3 DirLocal;

		// Token: 0x04000E3D RID: 3645
		public V3 MuzzleLocal;

		// Token: 0x04000E3E RID: 3646
		public bool ImpactOnVictim;

		// Token: 0x04000E3F RID: 3647
		public int BlowBone = -1;

		// Token: 0x04000E40 RID: 3648
		public bool Butt;

		// Token: 0x04000E41 RID: 3649
		public bool Kick;

		// Token: 0x04000E42 RID: 3650
		public bool Choke;

		// Token: 0x04000E43 RID: 3651
		public bool Stomp;

		// Token: 0x04000E44 RID: 3652
		public bool Down;

		// Token: 0x04000E45 RID: 3653
		public int HeldMs;
	}
}
