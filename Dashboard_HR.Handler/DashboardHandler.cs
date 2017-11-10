using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DashboardHR.Models.Models;
using Dashboard_HR.Repository.Repository;

namespace Dashboard_HR.Data.Handler
{
    public class DashboardHandler
    {
        private DashboardMp _aDashboardMp;

        public DashboardHandler()
        {
            _aDashboardMp = new DashboardMp();
        }

        public DataTable GetMpCompanies(string userId, string empType)
        {
            return _aDashboardMp.GetMpCompanyFromDb(userId, empType);
        }
        public DataTable GetMpDivisions(string userId, string empType)
        {
            return _aDashboardMp.GetMpDivisionFromDb(userId, empType);
        }
        public DataTable GetMpUnits(string userId, string empType)
        {
            return _aDashboardMp.GetMpUnitsFromDb(userId, empType);
        }


        public DataTable GetMpDepartments(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardMp = new DashboardMp();
            return _aDashboardMp.GetMpDepartmentsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardMp = new DashboardMp();
            return _aDashboardMp.GetMpSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpSubSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardMp = new DashboardMp();
            return _aDashboardMp.GetMpSubSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }


        public DataTable GetMpUdOnDepartments(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardMp = new DashboardMp();
            return _aDashboardMp.GetMpUdOnDepartmentsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpUdOnSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardMp = new DashboardMp();
            return _aDashboardMp.GetMpUdOnSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }
        public DataTable GetMpUdOnSubSections(string companyCode, string divisioncode, string unitCode, string empType)
        {
            _aDashboardMp = new DashboardMp();
            return _aDashboardMp.GetMpUdOnSubSectionsFromDb(companyCode, divisioncode, unitCode, empType);
        }

        public DataSet GetHrAllocatedEmpList(CompanyObj obj)
        {
            _aDashboardMp = new DashboardMp();
            return _aDashboardMp.GetHrAllocatedEmpListFromDb(obj.CompanyCode, obj.DivisionCode, obj.UnitCode, obj.DepartmentCode, obj.SectionCode, obj.SubSectionCode, obj.EmployeeType);
        }
        public DataTable GetHrUnallocatedEmpList(string companyCode)
        {
            _aDashboardMp = new DashboardMp();
            return _aDashboardMp.GetHrUnallocatedEmpListFromDb(companyCode);
        }
        public string GetDecriptionUserCode(string userCode)
        {
            var usercode = Decrypt(userCode);
            _aDashboardMp = new DashboardMp();
            var data = _aDashboardMp.GetUserEmil(usercode);
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
    }
}
