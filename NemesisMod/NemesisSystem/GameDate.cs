using System;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x0200006B RID: 107
	public static class GameDate
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x001226AC File Offset: 0x001208AC
		public static string Month(int month1)
		{
			return GameDate.Months[Math.Max(1, Math.Min(12, month1)) - 1];
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x001226C4 File Offset: 0x001208C4
		public static long ToMinutes(DateTime d)
		{
			return (long)(d - GameDate.Epoch).TotalMinutes;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x001226E8 File Offset: 0x001208E8
		public static DateTime FromMinutes(long m)
		{
			return GameDate.Epoch.AddMinutes((double)m);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00122704 File Offset: 0x00120904
		public static int DayKey(DateTime d)
		{
			return d.Year * 10000 + (d.Month - 1) * 100 + d.Day;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00122728 File Offset: 0x00120928
		public static DateTime FromDayKey(int key)
		{
			int year = Math.Max(1, key / 10000);
			int month = Math.Max(1, Math.Min(12, key / 100 % 100 + 1));
			return new DateTime(year, month, Math.Max(1, Math.Min(DateTime.DaysInMonth(year, month), key % 100)));
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00122777 File Offset: 0x00120977
		public static int TodayKey()
		{
			return CLOCK.GET_CLOCK_YEAR() * 10000 + CLOCK.GET_CLOCK_MONTH() * 100 + CLOCK.GET_CLOCK_DAY_OF_MONTH();
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00122794 File Offset: 0x00120994
		public static string NowText()
		{
			int clock_HOURS = CLOCK.GET_CLOCK_HOURS();
			int clock_MINUTES = CLOCK.GET_CLOCK_MINUTES();
			return string.Format("{0} {1}, {2}  {3}:{4:00} {5}", new object[]
			{
				GameDate.Months[Math.Max(0, Math.Min(11, CLOCK.GET_CLOCK_MONTH()))],
				CLOCK.GET_CLOCK_DAY_OF_MONTH(),
				CLOCK.GET_CLOCK_YEAR(),
				(clock_HOURS % 12 == 0) ? 12 : (clock_HOURS % 12),
				clock_MINUTES,
				(clock_HOURS < 12) ? "AM" : "PM"
			});
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00122825 File Offset: 0x00120A25
		public static string TodayShort()
		{
			return string.Format("{0} {1}", GameDate.Months[Math.Max(0, Math.Min(11, CLOCK.GET_CLOCK_MONTH()))], CLOCK.GET_CLOCK_DAY_OF_MONTH());
		}

		// Token: 0x04000C38 RID: 3128
		public static readonly DateTime Epoch = new DateTime(1890, 1, 1);

		// Token: 0x04000C39 RID: 3129
		public static readonly string[] Months = new string[]
		{
			"Jan",
			"Feb",
			"Mar",
			"Apr",
			"mayo",
			"Jun",
			"Jul",
			"Aug",
			"Sep",
			"Oct",
			"Nov",
			"Dec"
		};
	}
}
