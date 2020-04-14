using DataAccessLibrary.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<PersonModel>> GetPeople();
        Task<int> InsertPerson(PersonModel person);
        Task<int> UpdatePerson(PersonModel person);
        Task<int> DeletePerson(int id);
        Task<PersonModel> GetPersonById(int Id);
        Task<IEnumerable<CityModel>> GetCities();
        Task<AddPersonViewModel> GetPersonViewModel(int Id);
        Task<PeopleViewModel> Search(SearchModel searchModel);  



    }
}
