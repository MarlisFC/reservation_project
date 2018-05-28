using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RES.Web.Site.Extensions;
using System.Threading;
using System.Globalization;

namespace RES.Web.Site.Controllers
{
    public class CultureController : BaseController
    {
        // GET: Culture
        public ActionResult ChangeLanguage(string language)
        {
            var culture = language == "en" ? "en-US" : "es-ES";
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Session["CurrentLanguage"] = language;
            var oldLanguage = language == "en" ? "es" : "en";

            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
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

            var url = Request.UrlReferrer?.AbsoluteUri ?? "";
            return !string.IsNullOrEmpty(url) ? (ActionResult)Redirect(url) : RedirectToAction("ListReservations", "Reservation");
        }
    }
}