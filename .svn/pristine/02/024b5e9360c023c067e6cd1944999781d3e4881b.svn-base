/**********************************************************************
 * Author: ThongNT
 * DateCreate: 06-25-2014 
 * Description: CommonLogger   
 * ####################################################################
 * Author:......................
 * DateModify: .................
 * Description: ................
 * 
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyUtility.Extensions
{
    public class FormatNumberRound
    {
        public int ByteRound { get; set; }
        public int SByteRound { get; set; }
        public int UInt16Round { get; set; }
        public int UInt32Round { get; set; }
        public int UInt64Round { get; set; }
        public int Int16Round { get; set; }
        public int Int32Round { get; set; }
        public int Int64Round { get; set; }
        public int DecimalRound { get; set; }
        public int DoubleRound { get; set; }
        public int SingleRound { get; set; }

        public FormatNumberRound()
        {
            Int16Round = 0;
            Int32Round = 0;
            Int64Round = 0;
            DecimalRound = 2;
            DoubleRound = 4;
        }
    }

    public static class NumberExtension
    {

//        public static string ToCurrencyString(this int number, string unit = "")
//        {
//            return ConvertUtility.FormatCurrency(number, unit);
//        }

        public static string ToCurrencyString(this decimal number, bool enableShorten = false, bool showUnit = true, bool enableRound = true)
        {
            var unit = "đ";
            var format = "N00";

            if (enableRound == false)
            {
                if (showUnit)
                {
                    return number.ToString("#,##0.00") + unit;
                }
                return number.ToString("#,##0.00");
            }

            if (enableShorten)
            {
                if (number >= 1000000)
                {
                    number = number / (decimal)1000000;
                    unit = "tr";
                    format = "N01";
                }
                else if (number >= 1000)
                {
                    number = number / (decimal)1000;
                    unit = "k";
                    format = "N00";
                }
            }
            
            var currency = number.ToString(format, new CultureInfo("vi-VN"));
          
            return showUnit == false ? currency : string.Format("{0}{1}", currency, unit);
        }


        public static string GetPlatformIdName(this int? groupType)
        {
            if (groupType == null)
                return "";
            switch (groupType)
            {
                case 2:
                    return "Android";
                case 3:
                    return "Ios";
                case 4:
                    return "WebApp";
                default:
                    return "Web";
            }
        }


        public static string ToCurrencyString(this decimal? number, bool enableShorten = false, bool showUnit = true, bool enableRound = true)
        {
            return ToCurrencyString(number.GetValueOrDefault(), enableShorten, showUnit, enableRound);
        }

        public static string ToCurrencyString(this int number, bool enableShorten = false, bool showUnit = true)
        {
            return ToCurrencyString((decimal)number, enableShorten, showUnit);
        }

        public static string ToCurrencyString(this long number, bool enableShorten = false, bool showUnit = true)
        {
            return ToCurrencyString((decimal)number, enableShorten, showUnit);
        }
        
        public static string ToCurrencyString(this int? number, bool enableShorten = false, bool showUnit = true)
        {
            return ToCurrencyString((decimal)number.GetValueOrDefault(0), enableShorten, showUnit);
        }

        public static string ToCurrencyString(this long? number, bool enableShorten = false, bool showUnit = true)
        {
            return ToCurrencyString((decimal)number.GetValueOrDefault(0), enableShorten, showUnit);
        }

        /// <summary>
        /// format tiền tệ xu ThinhQHT
        /// </summary>
        /// <param name="number"></param>
        /// <param name="enableShorten"> </param>
        /// <param name="showUnit">= false để ko có chữ VND</param>
        /// <param name="enableRound"> = true để ko  có .00</param>
        /// <returns></returns>
        public static string ToCurrencyStringXu(this decimal number, bool enableShorten = false, bool showUnit = true, bool enableRound = true)
        {
            var unit = "";
            var format = "N00";

            if (enableRound == false)
            {
                if (showUnit)
                {
                    return number.ToString("#,##0.00") + unit;
                }
                return number.ToString("#,##0.00");
            }

            if (enableShorten)
            {
                if (number >= 1000000)
                {
                    number = number / (decimal)1000000;
                    unit = "tr";
                    format = "N01";
                }
                else if (number >= 1000)
                {
                    number = number / (decimal)1000;
                    unit = "k";
                    format = "N00";
                }
            }

            var currency = number.ToString(format, new CultureInfo("vi-VN"));

            return showUnit == false ? currency : string.Format("{0}{1}", currency, unit);
        }
        public static string ToCurrencyStringXu(this decimal? number, bool enableShorten = false, bool showUnit = true, bool enableRound = true)
        {
            return ToCurrencyStringXu(number.GetValueOrDefault(), enableShorten, showUnit, enableRound);
        }

        public static string ToCurrencyStringXu(this int number, bool enableShorten = false, bool showUnit = true)
        {
            return ToCurrencyStringXu((decimal)number, enableShorten, showUnit);
        }

        public static string ToCurrencyStringXu(this long number, bool enableShorten = false, bool showUnit = true)
        {
            return ToCurrencyStringXu((decimal)number, enableShorten, showUnit);
        }

        public static string ToCurrencyStringXu(this int? number, bool enableShorten = false, bool showUnit = true)
        {
            return ToCurrencyStringXu((decimal)number.GetValueOrDefault(0), enableShorten, showUnit);
        }

        public static string ToCurrencyStringXu(this long? number, bool enableShorten = false, bool showUnit = true)
        {
            return ToCurrencyStringXu((decimal)number.GetValueOrDefault(0), enableShorten, showUnit);
        }

        #region format theo culture

        public static string FormatCoinCultureUs(this decimal myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("en-us"), format, myCoin).Replace("$", "");
        }

        public static string FormatCoinCultureVN(this int myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("vi-VN"), format, myCoin).Replace("₫", "");
        }

        public static string FormatCoinCultureVN(this double myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("vi-VN"), format, myCoin).Replace("₫", "");
        }

        public static string FormatCoinCultureVN(this decimal myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("vi-VN"), format, myCoin).Replace("₫", "");
        }
        public static string FormatCoinCultureVN(this float myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("vi-vn"), format, myCoin).Replace("₫", "");
        }
        public static string FormatCoinCultureVN(this long myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("vi-vn"), format, myCoin).Replace("₫", "");
        }
        public static string FormatCoinCultureUs(this float myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("en-us"), format, myCoin).Replace("$", "");
        }

        /// <summary>
        /// <para>-1: sẽ có 2 số sau dấu .</para>
        /// <para>0: sẽ ko có số sau dấu .</para>
        /// <para>>0: sẽ có >0 số sau dấu .</para>
        /// </summary>
        /// <param name="myCoin"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public static string FormatCoinCultureUs(this int myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("en-us"), format, myCoin).Replace("$", "");
        }

        public static string FormatCoinCultureUs(this double myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("en-us"), format, myCoin).Replace("$", "");
        }
        public static string FormatCoinCultureUs(this long myCoin, int round = -1)
        {
            string format = "{0:C}";

            if (round >= 0)
                format = "{0:C" + round + "}";

            return string.Format(CultureInfo.GetCultureInfo("en-us"), format, myCoin).Replace("$", "");
        }
        #endregion

        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsBoolean(this object o)
        {
            return (Type.GetTypeCode(o.GetType()) == TypeCode.Boolean);
        }

        public static string FormatToString(this object myCoin, FormatNumberRound roundRule = null)
        {
            if (myCoin.IsNumericType())
            {
                if (roundRule == null) roundRule = new FormatNumberRound();

                var _round = -1;
                switch (Type.GetTypeCode(myCoin.GetType()))
                {
                    case TypeCode.Int32:
                        _round = roundRule.Int32Round;
                        break;
                    case TypeCode.Int64:
                        _round = roundRule.Int64Round;
                        break;
                    case TypeCode.Decimal:
                        _round = roundRule.DecimalRound;
                        break;
                    case TypeCode.Double:
                        _round = roundRule.DoubleRound;
                        break;
                }

                string format = "{0:C}";
                if (_round >= 0)
                    format = "{0:C" + _round + "}";

                return string.Format(CultureInfo.GetCultureInfo("vi-vn"), format, myCoin).Replace("₫", "");
            }
            else
            {
                return myCoin.ToString();
            }
        }

        public static object SumList(this List<object> data)
        {
            object _result = null;
            if(data != null && data.Any())
            {
                switch (Type.GetTypeCode(data[0].GetType()))
                {
                    case TypeCode.Int32:
                        _result = data.Sum(item => (int)item);
                        break;
                    case TypeCode.Int64:
                        _result = data.Sum(item => (long)item);
                        break;
                    case TypeCode.Decimal:
                        _result = data.Sum(item => (decimal)item);
                        break;
                    case TypeCode.Double:
                        _result = data.Sum(item => (double)item);
                        break;
                }
            }

            return _result;
        }
    }
}
