﻿using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DashboardHR.Models.Models;
using Dashboard_HR.Repository.Repository;
using Dashboard_HR.Data;

namespace Dashboard_HR.Handler
{
   public class DashboardDAHandler
    {
        private DashboardDA aDashboardDa;
        public DashboardDAHandler() 
        {
            this.aDashboardDa = new DashboardDA(); 
        }
       
        private static void AddWhereCondition(DashboardFilter aInfo)
        {
            var companycode = aInfo.CompanyCode;
            aInfo.CompanyCode = companycode.Replace(",", "','");
            var divisioncode = aInfo.DivisionCode;
            aInfo.DivisionCode = divisioncode.Replace(",", "','");
            var unitcode = aInfo.UnitCode;
            aInfo.UnitCode = unitcode.Replace(",", "','");

            var departmentcode = aInfo.DepartmentCode;
            aInfo.DepartmentCode = departmentcode.Replace(",", "','");
            var sectioncode = aInfo.SectionCode;
            aInfo.SectionCode = sectioncode.Replace(",", "','");
            var subSectioncode = aInfo.SubSectionCode;
            aInfo.SubSectionCode = subSectioncode.Replace(",", "','");

            var line = aInfo.LineCode;
            aInfo.LineCode = line.Replace(",", "','");
        }

        public DataTable GetMpDivisions(string userId, DashboardFilter aInfo)
        {
            var companycode = aInfo.CompanyCode;
            aInfo.CompanyCode = companycode.Replace(",", "','");
            var divisioncode = aInfo.DivisionCode;
            aInfo.DivisionCode = divisioncode.Replace(",", "','");
            var unitcode = aInfo.UnitCode;
            aInfo.UnitCode = unitcode.Replace(",", "','");
            return this.aDashboardDa.GetMpDivisionFromDb(userId, aInfo.EmployeeType, aInfo.CompanyCode, aInfo.DivisionCode, aInfo.UnitCode, aInfo.DepartmentCode, aInfo.SectionCode, aInfo.SubSectionCode, aInfo.ActivityCode);
        }

        public DataTable GetMpUnits(string userId, DashboardFilter aInfo)
        {
            var companycode = aInfo.CompanyCode;
            aInfo.CompanyCode = companycode.Replace(",", "','");
            var divisioncode = aInfo.DivisionCode;
            aInfo.DivisionCode = divisioncode.Replace(",", "','");
            var unitcode = aInfo.UnitCode;
            aInfo.UnitCode = unitcode.Replace(",", "','");
            return this.aDashboardDa.GetMpUnitsFromDb(userId, aInfo.EmployeeType, aInfo.CompanyCode, aInfo.DivisionCode, aInfo.UnitCode, aInfo.DepartmentCode, aInfo.SectionCode, aInfo.SubSectionCode, aInfo.ActivityCode);
        }


        public DataTable GetMpDepartments(DashboardFilter aInfo)
        {
            var departmentcode = aInfo.DepartmentCode;
            aInfo.DepartmentCode = departmentcode.Replace(",", "','");
            this.aDashboardDa = new DashboardDA();
            return this.aDashboardDa.GetMpDepartmentsFromDb(aInfo.CompanyCode, aInfo.DivisionCode, aInfo.UnitCode, aInfo.DepartmentCode, aInfo.EmployeeType);
        }
        public DataTable GetMpSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            this.aDashboardDa = new DashboardDA();
            return this.aDashboardDa.GetMpSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpSubSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            this.aDashboardDa = new DashboardDA();
            return this.aDashboardDa.GetMpSubSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }


        public DataTable GetMpUdOnDepartments(string companyCode, string divisioncode, string unitCode, string empType)
        {
            this.aDashboardDa = new DashboardDA();
            return this.aDashboardDa.GetMpUdOnDepartmentsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpUdOnSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            this.aDashboardDa = new DashboardDA();
            return this.aDashboardDa.GetMpUdOnSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpUdOnSubSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            this.aDashboardDa = new DashboardDA();
            return this.aDashboardDa.GetMpUdOnSubSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }

        public DataTable GetHrAllocatedEmpListMultiFilter(CompanyObj obj)
        {
            this.aDashboardDa = new DashboardDA();
            return this.aDashboardDa.GetHrAllocatedEmpListMultiFilterFromDb(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.DepartmentCode, obj.SectionCode, obj.SubSectionCode, obj.LineCode, obj.CurrentDate, obj.EmployeeType, obj.ActivityCode);
        }
        public DataTable GetHrUnallocatedEmpList(string companyCode)
        {
            this.aDashboardDa = new DashboardDA();
            return this.aDashboardDa.GetHrUnallocatedEmpListFromDb(companyCode);
        }
        public string GetDecriptionUserCode(string userCode)
        {
            var usercode = Decrypt(userCode);
            this.aDashboardDa = new DashboardDA();
            var data = this.aDashboardDa.GetUserEmil(usercode);
            return UserEmail(data, null);
        }
        private string UserEmail(DataTable data, string email)
        {
            foreach (DataRow aRow in data.Rows)
            {
                email = aRow["UserEmail"].ToString();
            }
            return email;
        }
        private static string Decrypt(string deusercode)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            deusercode = deusercode.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(deusercode);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    deusercode = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return deusercode;
        }
        public DataSet GetDashboardAllFilterData()
        {
            var aDashboardFilterData = new DashboardFilterData();
            return aDashboardFilterData.GetDashboardAllFilterDataFromDb();
        }

        public DataTable GetPeiChartsData()
        {
            var aDashboardFilterData = new DashboardFilterData();
            return aDashboardFilterData.GetPeiChartsDataFromDb();
        }

       public DataTable GetCompanywise(string userId, DashboardFilter aInfo)
       {
            AddWhereCondition(aInfo);
            return aDashboardDa.GetCompanyWiseFromDb(userId, aInfo.EmployeeType, aInfo.CompanyCode, aInfo.DivisionCode, aInfo.UnitCode, aInfo.DepartmentCode, aInfo.SectionCode, aInfo.SubSectionCode, aInfo.LineCode, aInfo.CurrentDate, aInfo.ActivityCode);
       }

       public DataTable GetPresentEmployeeList(CompanyObj obj)
       {
            return aDashboardDa.GetPresentEmployeeListFromDb(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.DepartmentCode, obj.SectionCode, obj.SubSectionCode, obj.LineCode, obj.CurrentDate, obj.EmployeeType, obj.ActivityCode);
       }
    }
}
