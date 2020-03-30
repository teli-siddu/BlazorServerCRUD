using DataAccessLibray.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;

namespace DataAccessLibray.DAL.Concrete
{
    public class OdbcDataAccess : IDbHandler
    {
        private string ConnectionString { get; set; }
        public void CloseConnection(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new OdbcCommand
            {
                CommandText = commandText,
                CommandType = commandType,
                Connection = (OdbcConnection)connection
            };
        }

        public IDbConnection CreateConnection()
        {
            return new OdbcConnection(ConnectionString);
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new OdbcDataAdapter((OdbcCommand)command);
        }

        public IDbDataParameter CreateDataParameter(IDbCommand command)
        {
            OdbcCommand odbcCommand = (OdbcCommand)command;
            return odbcCommand.CreateParameter();
        }
    }
}
