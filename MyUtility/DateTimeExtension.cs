using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyUtility.Extensions;

namespace MyUtility
{
    public static class DateTimeExtension
    {
        public enum DateFormat
        {
            [Description("dd/MM/yyyy")]
            NgayThangNam = 1,

            [Description("yyyy/MM/dd")]
            NamThangNgay = 2,

            [Description("MM/dd/yyyy")]
            ThangNgayNam = 3,

            [Description("yyyy-MM-dd")]
            NamThangNgaySql = 4,

            [Description("yyyyMMdd")]
            NamThangNgayExport = 5,

            [Description("MM/yyyy")]
            ThangNam = 6,

            [Description("dd/MM")]
            NgayThang = 7,

            [Description("yyyy-MM")]
            NamThangSql = 8,
        }

        public enum TimeFormat
        {
            [Description("H:mm:ss")]
            Hmmss,

            [Description("HH:mm:ss")]
            HHmmss,

            [Description("H:mm:ss:fff")]
            Hmmssfff,

            [Description("HH:mm:ss:fff")]
            HHmmssfff,

            [Description("HH:mm")]
            HHmm,
        }

        public enum DateTimeFormat
        {
            [Description("dd/MM/yyyy HH:mm")]
            NgayThangNam_HHmm,

            [Description("dd/MM/yyyy HH:mm:ss")]
            NgayThangNam_HHmmss,

            [Description("dd/MM/yyyy HH:mm:ss:fff")]
            NgayThangNam_HHmmssfff,

			[Description("yyyy-MM-ddTHH:mm:ss.fffZ")]
			ISODate
		}

        public enum DateTimeDurationFormat
        {
            [Description("{0}:{1}")]
            HHmm,

            [Description("{0}:{1}:{2}")]
            HHmmss,

            [Description("{0} {1}:{2}")]
            Ngay_HHmm,

            [Description("{0} {1}:{2}:{3}")]
            Ngay_HHmmss
        }

        //public enum GroupDateTypeEnum
        //{
        //    GroupMonthDate = 1 ,
        //    GroupWeekDate = 2 ,
        //    GroupDayDate = 3 ,
        //    GroupHourDate = 4 ,
        //    Exception = -1,
        //    RangeDate = 5,
        //    RangeHourDay = 6 // group ngày theo khung giờ
        //}

