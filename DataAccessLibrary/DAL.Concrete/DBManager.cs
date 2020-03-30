using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibray.DAL.Interface;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibray.DAL.Concrete
{
    public class DBManager : IDBManager
    {

        private DataBaseHandlerFactory dbFactory;
        private IDbHandler database;
        private string providerName;
        private IConfiguration _configuration;

        //public DBManager(string connectionString)
        //{
        //    dbFactory = new DataBaseHandlerFactory(connectionString);
        //    database = dbFactory.CreateDatabase();
        //    providerName = dbFactory.GetProviderName();
        //}
        public DBManager(IConfiguration configuration)
        {
            var y = configuration.GetSection("ConnectionStrings:ConnectionString:ProviderName").Value;
            var s = configuration.GetSection("ConnectionStrings:ConnectionString:ConnectionString").Value;

            dbFactory = new DataBaseHandlerFactory("ConnectionString",configuration);
            database = dbFactory.CreateDatabase();
            providerName = dbFactory.GetProviderName();
        }

        public IDbConnection GetDatabaseConnection()
        {
            return database.CreateConnection();
        }
        public void CloseConnction(IDbConnection connection)
        {
            database.CloseConnection(connection);
        }

        public IDataParameter CreateParameter(string name, object value, DbType type)
        {

            return DataParameterManager.CreateParameter(providerName, name, value, type);
        }

        public IDataParameter CreateParameter(string name, int size, object value, DbType type)
        {

            return DataParameterManager.CreateParameter(providerName, name, value, size, type);
        }

        public IDataParameter CreateParameter(string name, object value, DbType type, ParameterDirection direcction)
        {

            return DataParameterManager.CreateParameter(providerName, name, value, type);
        }

        public IDataParameter CreateParameter(string name, int size, object value, DbType type, ParameterDirection direction)
        {

            return DataParameterManager.CreateParameter(providerName, name, value, size, type);
        }

        public DataTable GetDataTable(string commandText, CommandType type, IDataParameter[] parameters = null)
        {
            using (var connection = database.CreateConnection())
            {
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                    }

                    var dataset = new DataSet();
                    var dataAdapter = database.CreateDataAdapter(command);
                    dataAdapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }


        }
        public DataSet GetDataSet(string commandText, CommandType type, IDataParameter[] parameters = null)
        {
            using (var connection = database.CreateConnection())
            {
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                    }

                    var dataset = new DataSet();
                    var dataAdapter = database.CreateDataAdapter(command);
                    dataAdapter.Fill(dataset);
                    return dataset;
                }
            }


        }

        public IDataReader GetDataReader(string commandText, CommandType type, IDataParameter[] parameters)
        {
            using (var connection = database.CreateConnection())
            {
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                    }

                    return command.ExecuteReader();
                }
            }
        }
        public IDataReader GetDataReader(string commandText, CommandType type, IDataParameter[] parameters, out IDbConnection connection)
        {
            IDataReader reader = null;
            connection = database.CreateConnection();
            var command = database.CreateCommand(commandText, type, connection);
            if (parameters != null)
            {
                foreach (IDataParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

            }

            reader = command.ExecuteReader();
            return reader;

        }

        public void Delete(string commandText, CommandType type, IDataParameter[] parameters = null)
        {

            using (var connection = database.CreateConnection())
            {
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();

                }
            }

        }

        public void Insert(string commandText, CommandType type, IDataParameter[] parameters)
        {

            using (var connection = database.CreateConnection())
            {
                connection.Open();
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();

                }
            }

        }
        public int Insert(string commandText, CommandType type, IDataParameter[] parameters, out int lastId)
        {

            using (var connection = database.CreateConnection())
            {
                connection.Open();
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    lastId = Convert.ToInt32(command.ExecuteScalar());

                }
            }
            return lastId;

        }
        public long Insert(string commandText, CommandType type, IDataParameter[] parameters, out long lastId)
        {

            using (var connection = database.CreateConnection())
            {
                connection.Open();
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    lastId = Convert.ToInt64(command.ExecuteScalar());

                }
            }
            return lastId;

        }


        public void InsertWithTransaction(string commandText, CommandType type, IDataParameter[] parameters)
        {

            IDbTransaction transactionScope = null;
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                transactionScope = connection.BeginTransaction();
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception x)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }


                }
            }


        }


        public void InsertWithTransaction(string commandText, CommandType type, IsolationLevel isolationLevel, IDataParameter[] parameters)
        {

            IDbTransaction transactionScope = null;
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                transactionScope = connection.BeginTransaction(isolationLevel);
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception x)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }


                }
            }


        }


        public void Update(string commandText, CommandType type, IDataParameter[] parameters = null)
        {
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();

                }
            }

        }

        public void UpdateWithTransaction(string commandText, CommandType type, IsolationLevel isolationLevel, IDataParameter[] parameters = null)
        {

            IDbTransaction transactionScope = null;
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                transactionScope = connection.BeginTransaction(isolationLevel);
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception x)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }


                }
            }


        }


        public void UpdateWithTransaction(string commandText, CommandType type, IDataParameter[] parameters = null)
        {

            IDbTransaction transactionScope = null;
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                transactionScope = connection.BeginTransaction();
                using (var command = database.CreateCommand(commandText, type, connection))
                    if (parameters != null)
                    {
                        {
                            foreach (IDataParameter parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }
                        try
                        {
                            command.ExecuteNonQuery();
                            transactionScope.Commit();
                        }
                        catch (Exception x)
                        {
                            transactionScope.Rollback();
                        }
                        finally
                        {
                            connection.Close();
                        }


                    }
            }


        }

        public object GetScalarValue(string commandText, CommandType type, IDataParameter[] parameters = null)
        {
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                using (var command = database.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (IDataParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    return command.ExecuteScalar();

                }
            }
        }

        public  Task<DataTable> GetDataTableAsync(string commandText, CommandType type, IDataParameter[] parameters = null)
        {
            return Task.Run(() =>
            {
                using (var connection = database.CreateConnection())
                {
                    using (var command = database.CreateCommand(commandText, type, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (IDataParameter parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }

                        }

                        var dataset = new DataSet();
                        var dataAdapter = database.CreateDataAdapter(command);
                        dataAdapter.Fill(dataset);
                        return dataset.Tables[0];
                    }
                }
            });
           
            }

        public  Task<int> InsertAsync(string commandText, CommandType type, IDataParameter[] parameters)
        {

            return Task.Run(() =>
            {
                using (var connection = database.CreateConnection())
                {
                    connection.Open();
                    using (var command = database.CreateCommand(commandText, type, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (IDataParameter parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }
                        return command.ExecuteNonQuery();

                    }
                }
               
            });
            

        }

    }
}
