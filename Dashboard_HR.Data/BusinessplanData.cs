using System;
using System.Data;
using System.Data.SqlClient;

namespace Dashboard_HR.Data
{
    public class BusinessplanData
    {
        public static DataTable GetLineKpiDataTable( string con, string companyCode, string fromDate, string toDate)
        {
            using (var conn = new SqlConnection(con))
            {
                conn.Open();
                var cmd = new SqlCommand();
                var aAdapter = new SqlDataAdapter();
                var aTable = new DataTable();
                var aDataSet = new DataSet();
                try
                {
                    cmd = new SqlCommand("[dbo].[BusinessPlan_MIS_Reports]", conn);
                    cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", fromDate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", toDate));
                    cmd.CommandType = CommandType.StoredProcedure;
                    aAdapter.SelectCommand = cmd;
                    aAdapter.Fill(aDataSet);
                    return aDataSet.Tables[0];
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }

}
