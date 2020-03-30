
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibray.DAL.Concrete
{
    public class DataParameterManager
    {


        public static IDataParameter CreateParameter( string providerName,string name, object value, DbType type, ParameterDirection direction = ParameterDirection.Input)
        {
            IDataParameter parameter = null;
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient" :
                    return CreateSqlParameter(name, value, type, direction);
                        break;
                case "system.data.oracleclient":
                    return CreateOracleParameter(name, value, type, direction);
                    break;
                case "system.data.odbcclient":
                    return  CreateOdbcParameter(name, value, type, direction);
                    break;
            }
            return parameter;


        }

        public static IDataParameter CreateParameter(string providerName, string name, object value,int size ,DbType type, ParameterDirection direction = ParameterDirection.Input)
        {
            IDataParameter parameter = null;
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    return CreateSqlParameter(name, value,size, type, direction);
                    break;
                case "system.data.oracleclient":
                    return CreateOracleParameter(name, value,size, type, direction);
                    break;
                case "system.data.odbcclient":
                    return CreateOdbcParameter(name, value,size, type, direction);
                    break;
            }
            return parameter;


        }

        public static IDataParameter CreateSqlParameter(string name, object value,DbType type, ParameterDirection direction)
        {
            return new SqlParameter
            {
                ParameterName = name,
                Value = value,
                DbType = type,
                Direction = direction

            };
        }
        public static IDataParameter CreateOracleParameter(string name, object value, DbType type, ParameterDirection direction)
        {
            return new OracleParameter
            {
                ParameterName = name,
                Value = value,
                DbType = type,
                Direction = direction

            };
        }
        public static IDataParameter CreateOdbcParameter(string name, object value, DbType type, ParameterDirection direction)
        {
            return new OdbcParameter
            {
                ParameterName = name,
                Value = value,
                DbType = type,
                Direction = direction

            };
        }

        public static IDataParameter CreateSqlParameter(string name, object value,int size, DbType type, ParameterDirection direction)
        {
            return new SqlParameter
            {
                ParameterName = name,
                Value = value,
                DbType = type,
                Direction = direction,
                Size=size

            };
        }
        public static IDataParameter CreateOracleParameter(string name, object value,int size, DbType type, ParameterDirection direction)
        {
            return new OracleParameter
            {
                ParameterName = name,
                Value = value,
                DbType = type,
                Direction = direction,
                Size = size

            };
        }
        public static IDataParameter CreateOdbcParameter(string name, object value,int size, DbType type, ParameterDirection direction)
        {
            return new OdbcParameter
            {
                ParameterName = name,
                Value = value,
                DbType = type,
                Direction = direction,
                Size=size

            };
        }

    }
}
