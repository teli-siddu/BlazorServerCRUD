using DataAccessLibray.DAL.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibray.DAL.Concrete
{
   public class DataBaseHandlerFactory
    {

        private ConnectionStringSettings connectionStringSettings;
        //private IConfiguration _configuration;

        
            public DataBaseHandlerFactory(string ConnectionString,IConfiguration configuration)
        {
           
            //var x= configuration.GetConnectionString("Default");
            //ConnectionStringSettings connectionStringSettings=new   ConnectionStringSettings("Default", x);
            var xx = "ConnectionStrings:" + ConnectionString + ":ProviderName";
            var dfsd=configuration.GetSection(xx);
            var provider = configuration.GetSection("ConnectionStrings:"+ConnectionString+":ProviderName").Value;
            var conString = configuration.GetSection("ConnectionStrings:"+ ConnectionString + ":ConnectionString").Value;

             connectionStringSettings = new ConnectionStringSettings(ConnectionString,conString,provider);

            // configuration.GetConnectionString(connectionString);

            //connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionString];
        }

        public IDbHandler CreateDatabase()
        {
            //var y = _configuration.GetSection("ConnectionStrings:ConnectionString:ProviderName").Value;
            //var s = _configuration.GetSection("ConnectionStrings:ConnectionString:ConnectionString").Value;
            IDbHandler database = null;
            switch (connectionStringSettings.ProviderName.ToLower())
            {
                case "system.data.sqlclient":
                    database = new SqlDataAccess(connectionStringSettings.ConnectionString);
                    break;
                case "system.data.oracleclient":
                    database = new OracleDataAccess(connectionStringSettings.ConnectionString);
                    break;
                case "system.data.odbc":
                    database = new OracleDataAccess(connectionStringSettings.ConnectionString);
                    break;

            }
            //IDbHandler database = null;
            //var z = _configuration.GetConnectionString("Default");
            //        database = new SqlDataAccess(_configuration.GetConnectionString("Default"));
              

            
            return database;
        }

        public string GetProviderName()
        {
            return connectionStringSettings.ProviderName;
        }

    }
}
