using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Dashboard_HR.Data;

namespace Dashboard_HR.Repository.Repository
{
    public class DashboardMp
    {
        private DataTable _aDataTable;
        private DataSet _aDataSet;
        private SqlCommand _cmd;
        private SqlDataAdapter _adapter;
        private readonly string _con = DbConnection.GetDashboardMpConnection();

        public DataTable GetMpCompanyFromDb(string userId, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_CompanyWise]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@UserId", userId));
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
        public DataTable GetMpDivisionFromDb(string userId, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_Divisions_By_Company]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@UserId", userId));
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
        public DataTable GetMpUnitsFromDb(string userId, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_Units_By_Division]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@UserId", userId));
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

        public DataTable GetMpDepartmentsFromDb(string companycode, string divisioncode, string unitCode, string empType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataTable = new DataTable();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_UserDefine_Departments_By_Unit]", conn);
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

        public DataSet GetHrAllocatedEmpListFromDb(string companyId, string divisioncode, string unitId, int departmentId, int sectionId, int subSectionId, string employeeType)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                _cmd = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _aDataSet = new DataSet();
                try
                {
                    this.SetParmsValue(companyId, divisioncode, unitId, departmentId, sectionId, subSectionId, employeeType, conn);
                    _adapter.SelectCommand = _cmd;
                    _adapter.Fill(_aDataSet);
                    return _aDataSet;
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

        private void SetParmsValue(string companyId,string divisioncode,string unitId,int departmentId,int sectionId,int subSectionId,string employeeType,SqlConnection conn)
        {
            this._cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_AllocatedEmployeeDetails]", conn);
            this._cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyId));
            this._cmd.Parameters.Add(new SqlParameter("@DivisionCode", divisioncode));
            this._cmd.Parameters.Add(new SqlParameter("@UnitId", unitId));
            this._cmd.Parameters.Add(new SqlParameter("@DepartmentId", departmentId));
            this._cmd.Parameters.Add(new SqlParameter("@SectionId", sectionId));
            this._cmd.Parameters.Add(new SqlParameter("@SubSectionId", subSectionId));
            this._cmd.Parameters.Add(new SqlParameter("@EmpType", employeeType));
            this._cmd.CommandType = CommandType.StoredProcedure;
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

    }
}
