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
            string query = "select top 200  * from City";
            //IDbDataParameter[] parameters =
            //{
            //     _dBManager.CreateParameter("")
            //};
            DataTable dt= await _dBManager.GetDataTableAsync(query, CommandType.Text);
            return dt.AsEnumerable().Select(x => GetCityModel(x)).ToList();
          
        }

        public async Task<List<PersonModel>> GetPeople()
        {
            string query = "select * from People P left join City c on c.Id=P.City";
            //IDbDataParameter[] parameters =
            //{
            //     _dBManager.CreateParameter("")
            //};
           DataTable dt = await _dBManager.GetDataTableAsync(query, CommandType.Text);
           return  dt.AsEnumerable().Select(x => GetPersonModel(x)).ToList();
            
        }

        public Task<int> InsertPerson(PersonModel person)
        {
            //List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            //foreach (var property in person.GetType().GetProperties()) 
            //{
            //    parameters.Add(_dBManager.CreateParameter(property.Name,property.GetValue ));
            //}

            IDataParameter[] parameters =
            {
                _dBManager.CreateParameter("@FirstName",person.FirstName,DbType.String),
                _dBManager.CreateParameter("@LastName",person.LastName,DbType.String),
                _dBManager.CreateParameter("@Email",person.Email,DbType.String),
                _dBManager.CreateParameter("@City",person.City.Name,DbType.String)
            };

            string query = "insert into people(FirstName,LastName,Email,City) values(@FirstName,@LastName,@Email,@City)";

            return _dBManager.InsertAsync(query, CommandType.Text, parameters);
        }

        public PersonModel GetPersonModel(DataRow dr) 
        {
            return new PersonModel()
            {
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
                Id=dr.Field<int>("Id"),
                Name=dr.Field<string>("Name")
            };
        }
    }
}
