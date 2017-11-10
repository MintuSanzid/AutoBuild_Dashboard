using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashboardHR.Models.Models;
using Dashboard_HR.Data.Handler;
using Newtonsoft.Json;

namespace Dashboard_WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private DashboardHandler _aDashboardHandler;

        // GET: Dashboard/MpBudgetOnroll
        public ActionResult MpBudgetOnroll(string userCode)
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
        public ActionResult MpBudgetUserDefine(string userCode)
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
        public ActionResult MpUserDefineOnroll(string userCode)
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

        // GET: Dashboard/DashboardCompany
        public string DashboardCompany(BusinessInfo aInfo)
        {
            try
            {
                var userId = Session["UserId"].ToString();
                if (true)
                {
                    _aDashboardHandler = new DashboardHandler();
                    var data = _aDashboardHandler.GetMpCompanies(userId, aInfo.EmployeeType);
                    return JsonConvert.SerializeObject(data);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DashboardDivision(BusinessInfo aInfo)
        {
            var userId = Session["UserId"].ToString();
            if (true)
            {
                _aDashboardHandler = new DashboardHandler();
                var data = _aDashboardHandler.GetMpDivisions(userId, aInfo.EmployeeType);
                return JsonConvert.SerializeObject(data);
            }
        }
        public string DashboardUnit(BusinessInfo aInfo)
        {
            var userId = Session["UserId"].ToString();
            if (true)
            {
                _aDashboardHandler = new DashboardHandler();
                var data = _aDashboardHandler.GetMpUnits(userId, aInfo.EmployeeType);
                return JsonConvert.SerializeObject(data);
            }
        }

        // GET: Dashboard/DashboardDepartment
        public string DashboardDepartment(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpDepartments(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSection(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSubSection(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpSubSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }

        // GET: Dashboard/DashboardDepartment
        public string DashboardDepartmentUdOn(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpUdOnDepartments(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSectionUdOn(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpUdOnSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSubSectionUdOn(CompanyObj obj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetMpUdOnSubSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardAllocatedEmpList(CompanyObj companyObj)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetHrAllocatedEmpList(companyObj);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardUnallocatedEmpList(string companyCode)
        {
            _aDashboardHandler = new DashboardHandler();
            var data = _aDashboardHandler.GetHrUnallocatedEmpList(companyCode);
            return JsonConvert.SerializeObject(data);
        }
    }
}
