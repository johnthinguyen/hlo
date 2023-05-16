/// <summary>
/// Author: QuocTuan
/// CreateDate: 2019-04-25
/// Description: Custom Attribute validate
/// </summary>

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResxLanguagesUtility.Enums;

namespace ResxLanguagesUtility.Helper
{
    public class RxLangRequiredAttribute : RequiredAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        public RxLangRequiredAttribute(string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
            : base()
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
        }
        public override string FormatErrorMessage(string name)
        {
            return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
        }
    }

    public class RxLangRegularExpressionAttribute : RegularExpressionAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        string pattern;
        public RxLangRegularExpressionAttribute(string _pattern, string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
            : base(_pattern)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
            pattern = _pattern;
        }

        public override string FormatErrorMessage(string name)
        {
            return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
        }
    }

    public class RxLangStringLengthAttribute : StringLengthAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        int maximumLength;
        public RxLangStringLengthAttribute(int _maximumLength, string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
            : base(_maximumLength)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
            maximumLength = _maximumLength;
        }

        public int RxLangMaximumLength {
            get {
                return MaximumLength;
            }
        }
        
        public int RxLangMinimumLength {
            get {
                return MinimumLength;
            }
            set {
                MinimumLength = value;
            }
        }
        public override string FormatErrorMessage(string name)
        {
            return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
        }
    }

    public class RxLangRemoteAttribute : RemoteAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        string action, controller;
        public RxLangRemoteAttribute(string _action, string _controller, string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
            : base(_action, _controller)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
            action = _action;
            controller = _controller;
        }
        public string RxLangHttpMethod {
            get
            {
                return HttpMethod;
            }
            set
            {
                HttpMethod = value;
            }
        }
        public string RxLangAdditionalFields {
            get
            {
                return AdditionalFields;
            }
            set
            {
                AdditionalFields = value;
            }
        }
        public override string FormatErrorMessage(string name)
        {
            return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
        }
    }

    public class RxLangRangeAttribute: RangeAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        object miniMum, maxiMum;

        public RxLangRangeAttribute(int _minimum, int _maximum, string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum) 
            : base(_minimum, _maximum)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
            miniMum = _minimum;
            maxiMum = _maximum;
        }

        public RxLangRangeAttribute(double _minimum, double _maximum, string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
            : base(_minimum, _maximum)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
            miniMum = _minimum;
            maxiMum = _maximum;
        }

        public object RxLangMinimum { get { return Minimum; } }
        public object RxLangMaximum { get { return Maximum; } }

        public override string FormatErrorMessage(string name)
        {
            return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
        }
    }

    public class RxLangMaxLengthAttribute : MaxLengthAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        int length;
        public RxLangMaxLengthAttribute(string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
            : base()
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
        }
        public RxLangMaxLengthAttribute(int _length, string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum) 
            : base(_length)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
            length = _length;
        }

        public override string FormatErrorMessage(string name)
        {
            return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
        }
    }

    public class RxLangMinLengthAttribute : MinLengthAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        int length;
        public RxLangMinLengthAttribute(int _length, string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
            : base(_length)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
            length = _length;
        }

        public override string FormatErrorMessage(string name)
        {
            return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
        }
    }

    public class RxLangCompareAttribute : System.ComponentModel.DataAnnotations.CompareAttribute
    {
        string resourceKeyName;
        ResxLanguagesEnum resxLanguagesEnum;
        string otherProperty;
        public RxLangCompareAttribute(string _otherProperty, string _resourceKeyName, ResxLanguagesEnum _resxLanguagesEnum)
            : base(_otherProperty)
        {
            resourceKeyName = _resourceKeyName;
            resxLanguagesEnum = _resxLanguagesEnum;
            otherProperty = _otherProperty;
        }
        public override string FormatErrorMessage(string name)
        {
            return ResxLanguages.GetText(resourceKeyName, resxLanguagesEnum);
        }
    }

}