using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Dashboard_HR.Data;

namespace Dashboard_HR.Repository.Repository
{
    public class DashboardDA
    {
        private DataTable _aDataTable;
        private SqlCommand _cmd;
        private SqlDataAdapter _adapter;
        private readonly string _con = DbConnection.GetDashboardMpConnection();

        public DataTable GetCompanyWiseFromDb(string userId, string empType, string companyCode, string divisionCode, string unitCode, string departmentCode, string sectionCode, string subSectionCode, string line, string currentDate, string activityCode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    AddParamsWithSqlCommand(userId, empType, companyCode, divisionCode, unitCode, departmentCode, sectionCode, subSectionCode, line, currentDate, activityCode, conn);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }

        private void AddParamsWithSqlCommand(string userId, string empType, string companyCode, string divisionCode, string unitCode, string departmentCode, string sectionCode, string subSectionCode, string line, string currentDate, string activityCode, SqlConnection conn)
        {
            _cmd = new SqlCommand("[dbo].[Dashboard_Get_DailyAttendance_CompanyWise]", conn);
            _cmd.Parameters.Add(new SqlParameter("@UserId", userId));
            _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
            _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
            _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisionCode));
            _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
            _cmd.Parameters.Add(new SqlParameter("@DepartmentCode", departmentCode));
            _cmd.Parameters.Add(new SqlParameter("@SectionCode", sectionCode));
            _cmd.Parameters.Add(new SqlParameter("@SubSectionCode", subSectionCode));
            _cmd.Parameters.Add(new SqlParameter("@LineCode", line));
            _cmd.Parameters.Add(new SqlParameter("@CurrentDate", currentDate));
            _cmd.Parameters.Add(new SqlParameter("@ActivityCode", activityCode));
        }

        public DataTable GetMpDivisionFromDb(string userId, string empType, string companyCode, string divisionCode, string unitCode, string departmentCode, string sectionCode, string subSectionCode, string activityCode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    AddParamsWithDivSqlCommand(userId, empType, companyCode, divisionCode, unitCode, departmentCode, sectionCode, subSectionCode, activityCode, conn);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        private void AddParamsWithDivSqlCommand(string userId, string empType, string companyCode, string divisionCode, string unitCode, string departmentCode, string sectionCode, string subSectionCode, string activityCode, SqlConnection conn)
        {
            _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_Divisions_By_Company_Filter]", conn);
            _cmd.Parameters.Add(new SqlParameter("@UserId", userId));
            _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
            _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
            _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisionCode));
            _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
            _cmd.Parameters.Add(new SqlParameter("@DepartmentCode", departmentCode));
            _cmd.Parameters.Add(new SqlParameter("@SectionCode", sectionCode));
            _cmd.Parameters.Add(new SqlParameter("@SubSectionCode", subSectionCode));
            _cmd.Parameters.Add(new SqlParameter("@ActivityCode", activityCode));
        }

        public DataTable GetMpUnitsFromDb(string userId, string empType, string companyCode, string divisionCode, string unitCode, string departmentCode, string sectionCode, string subSectionCode, string activityCode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    AddParamsWithUnitSqlCommand(userId, empType, companyCode, divisionCode, unitCode, departmentCode, sectionCode, subSectionCode, activityCode, conn);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        private void AddParamsWithUnitSqlCommand(string userId, string empType, string companyCode, string divisionCode, string unitCode, string departmentCode, string sectionCode, string subSectionCode, string activityCode, SqlConnection conn)
        {
            _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_Units_By_Division_Filter]", conn);
            _cmd.Parameters.Add(new SqlParameter("@UserId", userId));
            _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
            _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
            _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisionCode));
            _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
            _cmd.Parameters.Add(new SqlParameter("@DepartmentCode", departmentCode));
            _cmd.Parameters.Add(new SqlParameter("@SectionCode", sectionCode));
            _cmd.Parameters.Add(new SqlParameter("@SubSectionCode", subSectionCode));
            _cmd.Parameters.Add(new SqlParameter("@ActivityCode", activityCode));
        }


        public DataTable GetMpDepartmentsFromDb(string companycode, string divisioncode, string unitcode, string departmentcode, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    AddParamsWithDepartmentSqlCommand(companycode, divisioncode, unitcode, departmentcode, empType, conn);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        private void AddParamsWithDepartmentSqlCommand(string companycode, string divisioncode, string unitcode, string departmentcode, string empType, SqlConnection conn)
        {
            _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_Departments_By_Unit_Filter]", conn);
            _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companycode));
            _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisioncode));
            _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitcode));
            _cmd.Parameters.Add(new SqlParameter("@DepartmentCode", departmentcode));
            _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
        }

        public DataTable GetMpSectionsFromDb(string companycode, string divisioncode, string unitCode, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_Sections_By_Dept]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companycode));
                    _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisioncode));
                    _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
                    _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        public DataTable GetMpSubSectionsFromDb(string companycode, string divisioncode, string unitCode, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_SubSections_By_Sec]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companycode));
                    _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisioncode));
                    _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
                    _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public DataTable GetMpUdOnDepartmentsFromDb(string companycode, string divisioncode, string unitCode, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UdOn_Departments_By_Unit]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companycode));
                    _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisioncode));
                    _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
                    _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        public DataTable GetMpUdOnSectionsFromDb(string companycode, string divisioncode, string unitCode, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UdOn_Sections_By_Dept]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companycode));
                    _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisioncode));
                    _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
                    _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        public DataTable GetMpUdOnSubSectionsFromDb(string companycode, string divisioncode, string unitCode, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UdOn_SubSections_By_Sec]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companycode));
                    _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisioncode));
                    _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
                    _cmd.Parameters.Add(new SqlParameter("@EmpType", empType));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public DataTable GetHrAllocatedEmpListMultiFilterFromDb(string companycode, string divisioncode, string unitcode, int departmentcode, int sectioncode, int subSectioncode, int linecode, string currentdate, string employeeType, string activitycode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_AllocatedEmployeeDetails_Multi_Filter]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companycode));
                    _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisioncode));
                    _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitcode));
                    _cmd.Parameters.Add(new SqlParameter("@DepartmentCode", departmentcode));
                    _cmd.Parameters.Add(new SqlParameter("@SectionCode", sectioncode)); 
                    _cmd.Parameters.Add(new SqlParameter("@SubSectionCode", subSectioncode));
                    _cmd.Parameters.Add(new SqlParameter("@LineCode", linecode));
                    _cmd.Parameters.Add(new SqlParameter("@CurrentDate", currentdate));
                    _cmd.Parameters.Add(new SqlParameter("@EmpType", employeeType));
                    _cmd.Parameters.Add(new SqlParameter("@ActivityCode", activitycode));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        public DataTable GetHrUnallocatedEmpListFromDb(string companyCode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UnAllocatedEmployeeDetails]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        public DataTable GetHrExcessEmpListFromDb(string companyCode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_HR_ExcessEmployeeDetails]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        public DataTable GetUserEmil(string usercode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_HR_UserEmail]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@UserCode", usercode));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public DataTable GetPresentEmployeeListFromDb(string companyCode, string divisionCode, string unitCode, int departmentCode, int sectionCode, int subSectionCode, int lineCode, string currentDate, string employeeType, string activityCode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    AddParamsWithSqlCommandFilter(companyCode, divisionCode, unitCode, departmentCode, sectionCode, subSectionCode, lineCode, currentDate, employeeType, activityCode, conn);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }
        private void AddParamsWithSqlCommandFilter(string companyCode, string divisionCode, string unitCode, int departmentCode, int sectionCode, int subSectionCode, int lineCode, string currentDate, string employeeType, string activityCode, SqlConnection conn)
        {
            _cmd = new SqlCommand("[dbo].[Dashboard_Get_DailyAttendance_Employee_List]", conn);
            _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
            _cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisionCode));
            _cmd.Parameters.Add(new SqlParameter("@UnitCode", unitCode));
            _cmd.Parameters.Add(new SqlParameter("@DepartmentCode", departmentCode));
            _cmd.Parameters.Add(new SqlParameter("@SectionCode", sectionCode));
            _cmd.Parameters.Add(new SqlParameter("@SubSectionCode", subSectionCode));
            _cmd.Parameters.Add(new SqlParameter("@LineCode", lineCode));
            _cmd.Parameters.Add(new SqlParameter("@CurrentDate", currentDate));
            _cmd.Parameters.Add(new SqlParameter("@EmpType", employeeType));
            _cmd.Parameters.Add(new SqlParameter("@ActivityCode", activityCode));
        }
    }
}
