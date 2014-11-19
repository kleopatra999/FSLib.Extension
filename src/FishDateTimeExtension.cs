﻿using System;
using System.Collections.Generic;
using System.FishExtension.Resources;
using System.Linq;
using System.Text;

namespace System
{
	/// <summary>
	/// 日期时间扩展类
	/// </summary>
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public static class FishDateTimeExtension
	{
		/// <summary>
		/// 将时间显示转换为更加友好的方式
		/// </summary>
		/// <param name="dt">要显示的时间</param>
		/// <returns>转换的结果</returns>
		public static string MakeDateTimeFriendly(this DateTime dt)
		{
			TimeSpan t = DateTime.Now - dt;

			if (t.TotalSeconds < 60) return string.Format(SR.FriendlyTime_Second, t.TotalSeconds > 0 ? (int)t.TotalSeconds : 0);
			if (t.TotalMinutes < 60) return string.Format(SR.FriendlyTime_Minute, (int)t.TotalMinutes);
			if (t.TotalHours < 24) return string.Format(SR.FriendlyTime_Hour, (int)t.TotalHours);
			if (t.TotalDays < 2)
			{
				string prefix = string.Empty;
				switch ((int)(DateTime.Now.AddHours(-dt.Hour) - dt).TotalDays)
				{
					case 0: prefix = SR.FriendlyTime_Yesterday; break;
					case 1: prefix = SR.FriendlyTime_DayBeforeYesterday; break;
					default:
						break;
				}
				return string.Format("{0} {1}", prefix, dt.ToString("HH:mm:ss"));
			}


			return dt.ToString();
		}

		/// <summary>
		/// 将时间的天转换为友好的标记
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string MakeDateFriendly(this DateTime dt)
		{
			var ts = dt.Date - DateTime.Now.Date;

			switch (ts.Days)
			{
				case 0:
					return "今天";
				case -1:
					return "昨天";
				case -2:
					return "前天";
				case 1:
					return "明天";
				case 2:
					return "后天";
			}

			return Math.Abs(ts.Days) + "天" + (ts.Days > 0 ? "后" : "前");
		}

		/// <summary>
		/// 获得JS的时间的开始值
		/// </summary>
		public static readonly DateTime JsTicksStartBase = new DateTime(1970, 1, 1, 0, 0, 0);

		/// <summary>
		/// 获得Javascript中时间刻度
		/// </summary>
		/// <param name="dt">要表示的时间</param>
		/// <returns>类型为 <see cref="T:System.Int64"/> 格式的数值</returns>
		public static uint ToJsTicks(this DateTime dt)
		{
			return (uint)Math.Floor((dt.ToUniversalTime() - JsTicksStartBase).Ticks / 10000.0);
		}

		/// <summary>
		/// 获得两个日期之间的月数
		/// </summary>
		/// <param name="beginDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public static double GetMonthesBetween(this DateTime beginDate, DateTime endDate)
		{
			var months = 0.0;
			for (; beginDate <= endDate; beginDate = beginDate.AddMonths(1))
			{
				months++;
			}
			months = months - (beginDate - endDate).TotalDays / DateTime.DaysInMonth(endDate.Year, endDate.Month);
			return months;
		}

		/// <summary>
		/// 返回时间所在月份的第一天
		/// </summary>
		/// <param name="dt">当前时间</param>
		/// <returns>代表当月第一天的 <see cref="T:System.DateTime"/></returns>
		public static DateTime GetMonthStart(this DateTime dt)
		{
			return dt.AddDays(-DateTime.Now.Day + 1);
		}

		/// <summary>
		/// 返回时间所在月份的最后一天
		/// </summary>
		/// <param name="dt">当前时间</param>
		/// <returns>代表当月最后一天的 <see cref="T:System.DateTime"/></returns>
		public static DateTime GetMonthEnd(this DateTime dt)
		{
			return dt.AddDays(DateTime.DaysInMonth(dt.Year, dt.Month) - dt.Day);
		}

		/// <summary>
		/// 将时间精确到毫秒
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static DateTime TrimToMilliSecond(this DateTime dt)
		{
			var ticks = dt.Ticks;
			ticks -= ticks % 10000;
			return new DateTime(ticks);
		}

		/// <summary>
		/// 将时间精确到秒
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static DateTime TrimToSecond(this DateTime dt)
		{
			var ticks = dt.Ticks;
			ticks -= ticks % (10000 * 1000);
			return new DateTime(ticks);
		}

		/// <summary>
		/// 将时间精确到分钟
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static DateTime TrimToMinute(this DateTime dt)
		{
			var ticks = dt.Ticks;
			ticks -= ticks % (10000 * 1000 * 60);
			return new DateTime(ticks);
		}


		/// <summary>
		/// 将时间精确到小时
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static DateTime TrimToHour(this DateTime dt)
		{
			var ticks = dt.Ticks;
			ticks -= ticks % (10000L * 1000 * 60 * 60);
			return new DateTime(ticks);
		}

		/// <summary>
		/// 将时间精确到天
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static DateTime TrimToDay(this DateTime dt)
		{
			var ticks = dt.Ticks;
			ticks -= ticks % (10000L * 1000 * 60 * 60 * 24);
			return new DateTime(ticks);
		}

		/// <summary>
		/// 将TimeSpan偏移指定的小时
		/// </summary>
		/// <param name="timeSpan"></param>
		/// <param name="hours"></param>
		/// <returns></returns>
		public static TimeSpan AddHours(this TimeSpan timeSpan, int hours)
		{
			return timeSpan.Add(new TimeSpan(hours, 0, 0));
		}

		/// <summary>
		/// 将TimeSpan偏移指定的分钟
		/// </summary>
		/// <param name="timeSpan"></param>
		/// <returns></returns>
		public static TimeSpan AddMinutes(this TimeSpan timeSpan, int minutes)
		{
			return timeSpan.Add(new TimeSpan(0, minutes, 0));
		}

		/// <summary>
		/// 将TimeSpan偏移指定的秒数
		/// </summary>
		/// <param name="timeSpan"></param>
		/// <returns></returns>
		public static TimeSpan AddSeconds(this TimeSpan timeSpan, int seconds)
		{
			return timeSpan.Add(new TimeSpan(0, 0, seconds));
		}

		/// <summary>
		/// 将TimeSpan偏移指定的天
		/// </summary>
		/// <param name="timeSpan"></param>
		/// <returns></returns>
		public static TimeSpan AddDays(this TimeSpan timeSpan, int days)
		{
			return timeSpan.Add(new TimeSpan(days, 0, 0, 0));
		}
	}
}
