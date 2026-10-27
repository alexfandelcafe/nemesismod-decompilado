using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000096 RID: 150
	internal static class CrippleRules
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x00143740 File Offset: 0x00141940
		public static CrippleLevel Assess(IEnumerable<DamageItem> damage, out string effect)
		{
			HashSet<string> hashSet = new HashSet<string>(from d in damage
			where !d.Chipped
			select d.Key);
			if (hashSet.Contains("cervical"))
			{
				effect = "Cuello roto - paralizado";
				return CrippleLevel.NeckBroken;
			}
			if (hashSet.Contains("spine_t") || hashSet.Contains("spine_l"))
			{
				effect = "Columna seccionada - paralizado";
				return CrippleLevel.SpineBroken;
			}
			if (hashSet.Contains("pelvis"))
			{
				effect = "Pelvis destrozada - no puede mantenerse en pie";
				return CrippleLevel.Legless;
			}
			foreach (ValueTuple<string, string> valueTuple in new ValueTuple<string, string>[]
			{
				new ValueTuple<string, string>("knee", "Rodilla destrozada"),
				new ValueTuple<string, string>("femur", "Pierna rota")
			})
			{
				string item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				if (hashSet.Contains(item + "_l") || hashSet.Contains(item + "_r"))
				{
					effect = item2 + " - no puede mantenerse en pie";
					return CrippleLevel.Legless;
				}
			}
			bool flag = hashSet.Contains("foot_l");
			bool flag2 = hashSet.Contains("foot_r");
			if (flag && flag2)
			{
				effect = "Ambos pies destrozados - no puede mantenerse en pie";
				return CrippleLevel.Legless;
			}
			if (flag || flag2)
			{
				effect = "Pie destrozado - cojeando";
				return CrippleLevel.Limping;
			}
			bool flag3 = hashSet.Contains("tibia_l");
			bool flag4 = hashSet.Contains("tibia_r");
			if (flag3 && flag4)
			{
				effect = "Ambas tibias rotas - no puede mantenerse en pie";
				return CrippleLevel.Legless;
			}
			if (flag3 || flag4)
			{
				effect = "Tibia rota - cojeando";
				return CrippleLevel.Limping;
			}
			effect = null;
			return CrippleLevel.None;
		}
	}
}
