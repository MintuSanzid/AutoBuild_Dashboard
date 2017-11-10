using System.Collections.Generic;
using System.Web.Mvc;
using DashboardHR.Models.Models;

namespace Dashboard_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult JQueryTable()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View("Index");
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

        public ActionResult Barchart()
        {
            return View();
        }

        public ActionResult Pasing()
        {
            return View();
        }

        public JsonResult GetUnitTestCode()
        {
            var aCompanies = new List<Company>
            {
                new Company { CompanyCode = "BTP",
                CompanyId = 01,
                CompanyName = "Bitopi"},
                new Company { CompanyCode = "BTP",
                CompanyId = 02,
                CompanyName = "Bitopi" }
            };

            return Json(aCompanies, JsonRequestBehavior.AllowGet);
        }
    }
}