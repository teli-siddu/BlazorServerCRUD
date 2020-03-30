using DataAccessLibrary.DAL.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAccessLibrary.BAL.Interface
{
    public interface IPeopleData
    {
        Task<List<PersonModel>> GetPeople();
        Task<List<CityModel>> GetCities();
        Task<int> InsertPerson(PersonModel person);

    }
}