        public static DateTime ParseExact(string datetimeString, DateFormat format)
        {
            switch (format)
            {
                case DateFormat.ThangNgayNam:
                    return ParseExactThangNgayNam(datetimeString);

                case DateFormat.NgayThangNam:
                    return ParseExactNgayThangNam(datetimeString);

                case DateFormat.NamThangNgay:
                    return ParseExactNamThangNgay(datetimeString);

                case DateFormat.NamThangNgaySql:
                    return ParseExactNamThangNgaySql(datetimeString);

                default:
                    throw new Exception("Not regconize format");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datetimeString"></param>
        /// <param name="dateFormat"></param>
        /// <param name="timeFormat"></param>
        /// <param name="isGetDateIfError">Neu convert sang datetime bi loi thi tu dong convert sang date thoi</param>
        /// <returns></returns>
        public static DateTime ParseExact(string datetimeString, DateFormat dateFormat, TimeFormat timeFormat, bool isGetDateIfError = false)
        {
            if (isGetDateIfError)
            {
                try
                {
                    return ParseExactGeneral(datetimeString, dateFormat, timeFormat);
                }
                catch (Exception)
                {
                    return ParseExact(datetimeString, dateFormat);
                }
            }
            return ParseExactGeneral(datetimeString, dateFormat, timeFormat);
        }

        private static DateTime ParseExactThangNgayNam(string dateTimeString)
        {
            try { return DateTime.ParseExact(dateTimeString, "MM/dd/yyyy", null); }
            catch (Exception ex) { throw ex; }
        }

        private static DateTime ParseExactNgayThangNam(string dateTimeString)
        {
            try { return DateTime.ParseExact(dateTimeString, "dd/MM/yyyy", null); }
            catch (Exception ex) { throw ex; }
        }

        private static DateTime ParseExactNamThangNgay(string dateTimeString)
        {
            try { return DateTime.ParseExact(dateTimeString, "yyyy/MM/dd", null); }
            catch (Exception ex) { throw ex; }
        }

        private static DateTime ParseExactNamThangNgaySql(string dateTimeString)
        {
            try { return DateTime.ParseExact(dateTimeString, "yyyy-MM-dd", null); }
            catch (Exception ex) { throw ex; }
        }

        private static DateTime ParseExactGeneral(string dateTimeString, DateFormat dateFormat, TimeFormat timeFormat)
        {
            var formatString = "";

            switch (dateFormat)
            {
                case DateFormat.ThangNgayNam:
                    formatString = "MM/dd/yyyy";
                    break;
                case DateFormat.NgayThangNam:
                    formatString = "dd/MM/yyyy";
                    break;
                case DateFormat.NamThangNgay:
                    formatString = "yyyy/MM/dd";
                    break;
                default:
                    throw new Exception("Not regconize date format");
            }

            switch (timeFormat)
            {
                case TimeFormat.HHmmssfff:
                    formatString += " HH:mm:ss.fff";
                    break;
                case TimeFormat.HHmmss:
                    formatString += " HH:mm:ss";
                    break;
                case TimeFormat.Hmmss:
                    formatString += " H:mm:ss";
                    break;
                case TimeFormat.Hmmssfff:
                    formatString += " H:mm:ss.fff";
                    break;
                default:
                    throw new Exception("Not regconize time format");
            }

            try { return DateTime.ParseExact(dateTimeString, formatString, null); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// TanPVD: 2015/01/07
        /// </summary>
        /// <param name="dt"></param>
        /// format datetime
        /// <returns></returns>
        public static string FormatDateTime(object dt)
        {
            if (dt == null)
                return string.Empty;
            var datetime = dt.ToString();
            TimeSpan dateBetween = DateTime.Now - DateTime.Parse(datetime);
            if (dateBetween.Days < 1 && dateBetween.Hours == 0 && dateBetween.Minutes == 0)
            {
                return string.Format("vài giây trước");
            }
            else if (dateBetween.Days < 1 && dateBetween.Hours == 0 && dateBetween.Minutes > 0)
            {
                return string.Format("{0} phút trước", dateBetween.Minutes);
            }
            else if (dateBetween.Days < 1)
            {
                return string.Format("{0} giờ trước", dateBetween.Hours);
            }
            return (DateTime.Parse(datetime)).ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 20/01/2015</para>
        /// chuyển giờ thành định dạng giờ-phút
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static string ConvertHouseToHouseMinute(double minute)
        {
            return (
                        minute >= 60 ? String.Format(@"{0} Giờ {1} Phút", Math.Floor(minute / 60), Math.Floor(minute % 60))
                        : (String.Format(@"{0} Phút", Math.Floor(minute % 60)))
                    );
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated:02/02/2015</para>
        /// <para>Description: lấy totalSeconds tính đến thời điểm hiện tại</para>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ConvertTimesToTotalSeconds(DateTime date)
        {
            var span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return Math.Round(span.TotalSeconds, 0);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated:02/02/2015</para>
        /// <para>Description: tính độ lệch thời gian so với hiện tại</para>
        /// </summary>
        /// <param name="stime"></param>
        /// <param name="eslapedMinutes"></param>
        /// <returns></returns>
        public static bool GetEslapedMinutes(double stime, ref double eslapedMinutes)
        {
            try
            {
                var timeZone = TimeZone.CurrentTimeZone;
                var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                var senderTotalSeconds = stime + timeZone.GetUtcOffset(DateTime.Now).TotalSeconds;
                var senderTotalSecondsTimeSpan = TimeSpan.FromSeconds(senderTotalSeconds);

                var receiverDiffTimeSpan = DateTime.UtcNow - origin;
                var tsResult = receiverDiffTimeSpan - senderTotalSecondsTimeSpan;
                eslapedMinutes = tsResult.TotalMinutes;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Extension

        /// <summary>
        /// Lay ngay dau tien trong tuan cua mot ngay bat ky
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDateOfWeek(this DateTime date)
        {
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            DayOfWeek dOfWeek = info.Calendar.GetDayOfWeek(date);
            var h = new Hashtable();
            h["Sunday"] = 6;
            h["Monday"] = 0;
            h["Tuesday"] = 1;
            h["Wednesday"] = 2;
            h["Thursday"] = 3;
            h["Friday"] = 4;
            h["Saturday"] = 5;
            double indexOfday = double.Parse(h[dOfWeek.ToString()].ToString());
            var tmpDate = date.AddDays(-indexOfday);
            return new DateTime(tmpDate.Year, tmpDate.Month, tmpDate.Day);
        }

        /// <summary>
        /// Lay ngay cuoi cung trong tuan cua mot ngay bat ky
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDateOfWeek(this DateTime date)
        {
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            DayOfWeek dOfWeek = info.Calendar.GetDayOfWeek(date);
            var h = new Hashtable();
            h["Sunday"] = 6;
            h["Monday"] = 0;
            h["Tuesday"] = 1;
            h["Wednesday"] = 2;
            h["Thursday"] = 3;
            h["Friday"] = 4;
            h["Saturday"] = 5;
            double indexOfday = double.Parse(h[dOfWeek.ToString()].ToString());
            var tmpDate = date.AddDays(6 - indexOfday);
            return new DateTime(tmpDate.Year, tmpDate.Month, tmpDate.Day, 23, 59, 59);
        }

        /// <summary>
        /// Ngay dau thang cua mot ngay bat ky
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return DateTime.Parse(date.Year + "-" + date.Month + "-01");
        }

        public static List<int> MonthOfYear()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        }

        /// <summary>
        /// Ngay cuoi thang cua mot ngay bat ky
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            //var tmpDate = DateTime.Parse(date.Year + "-" + date.Month + "-" + DateTime.DaysInMonth(date.Year, date.Month));
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0).AddMonths(1).AddMilliseconds(-3);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        public static DateTime EndOfDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 997);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/03/2015</para>
        /// <para>Lấy số tuần hiện tại theo DateTime</para>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetWeekNumber(this DateTime dt)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dt);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                dt = dt.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            //return weekNo;
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/03/2015</para>
        /// <para>Lấy danh sách số tuần theo năm</para>
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<Week> GetWeeksOfTheYear(this int year)
        {
            var firstDayOfYear = new DateTime(year, 1, 1);
            var beginningDayOfWeek = firstDayOfYear.AddDays(-1 * Convert.ToInt32(firstDayOfYear.DayOfWeek));
            var endingDayOfWeek = beginningDayOfWeek.AddDays(6);
            var weekOfYear = 1;
            var weeksOfTheYear = new List<Week>();

            while (beginningDayOfWeek.Year < year + 1)
            {
                var week = new Week { Number = weekOfYear, BeginningOfWeek = beginningDayOfWeek };
                weeksOfTheYear.Add(week);

                beginningDayOfWeek = beginningDayOfWeek.AddDays(7);
                weekOfYear++;
            }

            return weeksOfTheYear;
        }

        public class Week
        {
            public DateTime BeginningOfWeek { get; set; }
            public DateTime EndOfWeek { get { return this.BeginningOfWeek.AddDays(6); } }
            public int Number { get; set; }
            public string Text { get { return this.ToString(); } }

            public override string ToString()
            {
                return DateTime.Now > BeginningOfWeek && DateTime.Now < EndOfWeek
                    ? String.Format(
                        "Week {0} or current week: {1} - {2}",
                        this.Number,
                        this.BeginningOfWeek.ToShortDateString(),
                        this.EndOfWeek.ToShortDateString())
                    : String.Format(
                        "Week {0}: {1} - {2}",
                        this.Number,
                        this.BeginningOfWeek.ToShortDateString(),
                        this.EndOfWeek.ToShortDateString());
            }
        }

        public static long GetLongUnixTimeStamp(this DateTime date)
        {
            var span = (date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return Convert.ToInt64(Math.Round(span.TotalMilliseconds, 0));

            //var span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //return Convert.ToInt64(Math.Round(span.TotalSeconds, 0));
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {

            var jan1 = new DateTime(year, 1, 1);
            if (weekOfYear == 1)
                return jan1;
            CultureInfo _culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            CultureInfo _uiculture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();

            _culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            _uiculture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

            Thread.CurrentThread.CurrentCulture = _culture;
            Thread.CurrentThread.CurrentUICulture = _uiculture;
            var daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            var firstMonday = jan1.AddDays(daysOffset);

            var firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }

            return firstMonday.AddDays(weekOfYear * 7);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 10/04/2015</para>
        /// <para>Gets the 12:00:00 instance of a DateTime</para>
        /// </summary>
        public static DateTime GetBeginOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 10/04/2015</para>
        /// <para>Gets the 11:59:59 instance of a DateTime</para>
        /// </summary>
        public static DateTime GetEndOfDay(this DateTime dateTime)
        {
            return GetBeginOfDay(dateTime).AddDays(1).AddMilliseconds(-3);
        }

        public static DateTime GetBeginOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime GetEndOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddMilliseconds(-3);
        }

		public static DateTime GetBeginOfHour(this DateTime dateTime)
		{
			var hour = dateTime.Hour;
			return dateTime.Date.AddHours(hour);
		}

		public static DateTime GetEndOfHour(this DateTime dateTime)
		{
			var hour = dateTime.Hour;
			return dateTime.Date.AddHours(hour).AddMinutes(59).AddSeconds(59).AddMilliseconds(997);
		}
		#endregion

		public static string CountDownTime(this DateTime? date, string formatHours = "h", string formatMinute = "")
        {
            if (!date.HasValue)
                return "";
            TimeSpan remaindate = date.Value - DateTime.Now;
            if (remaindate.TotalHours < 24)
            {
                return string.Format("{0}{1} {2}{3}", remaindate.Hours, formatHours, remaindate.ToString("mm"), formatMinute); // time.ToString(@"hhhmm");
            }
            else
            {
                return Math.Floor(remaindate.TotalDays).ToString(CultureInfo.InvariantCulture) + " ngày";
            }
        }

        public static DateTime GetDate19700101
        {
            get { return DateTime.Parse("1970/01/01"); }
        }

        public static string GetVnDateTimeFormat(this DateTime? date)
        {
            return date.HasValue ? GetVnDateTimeFormat(date.Value) : string.Empty;
        }

        public static string GetVnDateTimeFormat(this DateTime date)
        {
            var formatString = DateFormat.NgayThangNam.Text() + " " + TimeFormat.HHmmss.Text();
            return date.ToString(formatString);
        }


        /// <summary>
        /// Lấy format ngày tháng
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetVnDateFormat(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Author: QuocTuan
        /// CreateDate: 27/03/2019
        /// Description: Format ngày theo Culture
        /// </summary>
        /// <param name="date"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string FormatDateCulture(this DateTime date, string culture = "")
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            if (!string.IsNullOrEmpty(culture))
            {
                currentCulture = new CultureInfo(culture, false);
            }

            return date.ToString(new CultureInfo(currentCulture.Name));
        }

        public static DateTime ConvertSeccondToDateTime(this double data)
        {
            var tempDate = DateTime.Now;
            try
            {
                tempDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(data);
            }
            catch (Exception)
            {
            }

            return tempDate;
        }

        public static DateTime ConvertMilliSecondToDateTime(this double data)
        {
            var tempDate = DateTime.Now;
            try
            {
                tempDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(data);
            }
            catch (Exception)
            {
            }

            return tempDate;
        }
        public static DateTime ConvertSeccondToDateTime(this int data)
        {
            var tempDate = DateTime.Now;
            try
            {
                tempDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(data);
            }
            catch (Exception)
            {
            }

            return tempDate;
        }
        public static DateTime ConvertSeccondToDateTime(this long data)
        {
            var tempDate = DateTime.Now;
            try
            {
                tempDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(data);
            }
            catch (Exception)
            {
            }

            return tempDate;
        }
        public static double ConvertDateTimeToSeccond(this DateTime data)
        {
            var seccond = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            try
            {
                seccond = (data - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            }
            catch (Exception)
            {

            }
            return seccond;
        }
        public static double ConvertDateTimeToSeccond(this DateTime? data)
        {
            DateTime datas = data ?? DateTime.Now;
            var seccond = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            try
            {
                seccond = (datas - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            }
            catch (Exception)
            {

            }
            return seccond;
        }

        public static string DurationBetweenDate(this DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom == null || dateTo == null) return string.Empty;

            var _timeSpan = (dateFrom < dateTo) ? (dateTo - dateFrom) : (dateFrom - dateTo);
            var _seconds = _timeSpan.TotalSeconds;

            return DurationBetweenDate((long)_seconds);
        }

        public static string DurationBetweenDate(this long dateDiffValue, bool allowSecond = false, string emptyValue = "")
        {
            if (dateDiffValue == 0) return emptyValue;

            int days = (int)Math.Floor((decimal)(dateDiffValue / (3600 * 24)));
            int hours = (int)Math.Floor((decimal)(dateDiffValue % (3600 * 24) / 3600));
            int mins = (int)Math.Floor((decimal)(dateDiffValue % 3600 / 60));

			var rs = string.Format("{0} ngày {1} giờ {2} phút", days, hours.ToString("00"), mins.ToString("00"));

			if (allowSecond)
			{
				if (dateDiffValue >= 60)
				{
					var seconds = (int)Math.Floor((decimal)(dateDiffValue % 60));
					rs += string.Format(" {0} giây", seconds.ToString("00"));
				}
				else
				{
					rs = string.Format("{0} giây", dateDiffValue.ToString("00"));
				}
			}

            return rs;
        }
        public static string ToDateTimeDurationInt(this int seconds, DateTimeDurationFormat format = DateTimeDurationFormat.Ngay_HHmmss)
        {
            var t = TimeSpan.FromSeconds(seconds);

            var _result = string.Empty;

            var d = t.Days > 0 ? t.Days.ToString() + "d" : string.Empty;
            _result += d;

            var h = t.Hours.ToString("00");
            var m = t.Minutes.ToString("00");
            var s = t.Seconds.ToString("00");



            switch (format)
            {
                case DateTimeDurationFormat.HHmm:
                    return string.Format(DateTimeDurationFormat.HHmm.Text(), h, m);

                case DateTimeDurationFormat.HHmmss:
                    return string.Format(DateTimeDurationFormat.HHmm.Text(), h, m, s);

                case DateTimeDurationFormat.Ngay_HHmm:
                    return string.Format(DateTimeDurationFormat.HHmm.Text(), d, h, m);

                case DateTimeDurationFormat.Ngay_HHmmss:
                default:
                    return string.Format(DateTimeDurationFormat.Ngay_HHmmss.Text(), d, h, m, s);
            }
        }

        public static string ToDateTimeDuration(this long seconds, DateTimeDurationFormat format = DateTimeDurationFormat.Ngay_HHmmss)
        {
            var t = TimeSpan.FromSeconds(seconds);

            var _result = string.Empty;

            var d = t.Days > 0 ? t.Days.ToString() + "d" : string.Empty;
            _result += d;

            var h = t.Hours.ToString("00");
            var m = t.Minutes.ToString("00");
            var s = t.Seconds.ToString("00");

            switch (format)
            {
                case DateTimeDurationFormat.HHmm:
                    return string.Format(DateTimeDurationFormat.HHmm.Text(), h, m);

                case DateTimeDurationFormat.HHmmss:
                    return string.Format(DateTimeDurationFormat.HHmm.Text(), h, m, s);

                case DateTimeDurationFormat.Ngay_HHmm:
                    return string.Format(DateTimeDurationFormat.HHmm.Text(), d, h, m);

                case DateTimeDurationFormat.Ngay_HHmmss:
                default:
                    return string.Format(DateTimeDurationFormat.Ngay_HHmmss.Text(), d, h, m, s);
            }
        }
    }
}
