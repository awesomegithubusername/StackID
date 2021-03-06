﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenIdProvider.Helpers;
using OpenIdProvider;

namespace OpenIdProvider.Controllers
{
    public class HomeController : ControllerBase
    {
        [Route("favicon.ico")]
        public ActionResult Favicon()
        {
            return RedirectPermanent("/Content/img/favicon.ico");
        }

        [Route("404")]
        public ActionResult NotFoundLanding()
        {
            return NotFound();
        }

        [Route("error")]
        public ActionResult Error()
        {
            return IrrecoverableError("An error occurred on the server", "This event has been recorded.");
        }

        [Route("error-with-help")]
        public ActionResult ErrorWithHelp()
        {
            return IrrecoverableErrorWithHelp("An error occurred on the server", "This event has been recorded.");
        }

        [Route("proxy-help")]
        public ActionResult ProxyHelp()
        {
            return View();
        }

        [Route("")]
        public ActionResult Index()
        {
            var accepts = Request.AcceptTypes;

            if (accepts != null && accepts.Contains("application/xrds+xml"))
            {
                ViewData["OPIdentifier"] = true;
                return View("Xrds", null);
            }

            // Don't make logged in users waste a click
            if (Current.LoggedInUser != null) return SafeRedirect((Func<ActionResult>)(new UserController()).ViewUser);

            return View();
        }

        [Route("xrds")]
        public ActionResult Xrds()
        {
            ViewData["OPIdentifier"] = true;
            return View();
        }
    }
}
