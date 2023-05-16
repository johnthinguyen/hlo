using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;

namespace WebUtility
{
    public static class WebExtension
    {
        /// <summary>
        /// Clones an <see cref="HttpWebRequest" /> in order to send it again.
        /// </summary>
        /// <param name="message">The message to set headers on.</param>
        public static HttpRequestBase ConvertRequest(HttpRequestMessage message)
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            return context.Request;
        }

        #region Post data

        public static string GetValue(this NameValueCollection postDataRequest, string name)
        {
            var values = postDataRequest.GetValues(name);
            return values != null ? values.FirstOrDefault() : string.Empty;
        }

        public static int GetIntValue(this NameValueCollection postDataRequest, string name)
        {
            var value = GetValue(postDataRequest, name);
            int result;
            int.TryParse(value.Trim(), out result);
            return result;
        }

        #endregion
    }
}
