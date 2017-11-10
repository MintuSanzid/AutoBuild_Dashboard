using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using DashboardHR.Models.Models;
using Dashboard_HR.Data.Handler;

namespace Dashboard_WebApp.Controllers
{
    //[Authorize]
    public class ConfigurationController : Controller
    {
        private DashboardHandler _aDashboardHandler;

        // GET: Configuration/DashboardHRF
        public ActionResult DashboardHrf(string userCode)
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

        // GET: Configuration/DashboardCompanyJsonData
        public JsonResult DashboardCompanyJsonData(BusinessInfo aInfo)
        {
            DataTable data = null; 
            try
            {
                var userId = Session["UserId"].ToString();
                if (true)
                {
                    _aDashboardHandler = new DashboardHandler();
                    data = _aDashboardHandler.GetMpCompanies(userId, aInfo.EmployeeType);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Configuration/DashboardDivisionJsonData
        public JsonResult DashboardDivisionJsonData(BusinessInfo aInfo)
        {
            var userId = Session["UserId"].ToString();
            if (true)
            {
                _aDashboardHandler = new DashboardHandler();
                var data = _aDashboardHandler.GetMpDivisions(userId, aInfo.EmployeeType);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Configuration/DashboardUnitJsonData
        public JsonResult DashboardUnitJsonData(BusinessInfo aInfo)
        {
            var userId = Session["UserId"].ToString();
            if (true)
            {
                _aDashboardHandler = new DashboardHandler();
                var data = _aDashboardHandler.GetMpUnits(userId, aInfo.EmployeeType);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Configuration/DashboardHRFJson
        public JsonResult DashboardHrfDepartmentJson(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpDepartments(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: Configuration/DashboardHrfSectionJson
        public JsonResult DashboardHrfSectionJson(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DashboardHrfSubSectionJson(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpSubSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DashboardAllocatedEmpList(CompanyObj companyObj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetHrAllocatedEmpList(companyObj);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult DashboardExcessEmpList(string companyCode)
        //{
        //    _aDashboardHandler = new DashboardHandler();
        //    var data = _aDashboardHandler.GetHrExcessEmpList(companyCode);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult DashboardUnallocatedEmpList(string companyCode)
        //{
        //    _aDashboardHandler = new DashboardHandler();
        //    var data = _aDashboardHandler.GetHrUnallocatedEmpList(companyCode);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

    }
}
