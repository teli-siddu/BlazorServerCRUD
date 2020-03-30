using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLibray.DAL.Interface
{
    public interface IDbHandler
    {
        IDbConnection CreateConnection();
        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection);
        IDbDataAdapter CreateDataAdapter(IDbCommand command);

        IDbDataParameter CreateDataParameter(IDbCommand command);

        void CloseConnection(IDbConnection connection);

    }
}
