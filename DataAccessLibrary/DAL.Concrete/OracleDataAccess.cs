using DataAccessLibray.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace DataAccessLibray.DAL.Concrete
{
    public class OracleDataAccess : IDbHandler
    {
        private string ConnectionString { get; set; }
       public OracleDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }
     
        public void CloseConnection(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new OracleCommand
            {
                CommandText = commandText,
                CommandType = commandType,
                Connection = (OracleConnection)connection
            };
        }

        public IDbConnection CreateConnection()
        {
            return new OracleConnection(ConnectionString);
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new OracleDataAdapter((OracleCommand)command);
        }

        public IDbDataParameter CreateDataParameter(IDbCommand command)
        {
            OracleCommand oracleCOmmand = (OracleCommand)command;
            return oracleCOmmand.CreateParameter();
        }
    }
}
