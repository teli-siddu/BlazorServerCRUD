using DataAccessLibrary.DAL.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorServerCRUD.Services.Services.Interface
{
    public interface IPeopleService
    {
        Task<List<PersonModel>> GetPeople();
        Task<string> InsertPerson(PersonModel person);
        Task<string> UpdatePerson(PersonModel person);
        Task<string> UpdatePerson(HttpContent httpContent);
        Task<string> DeletePerson(int id);
        Task<PersonModel> GetPersonById(int Id);
        Task<IEnumerable<CityModel>> GetCities();
        Task<AddPersonViewModel> GetPersonViewModel(int Id);
        Task<PeopleViewModel> Search(SearchModel searchModel);
        Task<PeopleViewModel> Search(HttpContent httpContent);
        Task<string> InsertPerson(HttpContent httpContent);



    }
}
