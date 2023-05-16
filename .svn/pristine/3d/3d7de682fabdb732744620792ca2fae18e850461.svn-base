using ResxLanguagesUtility.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Logger;
using MyConfig;

namespace ResxLanguagesUtility
{
    public class ResxLanguages
    {
        /// <summary>
        /// Author: QuocTuan
        /// CreateDate: 03/04/2019
        /// Description: Lấy ngôn ngữ hiện tại của user => Thứ tự ưu tiên Cookie, culture, myconfig
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentLanguage
        {
            get
            {
                return CurrentLanguage();
            }
        }

        /// <summary>
        /// Author: QuocTuan
        /// CreateDate: 03/04/2019
        /// Description: Lấy giá trị trong file xml với language keyName
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="localLanguagesEnum"></param>
        /// <returns></returns>
        public static string GetText(string keyName, ResxLanguagesEnum resxLanguagesEnum)
        {
            string text = string.Empty;

            if (MyConfiguration.DefaultLanguagesUtility.IsShowKeyNotFound)
                text = keyName;

            try
            {
                var fullFilepath = GetFilePath(resxLanguagesEnum);
                XElement xelement = XElement.Load(fullFilepath);

                // Lấy element với key
                var elementData = (from xData in xelement.Elements("data")
                                   where (string)xData.Attribute("name") == keyName
                                   select xData).FirstOrDefault();

                if (elementData == null)
                {
                    CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.GetText] - Chua dinh nghia keyName: {0} vao file {1}", keyName, fullFilepath);
                    return text;
                }

                // Lấy giá trị của element "value" trong element "data"
                try
                {
                    text = elementData.Element("value").Value;
                }
                catch (Exception ex)
                {
                    CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.GetText] - Chua dinh nghia gia tri cho keyName: {0} trong file {1}", keyName, fullFilepath);
                    return text;
                }

            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.GetText] - {0}", ex);
            }

            return text;
        }

        /// <summary>
        /// Author: QuocTuan
        /// CreateDate: 17/05/2019
        /// Description: Lấy giá trị trong file xml với language tùy chọn keyName
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="resxLanguagesEnum"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string GetText(string keyName, ResxLanguagesEnum resxLanguagesEnum, string language)
        {
            string text = string.Empty;

            if (MyConfiguration.DefaultLanguagesUtility.IsShowKeyNotFound)
                text = keyName;

            try
            {
                var fullFilepath = GetFilePath(resxLanguagesEnum, language);
                XElement xelement = XElement.Load(fullFilepath);

                // Lấy element với key
                var elementData = (from xData in xelement.Elements("data")
                                   where (string)xData.Attribute("name") == keyName
                                   select xData).FirstOrDefault();

                if (elementData == null)
                {
                    CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.GetText] - Chua dinh nghia keyName: {0} vao file {1}", keyName, fullFilepath);
                    return text;
                }

                // Lấy giá trị của element "value" trong element "data"
                try
                {
                    text = elementData.Element("value").Value;
                }
                catch (Exception ex)
                {
                    CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.GetText] - Chua dinh nghia gia tri cho keyName: {0} trong file {1}", keyName, fullFilepath);
                    return text;
                }

            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.GetText] - {0}", ex);
            }

            return text;
        }

        /// <summary>
        /// Author: QuocTuan
        /// CreateDate: 03/04/2019
        /// Description: Lấy keyName theo 1 giá trị văn bản
        /// </summary>
        /// <returns></returns>
        public static string GetKey(string textFind, ResxLanguagesEnum resxLanguagesEnum)
        {
            string text = string.Empty;

            if (MyConfiguration.DefaultLanguagesUtility.IsShowKeyNotFound)
                text = textFind;
            try
            {
                var fullFilepath = GetFilePath(resxLanguagesEnum);
                XElement xelement = XElement.Load(fullFilepath);

                // Lấy element với key
                var elementData = (from xData in xelement.Elements("data")
                                   where (string)xData.Element("value").Value == textFind
                                   select xData).FirstOrDefault();

                if (elementData == null)
                {
                    CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.GetKey] - Không tìm thấy keyName với text : {0} trong file {1}", textFind, fullFilepath);
                    return text;
                }

                // Lấy giá trị của element "value" trong element "data"
                text = elementData.Attribute("name").Value;
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.GetKey] - {0}", ex);
            }

            return text;
        }

        public static void SetCurrentLanguages(string language)
        {
            try
            {
                if (string.IsNullOrEmpty(language) || !DoesCultureExist(language))
                    language = MyConfiguration.Default.DefaultLanguage;

                // Set Language cookie
                WebUtility.CookieModel model = new WebUtility.CookieModel("", MyConfiguration.Default.DefaultLanguageCookieName, language);
                WebUtility.MyCookieManager.Set(model);

                // Set language culture
                CultureInfo myCultureInfo = new CultureInfo(language);

                Thread.CurrentThread.CurrentUICulture = myCultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(myCultureInfo.Name);
            }
            catch (Exception ex)
            {                
                CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.SetCurrentLanguages] - {0}", ex);                
            }
        }

        /// <summary>
        /// Author: QuocTuan
        /// CreateDate: 03/04/2019
        /// Description: Hỗ trợ lấy ngôn ngữ hiện tại của user => Thứ tự ưu tiên Cookie, culture, myconfig
        /// </summary>
        /// <returns></returns>
        private static string CurrentLanguage()
        {
            string lang = string.Empty;

            try
            {
                // B1: Ưu tiên lấy ngôn ngữ từ cookie trước
                lang = WebUtility.MyCookieManager.Get(MyConfiguration.Default.DefaultLanguageCookieName);
                // B2: Cookie không có thì lấy theo Culture hiện tại
                if (string.IsNullOrEmpty(lang))
                {

                    CultureInfo CurrentCulture = Thread.CurrentThread.CurrentCulture;
                    lang = CurrentCulture.TwoLetterISOLanguageName;
                }

                // B3: Culture không có thì lấy theo MyConfig
                if (string.IsNullOrEmpty(lang))
                {
                    lang = MyConfiguration.Default.DefaultLanguage;
                }

            }
            catch (Exception ex)
            {
                lang = MyConfiguration.Default.DefaultLanguage;
                CommonLogger.DefaultLogger.ErrorFormat("[LanguagesUtility.Languages.CurrentLanguage] - {0}", ex);
            }
            return lang;
        }
        private static string GetFileName(ResxLanguagesEnum _enum, string language)
        {
            string fileName = string.Empty;
            switch (_enum)
            {
                case ResxLanguagesEnum.Account:
                    fileName = "Account\\Account." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Cashout:
                    fileName = "Cashout\\Cashout." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Common:
                    fileName = "Common\\Common." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Event:
                    fileName = "Event\\Event." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Game:
                    fileName = "Game\\Game." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Guild:
                    fileName = "Guild\\Guild." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Home:
                    fileName = "Home\\Home." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Payment:
                    fileName = "Payment\\Payment." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Reason:
                    fileName = "Reason\\Reason." + language + ".xml";
                    break;
                case ResxLanguagesEnum.Other:
                    fileName = "Other\\Other." + language + ".xml";
                    break;
                case ResxLanguagesEnum.BO:
                    fileName = "BO\\BO." + language + ".xml";
                    break;
                default:
                    break;
            }

            return fileName;
        }

        /// <summary>
        /// Author: QuocTuan
        /// CreateDate: 23/05/2019
        /// Description: Lấy full đường dẫn chứa ngôn ngữ
        /// </summary>
        /// <returns></returns>
        private static string GetFilePath(ResxLanguagesEnum resxLanguagesEnum)
        {
            try
            {
                // Lấy ngôn ngữ hiện tại
                string language = GetCurrentLanguage;
                string fileName = GetFileName(resxLanguagesEnum, language);
                string dicrectory = HttpContext.Current.Server.MapPath("~/" + MyConfiguration.DefaultLanguagesUtility.DefaultDirectory);                

                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["RESX_LANGUAGE_PATH"]))
                    dicrectory = System.Configuration.ConfigurationManager.AppSettings["RESX_LANGUAGE_PATH"];

                return Path.Combine(dicrectory, fileName);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("[GetRootPath] ex " + ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Author: QuocTuan
        /// CreateDate: 23/05/2019
        /// Description: Lấy full đường dẫn chứa ngôn ngữ tùy chọn
        /// </summary>
        /// <returns></returns>
        private static string GetFilePath(ResxLanguagesEnum resxLanguagesEnum, string language)
        {
            try
            {
                if (string.IsNullOrEmpty(language))
                {
                    language = MyConfiguration.Default.DefaultLanguage;
                }

                string fileName = GetFileName(resxLanguagesEnum, language);
                string dicrectory = HttpContext.Current.Server.MapPath("~/" + MyConfiguration.DefaultLanguagesUtility.DefaultDirectory);

                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["RESX_LANGUAGE_PATH"]))
                    dicrectory = System.Configuration.ConfigurationManager.AppSettings["RESX_LANGUAGE_PATH"];

                return Path.Combine(dicrectory, fileName);
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("[GetRootPath] ex " + ex);
                return string.Empty;
            }
        }

        private static bool DoesCultureExist(string cultureName)
        {
            return System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures).Any(culture => string.Equals(culture.Name, cultureName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
