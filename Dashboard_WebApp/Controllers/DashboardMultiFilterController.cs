using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashboardHR.Models.Models;
using Dashboard_HR.Data.Handler;
using Newtonsoft.Json;
using Dashboard_HR.Handler;

namespace Dashboard_WebApp.Controllers
{
    public class DashboardMultiFilterController : Controller
    {
        private DashboardMultiFilterHandler _aDashboardMultiFilterHandler;

        // GET: DashboardFilter/MpBudgetOnroll
        public ActionResult MpBudgetOnroll(string userCode)
        {
            try
            {
                _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
                var email = _aDashboardMultiFilterHandler.GetDecriptionUserCode(userCode);
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
                _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
                var email = _aDashboardMultiFilterHandler.GetDecriptionUserCode(userCode);
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
                _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
                var email = _aDashboardMultiFilterHandler.GetDecriptionUserCode(userCode);
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
                    _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
                    var data = _aDashboardMultiFilterHandler.GetMpCompanies(userId, aInfo);
                    TempData["FilterData"] = data;
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
                _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
                var data = _aDashboardMultiFilterHandler.GetMpDivisions(userId, aInfo);
                return JsonConvert.SerializeObject(data);
            }
        }
        public string DashboardUnit(DashboardFilter aInfo)
        {
            var userId = Session["UserId"].ToString();
            if (true)
            {
                _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
                var data = _aDashboardMultiFilterHandler.GetMpUnits(userId, aInfo);
                return JsonConvert.SerializeObject(data);
            }
        }

        // GET: DashboardFilter/DashboardDepartment
        public string DashboardDepartment(DashboardFilter aCompanyObj)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetMpDepartments(aCompanyObj);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSection(CompanyObj obj)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetMpSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSubSection(CompanyObj obj)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetMpSubSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }

        // GET: DashboardFilter/DashboardDepartment
        public string DashboardDepartmentUdOn(CompanyObj obj)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetMpUdOnDepartments(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSectionUdOn(CompanyObj obj)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetMpUdOnSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardSubSectionUdOn(CompanyObj obj)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetMpUdOnSubSections(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.EmployeeType);
            return JsonConvert.SerializeObject(data);
        }
        public string DashboardAllocatedEmpList(CompanyObj companyObj)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetHrAllocatedEmpListMultiFilter(companyObj);
            return JsonConvert.SerializeObject(data);
        }

        public string DashboardAllocatedEmpListMultiFilter(CompanyObj companyObj)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetHrAllocatedEmpListMultiFilter(companyObj);
            return JsonConvert.SerializeObject(data);
        }

        public string DashboardUnallocatedEmpList(string companyCode)
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetHrUnallocatedEmpList(companyCode);
            return JsonConvert.SerializeObject(data);
        }

        ///////////// Filtered Data load /////////////
        public string DashboardAllFilterDll()
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetDashboardAllFilterData();
            return JsonConvert.SerializeObject(data);
        }

        public string GetPeiChartsData()
        {
            _aDashboardMultiFilterHandler = new DashboardMultiFilterHandler();
            var data = _aDashboardMultiFilterHandler.GetPeiChartsData();
            return JsonConvert.SerializeObject(data);
        }

        public ActionResult GetExportToExcell(DashboardFilter aInfo)
        {
            try
            {
                DataTable data;
                if (TempData["FilterData"] == null)
                {
                    var userId = Session["UserId"].ToString();
                     data = _aDashboardMultiFilterHandler.GetMpCompanies(userId, aInfo);
                }
                else
                {
                     data = TempData["FilterData"] as DataTable;
                }

                var fileName = "MP_Filtered_Data_" + DateTime.Now.ToString("yyyy_dd_M_HH_mm_ss") + ".xls";
                var gv = new GridView { DataSource = data };
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                var sw = new StringWriter();
                var htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                return View("MpBudgetOnroll");
            }
            catch (Exception ex)
            {
                throw new HttpException(404, "Not found");
            }
        }
        ///////////// Filtered Data load /////////////
    }
}
