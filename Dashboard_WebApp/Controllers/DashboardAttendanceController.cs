using System;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashboardHR.Models.Models;
using Dashboard_HR.Data.Handler;
using Dashboard_HR.Handler;
using Newtonsoft.Json;

namespace Dashboard_WebApp.Controllers
{
    public class DashboardAttendanceController : Controller
    {
        private DashboardMultiFilterHandler _aDashboardMultiFilterHandler;
        private DashboardHandler aDashboardHandler;

        private string userId;
        private DashboardDAHandler aDaHandler;
        // GET: DashboardAttendance
        public ActionResult Index(string userCode)
        {
            try
            {
                aDashboardHandler = new DashboardHandler();
                var email = aDashboardHandler.GetDecriptionUserCode(userCode);
                if (userCode != null && email == null)
                {
                    return RedirectToAction("Register", "Account");
                }
                Session["UserId"] = email;
                return this.View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public string GetCompanyWise(DashboardFilter aInfo)
        {
            try
            {
                this.userId = Session["UserId"].ToString();
                if (true)
                {
                    this.aDaHandler = new DashboardDAHandler();
                    var data = this.aDaHandler.GetCompanywise(this.userId, aInfo);
                    TempData["FilterData"] = data;
                    return JsonConvert.SerializeObject(data);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetPresentEmployeeList(CompanyObj companyObj)
        {
            this.aDaHandler = new DashboardDAHandler();
            var data = this.aDaHandler.GetPresentEmployeeList(companyObj);
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
            DataTable data = null;
            if (TempData["FilterData"] != null)
            {
                data = TempData["FilterData"] as DataTable;
                return JsonConvert.SerializeObject(data);
            }
            //this.aDaHandler = new DashboardDAHandler();
            //data = aDaHandler.GetPeiChartsData();
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
                    aDaHandler = new DashboardDAHandler();
                    data = aDaHandler.GetCompanywise(userId, aInfo);
                }
                else
                {
                    data = TempData["FilterData"] as DataTable;
                }

                var fileName = "Daily_Attendance_Data_" + DateTime.Now.ToString("yyyy_dd_M_HH_mm_ss") + ".xls";
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
                return View("Index");
            }
            catch (Exception ex)
            {
                throw new HttpException(404, "Not found");
            }
        }
        ///////////// Filtered Data load /////////////
    }
}
