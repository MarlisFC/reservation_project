using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RES.Web.Site.Extensions;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RES.Web.Site.Controllers
{
    public class CultureController : BaseController
    {
        public ActionResult ChangeLanguage(string culture, string returnUrl)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            Regex re = new Regex("^/\\w{2,3}(-\\w{2})?");
            returnUrl = re.Replace(returnUrl, "/" + culture.ToLower());
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return  Redirect(returnUrl);
        }
    }
}