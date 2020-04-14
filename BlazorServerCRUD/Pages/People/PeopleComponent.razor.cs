using BlazorInputFile;
using BlazorServerCRUD.Services.Services.Interface;
using DataAccessLibrary.DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace BlazorServerCRUD.Pages.People
{
    public partial class PeopleComponent
    {

        [Inject]
        public IPeopleService peopleService { get; set; }

        public IFileListEntry[] files1;
        private List<PersonModel> people;
        protected IEnumerable<CityModel> cities = new List<CityModel>();

        private PersonModel Person = new PersonModel();
        SearchModel searchModel = new SearchModel();
        private bool Update = false;

        protected override async Task OnInitializedAsync()
        {

            //   people= await peopleRepository.GetPeople();
            people = await peopleService.GetPeople();
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && cities.Count() == 0)
            {
                cities = await peopleService.GetCities();
                StateHasChanged();
            }
        }
        protected async Task AddPerson(EditContext formContext)
        {
            if (!formContext.Validate())
            {
                return;
            }
            var formData = new MultipartFormDataContent();
            byte[] fileBytes = null;
            if (files1?.Count() > 0) 
            {
                foreach (var file in files1)
                {
                    if (file.Size > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await file.Data.CopyToAsync(ms);
                            fileBytes = ms.ToArray();
                            formData.Add(new ByteArrayContent(ms.GetBuffer()), "files", file.Name);
                            //string s = Convert.ToBase64String(fileBytes);
                            // act on the Base64 data
                        }
                    }
                }
            }
           
            //  var personModelJson = JsonConvert.SerializeObject(person, Formatting.None, new IsoDateTimeConverter());




            //formData.Add(new StringContent(personModelJson  ), "PersonModel");

            foreach (var property in Person.GetType().GetProperties())
            {
                var prop = property.Name;
                var propVal = property.GetValue(Person, null)?.ToString();
                if (propVal != null)
                {
                    formData.Add(new StringContent(propVal), prop);
                }
            }
            //string retVal=string.Empty;

            //var response= await httpClient.PostAsync("https://localhost:44333/api/people", formData);
            //string apiResponse = await response.Content.ReadAsStringAsync();
            //retVal = JsonConvert.DeserializeObject<string>(apiResponse); 

            await peopleService.InsertPerson(formData);
        }

        protected async Task UpdatePerson(EditContext formContext)
        {
            //if (!formContext.Validate())
            //{
            //    return;
            //}
            //string retVal = await peopleService.UpdatePerson(Person);
            //if (int.Parse(retVal) > 0) 
            //{
            //  people=   await peopleService.GetPeople();
            //}
            if (!formContext.Validate())
            {
                return;
            }
            var formData = new MultipartFormDataContent();
            byte[] fileBytes = null;
            if (files1?.Count() > 0)
            {
                foreach (var file in files1)
                {
                    if (file.Size > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await file.Data.CopyToAsync(ms);
                            fileBytes = ms.ToArray();
                            formData.Add(new ByteArrayContent(ms.GetBuffer()), "files", file.Name);
                            //string s = Convert.ToBase64String(fileBytes);
                            // act on the Base64 data
                        }
                    }
                }
            }

          




            //formData.Add(new StringContent(personModelJson  ), "PersonModel");

            foreach (var property in Person.GetType().GetProperties())
            {
                var prop = property.Name;
                var propVal = property.GetValue(Person, null)?.ToString();
                if (propVal != null)
                {
                    formData.Add(new StringContent(propVal), prop);
                }
            }
          string retVal=  await peopleService.UpdatePerson(formData);

            if (int.Parse(retVal) > 0) 
            {
                people= await peopleService.GetPeople();
            }


        }
        //protected async Task<PersonModel> GetPersonById(int id) 
        //{
        //   PersonModel person=  await peopleService.GetPersonById(id);
        //   return person;
        //}

        protected async Task EditPerson(int id)
        {
            Person = await peopleService.GetPersonById(id);
            Update = true;
        }
        protected async Task DeletePerson(int id)
        {
            string retVal = await peopleService.DeletePerson(id);
            if ( int.Parse( retVal) > 0) 
            {
                people.Remove(people.Where(x=>x.Id==id).FirstOrDefault());
            }

        }

        protected async Task Search() 
        {
            //var formData = new MultipartFormDataContent();
            //foreach (var property in searchModel.GetType().GetProperties())
            //{
            //    var prop = property.Name;
            //    var propVal = property.GetValue(searchModel, null)?.ToString();
            //    if (propVal != null)
            //    {
            //        formData.Add(new StringContent(propVal), prop);
            //    }

            //}
            //PeopleViewModel peopleViewModel= await  peopleService.Search(formData);

            //people = peopleViewModel.People.ToList();
          PeopleViewModel peopleViewModel=   await peopleService.Search(searchModel);

            people = peopleViewModel.People.ToList();
        }

    }
}
