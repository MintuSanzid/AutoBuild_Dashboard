using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashboardHR.Models.Models;
using Dashboard_HR.Data.Handler;
using Newtonsoft.Json;
using Dashboard_HR.Handler;

namespace Dashboard_WebApp.Controllers
{
    public class DashboardFilterController : Controller
    {
        private DashboardFilterHandler _aDashboardFilterHandler;

        // GET: DashboardFilter/MpBudgetOnroll
        public ActionResult MpBudgetOnroll(string userCode)
        {
            try
            {
                _aDashboardFilterHandler = new DashboardFilterHandler();
                var email = _aDashboardFilterHandler.GetDecriptionUserCode(userCode);
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
                _aDashboardFilterHandler = new DashboardFilterHandler();
                var email = _aDashboardFilterHandler.GetDecriptionUserCode(userCode);
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
                _aDashboardFilterHandler = new DashboardFilterHandler();
                var email = _aDashboardFilterHandler.GetDecriptionUserCode(userCode);
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

        // GET: DashboardFilter/DashboardCompany
        public string DashboardCompany(DashboardFilter aInfo)
        {
            try
            {
                var userId = Session["UserId"].ToString();
                if (true)
                {
                    _aDashboardFilterHandler = new DashboardFilterHandler();
                    var data = _aDashboardFilterHandler.GetMpCompanies(userId, aInfo);
                    return JsonConvert.SerializeObject(data);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DashboardDivision(DashboardFilter aInfo)
        {
            var userId = Session["UserId"].ToString();
            if (true)
            {
                _aDashboardFilterHandler = new DashboardFilterHandler();
                var data = _aDashboardFilterHandler.GetMpDivisions(userId, aInfo);
                return JsonConvert.SerializeObject(data);
            }
        }
        public string DashboardUnit(DashboardFilter aInfo)
        {
            var userId = Session["UserId"].ToString();
            if (true)
            {
                _aDashboardFilterHandler = new DashboardFilterHandler();
                var data = _aDashboardFilterHandler.GetMpUnits(userId, aInfo);
                return JsonConvert.SerializeObject(data);
            }
        }

        // GET: DashboardFilter/DashboardDepartment
        public string DashboardDepartment(DashboardFilter aCompanyObj) 
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetMpDepartments(aCompanyObj);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSection(CompanyObj obj)
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetMpSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSubSection(CompanyObj obj)
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetMpSubSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }

        // GET: DashboardFilter/DashboardDepartment
        public string DashboardDepartmentUdOn(CompanyObj obj)
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetMpUdOnDepartments(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSectionUdOn(CompanyObj obj)
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetMpUdOnSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSubSectionUdOn(CompanyObj obj)
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetMpUdOnSubSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardAllocatedEmpList(CompanyObj companyObj)
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetHrAllocatedEmpList(companyObj);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardUnallocatedEmpList(string companyCode)
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetHrUnallocatedEmpList(companyCode);
            return JsonConvert.SerializeObject(data);
        }

        ///////////// Filtered Data load /////////////
        public string DashboardAllFilterDll()
        {
            _aDashboardFilterHandler = new DashboardFilterHandler();
            var data = _aDashboardFilterHandler.GetDashboardAllFilterData();
            return JsonConvert.SerializeObject(data);
        }
        ///////////// Filtered Data load /////////////
    }
}
