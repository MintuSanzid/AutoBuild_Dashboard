using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DashboardHR.Models.Models;
using Dashboard_HR.Repository.Repository;
using Dashboard_HR.Data;

namespace Dashboard_HR.Handler
{
    public class DashboardFilterHandler
    {
        private DashboardFilterMp _aDashboardFilterMp;
        public DashboardFilterHandler() 
        {
            _aDashboardFilterMp = new DashboardFilterMp();
        }

        public object GetMpCompanies(string userId, DashboardFilter aInfo )
        {
            AddWhwereCondition(aInfo);
            return _aDashboardFilterMp.GetMpCompanyFromDb(userId, aInfo.EmployeeType, aInfo.CompanyCode, aInfo.DivisionCode, aInfo.UnitCode, aInfo.DepartmentCode, aInfo.SectionCode, aInfo.SubSectionCode, aInfo.ActivityCode);
        }

        private static void AddWhwereCondition(DashboardFilter aInfo)
        {
            var companycode = aInfo.CompanyCode;
            aInfo.CompanyCode = companycode.Replace(",", "','");
            var divisioncode = aInfo.DivisionCode;
            aInfo.DivisionCode = divisioncode.Replace(",", "','");
            var unitcode = aInfo.UnitCode;
            aInfo.UnitCode = unitcode.Replace(",", "','");
        }

        public DataTable GetMpDivisions(string userId, DashboardFilter aInfo)
        {
            var companycode = aInfo.CompanyCode;
            aInfo.CompanyCode = companycode.Replace(",", "','");
            var divisioncode = aInfo.DivisionCode;
            aInfo.DivisionCode = divisioncode.Replace(",", "','");
            var unitcode = aInfo.UnitCode;
            aInfo.UnitCode = unitcode.Replace(",", "','");
            return _aDashboardFilterMp.GetMpDivisionFromDb(userId, aInfo.EmployeeType, aInfo.CompanyCode, aInfo.DivisionCode, aInfo.UnitCode, aInfo.DepartmentCode, aInfo.SectionCode, aInfo.SubSectionCode, aInfo.ActivityCode);
        }
        public DataTable GetMpUnits(string userId, DashboardFilter aInfo)
        {
            var companycode = aInfo.CompanyCode;
            aInfo.CompanyCode = companycode.Replace(",", "','");
            var divisioncode = aInfo.DivisionCode;
            aInfo.DivisionCode = divisioncode.Replace(",", "','");
            var unitcode = aInfo.UnitCode;
            aInfo.UnitCode = unitcode.Replace(",", "','");
            return _aDashboardFilterMp.GetMpUnitsFromDb(userId, aInfo.EmployeeType, aInfo.CompanyCode, aInfo.DivisionCode, aInfo.UnitCode, aInfo.DepartmentCode, aInfo.SectionCode, aInfo.SubSectionCode, aInfo.ActivityCode);
        }


        public DataTable GetMpDepartments(DashboardFilter aInfo)
        {
            var departmentcode = aInfo.DepartmentCode;
            aInfo.DepartmentCode = departmentcode.Replace(",", "','");
            _aDashboardFilterMp = new DashboardFilterMp();
            return _aDashboardFilterMp.GetMpDepartmentsFromDb(aInfo.CompanyCode, aInfo.DivisionCode, aInfo.UnitCode, aInfo.DepartmentCode, aInfo.EmployeeType);
        }
        public DataTable GetMpSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardFilterMp = new DashboardFilterMp();
            return _aDashboardFilterMp.GetMpSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpSubSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardFilterMp = new DashboardFilterMp();
            return _aDashboardFilterMp.GetMpSubSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }


        public DataTable GetMpUdOnDepartments(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardFilterMp = new DashboardFilterMp();
            return _aDashboardFilterMp.GetMpUdOnDepartmentsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpUdOnSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardFilterMp = new DashboardFilterMp();
            return _aDashboardFilterMp.GetMpUdOnSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpUdOnSubSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardFilterMp = new DashboardFilterMp();
            return _aDashboardFilterMp.GetMpUdOnSubSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        
        public DataTable GetHrAllocatedEmpList(CompanyObj obj)
        {
            _aDashboardFilterMp = new DashboardFilterMp();
            return _aDashboardFilterMp.GetHrAllocatedEmpListFromDb(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.DepartmentCode, obj.SectionCode, obj.SubSectionCode, obj.EmployeeType);
        }
        public DataTable GetHrUnallocatedEmpList(string companyCode)
        {
            _aDashboardFilterMp = new DashboardFilterMp();
            return _aDashboardFilterMp.GetHrUnallocatedEmpListFromDb(companyCode);
        }
        public string GetDecriptionUserCode(string userCode)
        {
            var usercode = Decrypt(userCode);
            _aDashboardFilterMp = new DashboardFilterMp();
            var data = _aDashboardFilterMp.GetUserEmil(usercode);
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
            DashboardFilterData aDashboardFilterData = new DashboardFilterData();
            return aDashboardFilterData.GetDashboardAllFilterDataFromDb();
        }
    }
}
