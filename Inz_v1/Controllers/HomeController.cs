using BusinessLogic.Api;
using BusinessLogic.Api.Interface;
using Inz_v1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inz_v1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewLands()
        {
            ViewBag.Message = "Przeglądaj działki";
            List<LandOfferModel> list = new List<LandOfferModel>();
            IProjectApi api = new ProjectApi();
            var dataResponse = api.GetLandOffers();
            if (!dataResponse.Success)
            {
                Trace.TraceWarning(dataResponse.Errors);
            }

            return View();
        }

        public ActionResult PriceYourLand()
        {
            ViewBag.Message = "Wyceń swoją działkę";

            return View();
        }
    }
}