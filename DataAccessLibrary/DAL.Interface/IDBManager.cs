using System.Data;
using System.Threading.Tasks;

namespace DataAccessLibray.DAL.Concrete
{
    public interface IDBManager
    {
        void CloseConnction(IDbConnection connection);
        IDataParameter CreateParameter(string name, int size, object value, DbType type);
        IDataParameter CreateParameter(string name, int size, object value, DbType type, ParameterDirection direction);
        IDataParameter CreateParameter(string name, object value, DbType type);
        IDataParameter CreateParameter(string name, object value, DbType type, ParameterDirection direcction);
        void Delete(string commandText, CommandType type, IDataParameter[] parameters = null);
        IDbConnection GetDatabaseConnection();
        IDataReader GetDataReader(string commandText, CommandType type, IDataParameter[] parameters);
        IDataReader GetDataReader(string commandText, CommandType type, IDataParameter[] parameters, out IDbConnection connection);
        DataSet GetDataSet(string commandText, CommandType type, IDataParameter[] parameters = null);
        DataTable GetDataTable(string commandText, CommandType type, IDataParameter[] parameters = null);
        Task<DataTable> GetDataTableAsync(string commandText, CommandType type, IDataParameter[] parameters = null);
        object GetScalarValue(string commandText, CommandType type, IDataParameter[] parameters = null);
        void Insert(string commandText, CommandType type, IDataParameter[] parameters);
        Task<int> InsertAsync(string commandText, CommandType type, IDataParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string commandText, CommandType type, IDataParameter[] parameters);

        int Insert(string commandText, CommandType type, IDataParameter[] parameters, out int lastId);
        long Insert(string commandText, CommandType type, IDataParameter[] parameters, out long lastId);
        void InsertWithTransaction(string commandText, CommandType type, IDataParameter[] parameters);
        void InsertWithTransaction(string commandText, CommandType type, IsolationLevel isolationLevel, IDataParameter[] parameters);
        void Update(string commandText, CommandType type, IDataParameter[] parameters = null);
        void UpdateWithTransaction(string commandText, CommandType type, IDataParameter[] parameters = null);
        void UpdateWithTransaction(string commandText, CommandType type, IsolationLevel isolationLevel, IDataParameter[] parameters = null);
    }
}