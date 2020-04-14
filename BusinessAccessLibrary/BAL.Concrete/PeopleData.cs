using BusinessAccessLibrary.BAL.Interface;
using DataAccessLibrary.DAL.Models;
using DataAccessLibray.DAL.Concrete;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using System.Collections;

namespace BusinessAccessLibrary.BAL.Conrete
{
    public class PeopleData : IPeopleData
    {
        private readonly IDBManager _dBManager;
        public PeopleData(IDBManager dBManager)
        {
            _dBManager = dBManager;
        }

        public async Task<List<CityModel>> GetCities()
        {
            string query = "select top 200  Id as CityId,Name from Cities";
            //IDbDataParameter[] parameters =
            //{
            //     _dBManager.CreateParameter("")
            //};
            DataTable dt= await _dBManager.GetDataTableAsync(query, CommandType.Text);
            return dt.AsEnumerable().Select(x => GetCityModel(x)).ToList();
          
        }

        public async Task<List<PersonModel>> GetPeopleAsync()
        {
            string query = "select p.Id,fgf FirstName,LastName,Email,ISNULL(c.Name,'') Name,ISNULL(c.Id,0) as CityId from People P left join City c on c.Id=P.City";
            //IDbDataParameter[] parameters =
            //{
            //     _dBManager.CreateParameter("")
            //};
           DataTable dt =  _dBManager.GetDataTable(query, CommandType.Text);
           return  dt.AsEnumerable().Select(x => GetPersonModel(x)).OrderByDescending(x=>x.Id).ToList();
            
        }
        public List<PersonModel> GetPeople()
        {
            string query = "select p.Id, FirstName,LastName,Email,ISNULL(c.Name,'') Name,ISNULL(c.Id,0) as CityId from People P left join City c on c.Id=P.City";
            //IDbDataParameter[] parameters =
            //{
            //     _dBManager.CreateParameter("")
            //};
            DataTable dt = _dBManager.GetDataTable(query, CommandType.Text);
            return dt.AsEnumerable().Select(x => GetPersonModel(x)).OrderByDescending(x => x.Id).ToList();

        }


        public async Task<int> InsertPerson(PersonModel person)
        {
            //List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            //foreach (var property in person.GetType().GetProperties()) 
            //{
            //    parameters.Add(_dBManager.CreateParameter(property.Name,property.GetValue ));
            //}
            try
            {
                IDataParameter[] parameters =
           {
                _dBManager.CreateParameter("@FirstName",person.FirstName,DbType.String),
                _dBManager.CreateParameter("@LastName",person.LastName,DbType.String),
                _dBManager.CreateParameter("@Email",person.Email==null?"":person.Email,DbType.String),
                _dBManager.CreateParameter("@City",person.City.Id,DbType.String)
            };

                string query = "insert into people(FirstName,LastName,Email,City) values(@FirstName,@LastName,@Email,@City)";

                return await _dBManager.InsertAsync(query, CommandType.Text, parameters);
            }
            catch (Exception x) 
            {
                return 0;
            }
           
        }

        public async Task<int> UpdatePerson(PersonModel person) 
        {
            string query = "update people set FirstName=@FirstName,LastName=@LastName,City=@City,Email=@Email where Id=@Id";

            IDataParameter[] parameters =
            {
                _dBManager.CreateParameter("FirstName",person.FirstName,DbType.String),
                _dBManager.CreateParameter("LastName",person.LastName,DbType.String),
                _dBManager.CreateParameter("City",person.City.Id,DbType.String),
                _dBManager.CreateParameter("Email",person.Email,DbType.String),
                _dBManager.CreateParameter("Id",person.Id,DbType.Int32)
            };
           return await _dBManager.ExecuteNonQueryAsync(query, CommandType.Text, parameters);
            
        }

        public PersonModel GetPersonModel(DataRow dr) 
        {
            return new PersonModel()
            {
                Id=dr.Field<int>("Id"),
                FirstName = dr.Field<string>("FirstName"),
                LastName = dr.Field<string>("LastName"),
                City = GetCityModel(dr),
                Email = dr.Field<string>("Email"),
            };
        }


       

        public CityModel GetCityModel(DataRow dr) 
        {
            return new CityModel()
            {
                Id=dr.Field<int>("CityId"),
                Name=dr.Field<string>("Name")
            };
        }

        public async Task<int> DeletePerson(int id)
        {

            string query = "delete from people where Id=@Id";

            IDataParameter[] parameters =
            {
                _dBManager.CreateParameter("@Id",id,DbType.Int32)
            };

            return await _dBManager.ExecuteNonQueryAsync(query, CommandType.Text, parameters);
        }

        public async Task<PersonModel> GetPersonById(int Id)
        {
            string query = "select p.Id,xzcxz FirstName,LastName,Email,ISNULL(c.Name,'') Name,ISNULL(c.Id,0) as CityId from People P left join City c on c.Id=P.City where Id=@Id";
            IDataParameter[] parameters =
            {
                 _dBManager.CreateParameter("@Id",Id,DbType.Int32)
            };
            DataTable dt = await _dBManager.GetDataTableAsync(query, CommandType.Text,parameters);
            return dt.AsEnumerable().Select(x => GetPersonModel(x)).FirstOrDefault();
        }

      
    }
}
