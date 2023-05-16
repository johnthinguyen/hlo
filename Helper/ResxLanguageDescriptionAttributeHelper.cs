using ResxLanguagesUtility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxLanguagesUtility.Helper
{
    /// <summary>
    /// Author: QuocTuan
    /// CreateDate: 2019-04-19
    /// Description: Attribute đa ngôn ngữ description cho enum
    /// </summary>
    public class ResxLanguageDescriptionAttributeHelper : DescriptionAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        public ResxLanguageDescriptionAttributeHelper(string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
        }

        public override string Description
        {
            get
            {
                try
                {
                    return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
                }
                catch (Exception ex)
                {
                    Logger.CommonLogger.DefaultLogger.Error("BoHelper -- ResxLanguageDescriptionAttributeHelper -- ex", ex);
                    return resourceKeyName;
                }
            }
        }
    }
}
