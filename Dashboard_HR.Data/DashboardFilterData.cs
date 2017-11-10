using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_HR.Data
{
    public class DashboardFilterData
    {
        private DataSet _aDataset;
        private DataTable _aDataTable;
        private SqlCommand _cmd;
        private SqlDataAdapter _aDataAdpter;
        private readonly string _con = DbConnection.GetDashboardMpConnection();
        public DataSet GetDashboardAllFilterDataFromDb() 
        {
            _aDataset = new DataSet();
            _aDataAdpter = new SqlDataAdapter();
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_DashboardFilterDll]", conn);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _aDataAdpter.SelectCommand = _cmd;
                    _aDataAdpter.Fill(_aDataset);
                    return _aDataset;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    _cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public DataTable GetPeiChartsDataFromDb() 
        {
            _aDataTable = new DataTable();
            _aDataAdpter = new SqlDataAdapter();
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_MP_PeiChartsData_By_Line]", conn);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _aDataAdpter.SelectCommand = _cmd;
                    _aDataAdpter.Fill(_aDataTable);
                    return _aDataTable;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
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
