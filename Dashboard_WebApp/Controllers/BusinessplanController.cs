using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DashboardHR.Models.Models;
using Dashboard_HR.Data.Handler;
using Dashboard_HR.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dashboard_WebApp.Controllers
{
    public class BusinessplanController : Controller
    {
        private DashboardHandler _aDashboardHandler;
        private BusinessplanHandler _aBusinessplanHandler;

        // GET: Configuration/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: Configuration/BusinessPlanJsonData 
        public JsonResult DashboardFindByCompanyJsonData(BusinessInfo aBusinessInfo)
        {
            List<BusinessplanModel> data = null;
            try
            {
                if (aBusinessInfo == null) return Json(null, JsonRequestBehavior.AllowGet);
                _aBusinessplanHandler = new BusinessplanHandler();
                data = _aBusinessplanHandler.GetDashboardFindByCompanyData(aBusinessInfo);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DashboardBpCapacityJson(BusinessInfo aBusinessInfo)
        {
            List<BusinessplanModel> data = null;
            try
            {
                if (aBusinessInfo == null) return Json(null, JsonRequestBehavior.AllowGet);
                var aBusinessplanHandler = new BusinessplanHandler();
                data = aBusinessplanHandler.GetBpCapacityData(aBusinessInfo);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        /////////// Business Plan Line KPI ///////////
        public JsonResult DashboardJsonData()
        {
            Businessplan data = null;
            try
            {
                if (true)
                {
                    _aBusinessplanHandler = new BusinessplanHandler();
                    data = _aBusinessplanHandler.GetBusinessplanData();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LineKpi(string userCode)
        {
            try
            {

                _aDashboardHandler = new DashboardHandler();
                var email = _aDashboardHandler.GetDecriptionUserCode(userCode);
                if (userCode != null && email == null)
                {
                    return RedirectToAction("Register", "Account");
                }
                Session["UserId"] = email;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public string GetLineKpiJonData(BusinessInfo aInfo)
        {
            _aBusinessplanHandler = new BusinessplanHandler();
            var dataSet = _aBusinessplanHandler.GetLineKpiDataList(aInfo);
            return JsonConvert.SerializeObject(dataSet);
        }

        /////////// End of Business Plan Line KPI ///////////
    }
}
