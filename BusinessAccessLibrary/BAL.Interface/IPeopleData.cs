using DataAccessLibrary.DAL.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAccessLibrary.BAL.Interface
{
    public interface IPeopleData
    {
        Task<List<PersonModel>> GetPeopleAsync();

        List<PersonModel> GetPeople();

        Task<List<CityModel>> GetCities();
        Task<int> InsertPerson(PersonModel person);
        Task<int> DeletePerson(int id);

        Task<int> UpdatePerson(PersonModel person);
        Task<PersonModel> GetPersonById(int Id);

    }
}