using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard_HR.Data;

namespace Dashboard_HR.Repository.Repository
{
    public class DashboardBp 
    {
        public DataSet ADataset;
        private SqlCommand _cmd;  
        private SqlDataAdapter _aAdapter;   

        private readonly string _con = DbConnection.GetDashboardMpConnection();
        public DataSet GetBusinessplanDataFromDb()
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                 _cmd = new SqlCommand();
                 _aAdapter = new SqlDataAdapter();
                ADataset = new DataSet();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_BusinessplanData]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@UserId", ""));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _aAdapter.SelectCommand = _cmd;
                    _aAdapter.Fill(ADataset);
                    return ADataset;
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
        public DataSet GetDashboardFindByCompanyFromDb(string buyerCode, string merchantCode, string companyCode,string filterCode )
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                 _cmd = new SqlCommand();
                 _aAdapter = new SqlDataAdapter();
                ADataset = new DataSet();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_BP_FindByCompanyData]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@BuyerCode", buyerCode));
                    _cmd.Parameters.Add(new SqlParameter("@MerchantCode", merchantCode));
                    _cmd.Parameters.Add(new SqlParameter("@CompanyId", companyCode));
                    _cmd.Parameters.Add(new SqlParameter("@FilterCode", filterCode));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _aAdapter.SelectCommand = _cmd;
                    _aAdapter.Fill(ADataset);
                    return ADataset;
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
        public DataSet GetBpCapacityDataFromDb(string buyerCode, string merchantCode, string companyCode)
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                 _cmd = new SqlCommand();
                 _aAdapter = new SqlDataAdapter();
                ADataset = new DataSet();
                try
                {
                    _cmd = new SqlCommand("[dbo].[Dashboard_Get_BP_Capacity_SubItem]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@BuyerCode", buyerCode));
                    _cmd.Parameters.Add(new SqlParameter("@MerchantCode", merchantCode));
                    _cmd.Parameters.Add(new SqlParameter("@CompanyId", companyCode));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _aAdapter.SelectCommand = _cmd;
                    _aAdapter.Fill(ADataset);
                    return ADataset;
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
        public DataSet GetLineKpiDataTable(string companyCode, string fromDate, string toDate)  
        {
            using (var conn = new SqlConnection(_con))
            {
                conn.Open();
                 _cmd = new SqlCommand();
                 _aAdapter = new SqlDataAdapter();
                ADataset = new DataSet();
                try
                {
                    _cmd = new SqlCommand("[dbo].[BusinessPlan_MIS_Reports_TEST]", conn);
                    _cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                    _cmd.Parameters.Add(new SqlParameter("@FromDate", fromDate));
                    _cmd.Parameters.Add(new SqlParameter("@ToDate", toDate));
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _aAdapter.SelectCommand = _cmd;
                    _aAdapter.Fill(ADataset);
                    return ADataset;
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
