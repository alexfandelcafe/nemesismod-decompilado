using System;
using System.Collections.Generic;
using System.Linq;
using RDR2.Math;

namespace NemesisSystem
{
	// Token: 0x0200000F RID: 15
	internal static class Camps
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000A998 File Offset: 0x00008B98
		public static string RetiredName(string id)
		{
			if (id == null)
			{
				return null;
			}
			CampSite campSite = Camps.Retired.FirstOrDefault((CampSite s) => s.Id == id);
			if (campSite == null)
			{
				return null;
			}
			return campSite.Name;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public static CampSite RetiredNear(Vector3 p, float radius)
		{
			return Camps.Retired.FirstOrDefault((CampSite s) => new Vector3(p.X - s.X, p.Y - s.Y, 0f).Length() < radius);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000AA18 File Offset: 0x00008C18
		public static CampSite[] ForGang(string gangId)
		{
			return ((gangId == "odriscoll") ? Camps.ODriscoll : new CampSite[0]).Concat(from c in Camps.GameGangCamps
			where c.Gang == gangId
			select c).Concat(from c in Camps.Extra
			where c.Gang == gangId
			select c).ToArray<CampSite>();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public static IEnumerable<CampSite> AllSites
		{
			get
			{
				return Camps.ODriscoll.Concat(Camps.GameGangCamps).Concat(Camps.Extra);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000AAA7 File Offset: 0x00008CA7
		public static string StartingOwner(CampSite s)
		{
			string result;
			if (s != null)
			{
				if ((result = s.Gang) == null)
				{
					return "odriscoll";
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public static CampSite ById(string id)
		{
			CampSite result;
			if (id != null)
			{
				if ((result = Camps.ODriscoll.FirstOrDefault((CampSite c) => c.Id == id)) == null && (result = Camps.GameGangCamps.FirstOrDefault((CampSite c) => c.Id == id)) == null)
				{
					return Camps.Extra.FirstOrDefault((CampSite c) => c.Id == id);
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000117 RID: 279
		public static readonly CampSite[] ODriscoll = new CampSite[]
		{
			new CampSite("six_point_cabin", 0U, -43.4f, 1230.9f, 172.7f, "Cumberland Forest", true, "Six Point Cabin", "odriscoll"),
			new CampSite("hanging_dog_ranch", 0U, -2197.1f, 700.3f, 121.5f, "Big Valley", true, "Hanging Dog Ranch", "odriscoll"),
			new CampSite("gang_odriscollboys_bgv001", 4186667583U, -1118.4f, -220.2f, 91.2f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv002", 2285934507U, -980.4f, -212.6f, 90.2f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv003", 3472794918U, -2601.2f, -24.6f, 161.5f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv004", 3954957984U, -1602.5f, -893f, 86.5f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv005", 1122045165U, -2763.2f, 113.3f, 165.2f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv006", 1352837232U, -1448.7f, 101.4f, 91.6f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv007", 2554279848U, -1580.8f, 496.3f, 115.5f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv008", 2786710365U, -1345.1f, 492.8f, 97.3f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv009", 1739445890U, -1711.5f, 605.2f, 119.3f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_bgv010", 3477907118U, -1622.6f, 822.5f, 153.1f, "Big Valley", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_cml001", 1548242606U, -2.8f, 949.6f, 211.4f, "Cumberland Forest", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_cml002", 3102214124U, 645.5f, 1298.7f, 197.1f, "Cumberland Forest", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_cml003", 3812711582U, 102.4f, 1406.9f, 168.4f, "Cumberland Forest", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_cml004", 4058053085U, 586.5f, 1431.2f, 185.7f, "Cumberland Forest", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_cml005", 2130187273U, 497.3f, 1498.1f, 173.9f, "Cumberland Forest", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt001", 3007696601U, -647.6f, -391.5f, 80.3f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odoriscollboys_hrt002", 3358762491U, 499.1f, -259.1f, 144.8f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt003", 2409367430U, 649.7f, -111.8f, 150.6f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt004", 4176796214U, 1059.1f, 34.3f, 87.3f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odoriscollboys_hrt005", 2118488614U, -1172.4f, 94.5f, 45.1f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt006", 1726559781U, -961f, 296.2f, 96.1f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt007", 1455068616U, -645.9f, 277.5f, 91.5f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt008", 3225381068U, -191.8f, 294.7f, 97.5f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt009", 3606222394U, 182.8f, 344.3f, 121.4f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt010", 2301131135U, -849.9f, 465.8f, 91f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt011", 402954041U, 439.7f, 830f, 147.1f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt012", 616706228U, 275.6f, 835.1f, 191.2f, "the Heartlands", false, null, "odriscoll"),
			new CampSite("gang_odriscollboys_hrt013", 3015134876U, 1000.1f, 991.4f, 152.3f, "the Heartlands", false, null, "odriscoll")
		};

		// Token: 0x04000118 RID: 280
		public static readonly CampSite[] GameGangCamps = new CampSite[]
		{
			new CampSite("gang_lemoyne_scm001", 1971923782U, 1173.2f, -2180.8f, 55.8f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm002", 401043460U, 1426.4f, -2185f, 50f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm003", 104287396U, 1562.1f, -2048.9f, 49f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm004", 1014446371U, 1705f, -2008.8f, 45.4f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm005", 715920781U, 1380.3f, -1696.1f, 68.3f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm006", 3394426076U, 1622.1f, -1649.4f, 57.7f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm007", 3095671103U, 1503f, -1470.5f, 73f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm008", 4004421011U, 868.5f, -1330.5f, 56.3f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm009", 3741777476U, 948f, -1318.5f, 59.8f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm010", 546408124U, 1083.4f, -1268.5f, 69.2f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm011", 1313890873U, 701.8f, -1173.6f, 48.6f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm012", 129717520U, 856.7f, -1134.3f, 59f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm013", 3062182565U, 683.4f, -992.4f, 54.1f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm014", 3812068361U, 911.2f, -986.3f, 58.6f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm015", 301558156U, 708.5f, -885.4f, 49.9f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm016", 1859330882U, 1334.9f, -594.7f, 76.2f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm017", 1683033662U, 879.3f, -538.3f, 89.7f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm018", 2875595879U, 961.2f, -572.9f, 90.9f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_lemoyne_scm019", 3651926258U, 1012.5f, -392.5f, 92.1f, "Scarlett Meadows", false, null, "lemoyneraiders"),
			new CampSite("gang_skinner_tal001", 3926071903U, -2014.5f, -1828.5f, 114f, "Tall Trees", false, null, "skinners"),
			new CampSite("gang_skinner_tal002", 4189370818U, -2136.9f, -1783.6f, 139.3f, "Tall Trees", false, null, "skinners"),
			new CampSite("gang_skinner_tal003", 190078209U, -2667.6f, -1462.4f, 146.2f, "Tall Trees", false, null, "skinners"),
			new CampSite("gang_skinner_tal004", 561187138U, -2429.4f, -1329.6f, 151.9f, "Tall Trees", false, null, "skinners"),
			new CampSite("gang_skinner_tal005", 859352269U, -2109.8f, -1275f, 123.7f, "Tall Trees", false, null, "skinners"),
			new CampSite("gang_skinner_tal006", 1023328345U, -2033.1f, -1278.6f, 115.2f, "Tall Trees", false, null, "skinners"),
			new CampSite("murfree_beaver_hollow", 0U, 2351.4f, 1358.2f, 106.9f, "Roanoke Ridge", true, "Beaver Hollow", "murfree"),
			new CampSite("murfree_ransack07", 0U, 2357.8f, 836.1f, 79.6f, "Roanoke Ridge", false, null, "murfree"),
			new CampSite("murfree_ransack11", 0U, 2299.3f, 1050.8f, 116.6f, "Roanoke Ridge", false, null, "murfree"),
			new CampSite("murfree_ransack12", 0U, 2517.6f, 1746.4f, 88.3f, "Roanoke Ridge", false, null, "murfree"),
			new CampSite("murfree_ransack13", 0U, 2792.8f, 2008.3f, 165.3f, "Roanoke Ridge", false, null, "murfree"),
			new CampSite("murfree_ransack01", 0U, 2119.9f, -139.3f, 50.3f, "Bluewater Marsh", false, null, "murfree"),
			new CampSite("murfree_mossy_flats", 0U, 2453.5f, 290.5f, 70.4f, "Roanoke Ridge", false, null, "murfree"),
			new CampSite("murfree_rocky_seven", 0U, 2996.8f, 2197.3f, 166.1f, "Roanoke Ridge", false, null, "murfree"),
			new CampSite("bronte_mansion", 0U, 2338.6f, -1204.4f, 44f, "Bayou Nwa", true, "the Bronte Mansion", "bronte"),
			new CampSite("bronte_docks", 0U, 2806.9f, -1417.9f, 43f, "Saint Denis", false, null, "bronte"),
			new CampSite("bronte_warehouses", 0U, 2574.1f, -1405.9f, 45f, "Saint Denis", false, null, "bronte"),
			new CampSite("bronte_railyard", 0U, 2327.5f, -1502.4f, 46.2f, "Saint Denis", false, null, "bronte"),
			new CampSite("bronte_slums", 0U, 2840f, -1229.3f, 47.6f, "Saint Denis", false, null, "bronte"),
			new CampSite("bronte_lakay", 0U, 2264.4f, -764.2f, 43.1f, "Bayou Nwa", true, "Lakay", "bronte"),
			new CampSite("bronte_boathouse", 0U, 2020.9f, -775.8f, 42.7f, "Bayou Nwa", false, null, "bronte"),
			new CampSite("bronte_copperhead", 0U, 2040.7f, -1639.4f, 41.3f, "Bayou Nwa", false, null, "bronte"),
			new CampSite("laramie_manteca", 0U, -1657f, -2760f, 71f, "Hennigan's Stead", true, "Manteca Falls", "laramie"),
			new CampSite("laramie_hen_shack", 0U, -1810.8f, -2393.1f, 41.5f, "Hennigan's Stead", false, null, "laramie"),
			new CampSite("laramie_hen_cabin", 0U, -1412.4f, -2671.5f, 43.4f, "Hennigan's Stead", false, null, "laramie"),
			new CampSite("laramie_quakers", 0U, -1199.9f, -1938.9f, 42.6f, "the Great Plains", false, null, "laramie"),
			new CampSite("delobo_gaptooth", 0U, -5990f, -3261f, -21f, "Gaptooth Ridge", true, "Gaptooth Breach", "delobo"),
			new CampSite("delobo_gap_shack", 0U, -5850.3f, -3743.9f, -25.9f, "Gaptooth Ridge", false, null, "delobo"),
			new CampSite("delobo_ben_shack", 0U, -5204.8f, -3490.9f, -23.4f, "Gaptooth Ridge", false, null, "delobo"),
			new CampSite("delobo_rio_camp", 0U, -5248.1f, -3851.5f, -2.1f, "Rio Bravo", false, null, "delobo"),
			new CampSite("delobo_cho_shack", 0U, -3400.6f, -3305.4f, -6.2f, "Cholla Springs", false, null, "delobo"),
			new CampSite("delobo_cho_shack2", 0U, -3552.2f, -3009.9f, 10.1f, "Cholla Springs", false, null, "delobo"),
			new CampSite("delobo_cho_rocks", 0U, -3956.8f, -2124.6f, -5.8f, "Cholla Springs", false, null, "delobo"),
			new CampSite("nightfolk_shack", 0U, 1707.4f, -1005.4f, 40.2f, "Bayou Nwa", false, null, "nightfolk"),
			new CampSite("nightfolk_shady", 0U, 1870f, -1853f, 43f, "Bayou Nwa", false, null, "nightfolk"),
			new CampSite("placed_delobo_1d0d56", 0U, -4350.9f, -2950.6f, -14.5f, "Cholla Springs", false, null, "delobo"),
			new CampSite("placed_delobo_03907f", 0U, -3835.7f, -3150.5f, -12.4f, "Cholla Springs", false, null, "delobo"),
			new CampSite("placed_delobo_80f73c", 0U, -5600.3f, -3306.9f, -21.5f, "Gaptooth Ridge", false, null, "delobo"),
			new CampSite("placed_laramie_a7c059", 0U, -1949.7f, -2993.8f, -4.4f, "Hennigan's Stead", false, null, "laramie"),
			new CampSite("placed_skinners_4fa891", 0U, -1545f, -1958.1f, 47.7f, "Tall Trees", false, null, "skinners"),
			new CampSite("placed_skinners_7ac923", 0U, -2236f, -1291.2f, 134.9f, "Tall Trees", false, null, "skinners"),
			new CampSite("placed_murfree_db3b49", 0U, 2476.3f, 1756.8f, 87.5f, "Roanoke Ridge", false, null, "murfree"),
			new CampSite("placed_murfree_70f4d7", 0U, 3244.3f, 1488.8f, 51.7f, "Roanoke Ridge", false, null, "murfree"),
			new CampSite("placed_lemoyneraiders_c2958d", 0U, 2172.3f, -399.5f, 42.4f, "Bluewater Marsh", false, null, "lemoyneraiders"),
			new CampSite("placed_lemoyneraiders_701490", 0U, 1053.3f, 1248.7f, 201.2f, "the Grizzlies", false, null, "lemoyneraiders"),
			new CampSite("wapiti_hunting_camp", 0U, 712.7f, 1882.3f, 239.1f, "Grizzlies East", false, null, "wapiti"),
			new CampSite("wapiti_lowland_camp", 0U, 794f, 1511f, 204f, "Grizzlies East", false, null, "wapiti"),
			new CampSite("wapiti_ridge", 0U, 992.4f, 1452.6f, 276f, "Grizzlies East", false, null, "wapiti")
		};

		// Token: 0x04000119 RID: 281
		public static List<CampSite> Extra = new List<CampSite>();

		// Token: 0x0400011A RID: 282
		public static readonly CampSite[] Retired = new CampSite[]
		{
			new CampSite("wapiti_village", 0U, 440.8f, 2229.5f, 247.8f, "Grizzlies East", true, "the Wapiti Reservation", "wapiti")
		};

		// Token: 0x0400011B RID: 283
		public const float RetiredCalmRadius = 300f;
	}
}
