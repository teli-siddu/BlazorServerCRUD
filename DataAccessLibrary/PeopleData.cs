
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class PeopleData : IPeopleData
    {
        private readonly ISqlDataAccess _sqlDataAccess;
        public PeopleData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public Task<List<PersonModel>> GetPeople()
        {
            string sql = "select  * from dbo.People P Left join City C on C.Id=P.City";

            

            return _sqlDataAccess.LoadData<PersonModel, dynamic>(sql, new { });
        }

        public Task<List<CityModel>> GetCities() 
        {
            string sql = "select top 200 * from city";
           return  _sqlDataAccess.LoadData<CityModel, dynamic>(sql, new { });
        }
        public Task InsertPerson(PersonModel person)
        {
            string sql = @"insert into dbo.People (FirstName,LastName,Email,City)
                         values(@FirstName,@LastName,@Email,@City)";

            return _sqlDataAccess.SaveData(sql, person);
        }
    }
}
