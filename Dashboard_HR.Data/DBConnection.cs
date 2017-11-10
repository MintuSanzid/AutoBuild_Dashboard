using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_HR.Data

{
    public static class DbConnection
    {
        private static string _connectionString;
        private const string ProviderName = "System.Data.SqlClient";
        private const string ConnectionName = "DashboardMPConnection";
        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    _connectionString = GetConnectionStrings();
                }
                return _connectionString;
            }
        }

        public static string GetConnectionStringByProvider(string providerName)
        {
            ConnectionStringSettingsCollection settingsCollection = ConfigurationManager.ConnectionStrings;

            if (settingsCollection != null)
            {
                return _connectionString = settingsCollection.Cast<ConnectionStringSettings>()
                    .Where(c => c.ProviderName == providerName)
                    .Select(x => x.ConnectionString).Skip(1).FirstOrDefault();
            }
            return null;
        }
        public static string GetConnectionStrings()
        {
            _connectionString = GetConnectionStringByProvider(ProviderName);
            if (_connectionString == null)
                _connectionString = GetConnectionStringByName(ConnectionName);
            return _connectionString;
        }
        public static string GetConnectionStringByName(string name)
        {
            string connName = name ?? ConnectionName;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connName];

            if (settings != null)
                _connectionString = settings.ConnectionString;

            return _connectionString;
        }


        public static string GetDefaultConnection()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to get DB Connection string from Config File. Contact Administrator" + ex);
            }
        }
        public static string GetDashboardMpConnection()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["DashboardMPConnection"].ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to get DB Connection string from Config File. Contact Administrator" + ex);
            }
        }
    }
}
