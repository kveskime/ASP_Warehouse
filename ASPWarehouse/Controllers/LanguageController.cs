﻿using System;
using System.Web;
using System.Web.Mvc;
using ASPWarehouse.Helpers;

namespace ASPWarehouse.Controllers
{
    public class LanguageController : BaseController
    {
        // GET: Language
        public ActionResult SetLanguage(string culture, string returnUrl)
        {
            // Validate input
            culture = CultureHelper.GetImplementedUICulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture; // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}