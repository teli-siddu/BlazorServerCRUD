using BlazorServerCRUD.Services.Services.Interface;
using DataAccessLibrary;
using DataAccessLibrary.DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace  BlazorServerCRUD.Services.Services.Concrete
{
    public class PeopleService : IPeopleService
    {

        private readonly HttpClient _httpClient;
        public PeopleService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> DeletePerson(int id)
        {
            HttpResponseMessage httpResponseMessage= await _httpClient.DeleteAsync("api/People?id="+id);
            string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            string retVal = JsonConvert.DeserializeObject<string>(apiResponse);
            return retVal;

        }

        public async Task<IEnumerable<CityModel>> GetCities()
        {
            return await _httpClient.GetJsonAsync<IEnumerable<CityModel>>("api/People/GetCities");
        }

        public async Task<List<PersonModel>> GetPeople()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/People");
            string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<List<PersonModel>>(apiResponse);
            return people;
        }


        public async Task<PersonModel> GetPersonById(int Id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/People/GetPersonById/"+Id);
            string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<PersonModel>(apiResponse);
            return person;
        }

        public Task<AddPersonViewModel> GetPersonViewModel(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<string> InsertPerson(PersonModel person)
        {
            throw new NotImplementedException();
        }

        public async Task<string> InsertPerson(HttpContent httpContent)
        {

            var response= await _httpClient.PostAsync("api/people", httpContent);
            string apiResponse = await response.Content.ReadAsStringAsync();
            string retVal= JsonConvert.DeserializeObject<string>(apiResponse);
            return retVal;

        }

        public async Task<PeopleViewModel> Search(SearchModel searchModel)
        {
           var stringPayLoad=  JsonConvert.SerializeObject(searchModel);

            var httpContent = new StringContent(stringPayLoad,Encoding.UTF8,"application/json");
            //var obj =await _httpClient.PostJsonAsync("api/People/Search", searchModel);
            var response = await _httpClient.PostAsync("api/people/Search", httpContent);
            string apiResponse = await response.Content.ReadAsStringAsync();
            PeopleViewModel peopleViewModel = JsonConvert.DeserializeObject<PeopleViewModel>(apiResponse);
            return peopleViewModel;


            
        }

        public async Task<PeopleViewModel> Search(HttpContent httpContent)
        {
            var response = await _httpClient.PostAsync("api/people/Search", httpContent);
            string apiResponse = await response.Content.ReadAsStringAsync();
            PeopleViewModel peopleViewModel = JsonConvert.DeserializeObject<PeopleViewModel>(apiResponse);
            return peopleViewModel;
        }

        public async Task<string> UpdatePerson(PersonModel person)
        {
             var retVal= await _httpClient.PutJsonAsync<PersonModel>("api/People", person);
            return "1";
        }

        public async Task<string> UpdatePerson(HttpContent httpContent)
        {
            var response = await _httpClient.PutAsync("api/people", httpContent);
            string apiResponse = await response.Content.ReadAsStringAsync();
            string retVal = JsonConvert.DeserializeObject<string>(apiResponse);
            return retVal;
        }
    }
}
