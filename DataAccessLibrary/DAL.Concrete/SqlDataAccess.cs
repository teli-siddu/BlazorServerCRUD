using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLibray.DAL.Interface;
using System.Data.SqlClient;

namespace DataAccessLibray.DAL.Concrete
{
    public class SqlDataAccess : IDbHandler
    {

       private string ConnectionString { get; set; }
        public SqlDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            return new SqlCommand
            {
                CommandText =commandText,
                CommandType=commandType,
                Connection=(SqlConnection)connection
                
            };    

        }

        public IDbDataParameter CreateDataParameter(IDbCommand command)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            return sqlCommand.CreateParameter();
        }
        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        public void CloseConnection(IDbConnection connection)
        {
           if(connection.State!=ConnectionState.Closed)
            {
                connection.Close();
                connection.Dispose();
            }
        }



       


       

       
    }
}
