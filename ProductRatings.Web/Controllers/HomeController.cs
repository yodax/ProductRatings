using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductRatings.Persistence;

namespace ProductRatings.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPersistenceBackend _persistenceBackend;

        public HomeController(IPersistenceBackend persistence)
        {
            _persistenceBackend = persistence;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}