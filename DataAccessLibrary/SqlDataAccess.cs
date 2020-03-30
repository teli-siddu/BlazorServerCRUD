using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private IConfiguration _configuration;
        public string connectionStringName { get; set; } = "Default";


      
        public SqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;


        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connectionString = _configuration.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }


        }




        public async Task SaveData<T>(string sql, T parameters)
        {
            string connectionString = _configuration.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }


        //public DataSet GetDataSet(string sql, CommandType commandType, IDbDataParameter[] parameters=null) 
        //{
        //    using (SqlConnection con=new SqlConnection())
        //}

    }
}
