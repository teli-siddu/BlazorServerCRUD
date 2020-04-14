using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using DataAccessLibrary;
using DataAccessLibrary.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
//using MVCCRUD.Models.People;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Repository.Repository.Concrete;
using Repository.Repository.Interface;

namespace MVCCRUD.Controllers
{
    public class PeopleController : Controller
    {
        private PeopleDBContext _peopleDBContext;
        private IWebHostEnvironment _environment;
        //private IPeopleRepository _peopleRepository;
        public PeopleController(PeopleDBContext peopleDBContext, IWebHostEnvironment environment)
        {
            _peopleDBContext = peopleDBContext;
            _environment = environment;
            //_peopleRepository = peopleRepository;
        }
        public async Task<IActionResult> Index()
        {
            //IEnumerable<PersonModel> people = _peopleDBContext
            //                                 .People
            //                                 .Include(x => x.City)
            //                                 .Include(y => y.Attachments);
            //PeopleViewModel peopleViewModel = new PeopleViewModel
            //{
            //    People = people
            //};
            //return View(peopleViewModel);

            IEnumerable<PersonModel> people;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44333/api/people"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    people = JsonConvert.DeserializeObject<List<PersonModel>>(apiResponse);
                }
            }

            PeopleViewModel peopleViewModel = new PeopleViewModel
            {
                People = people
            };

            return View(peopleViewModel);

        }



        [HttpPost]
        public async Task<IActionResult> AddPerson(PersonModel person)
        {
            //_peopleDBContext.People.Add(person);
            
            var formData = new MultipartFormDataContent();
            byte[] fileBytes=null;
            foreach (var file in person.Files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                       file.CopyTo(ms);
                       fileBytes = ms.ToArray();
                       formData.Add(new ByteArrayContent(fileBytes), "files",file.FileName);
                        //string s = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                    }
                }
            }
            //  var personModelJson = JsonConvert.SerializeObject(person, Formatting.None, new IsoDateTimeConverter());




            //formData.Add(new StringContent(personModelJson), "PersonModel");

            foreach (var property in person.GetType().GetProperties()) 
            {
                var prop = property.Name;
                var propVal = property.GetValue(person, null)?.ToString();
                if (propVal!= null) 
                {
                    formData.Add(new StringContent(propVal), prop);
                } 
            }
            string retVal;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:44333/api/people", formData))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    retVal = JsonConvert.DeserializeObject<string>(apiResponse);
                }
            }


            return RedirectToAction("Index");



        }

        [HttpGet]
        public async Task<IActionResult> AddPerson()
        {
          
            //IEnumerable<CityModel> cities = _peopleDBContext.Cities;

            IEnumerable<CityModel> cities;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44333/api/people/GetCities"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cities = JsonConvert.DeserializeObject<List<CityModel>>(apiResponse);
                }
            }

            AddPersonViewModel addPersonViewModel = new AddPersonViewModel()
            {
                Cities = cities
            };

            return View(addPersonViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditPerson(int Id)
        {
            //var person = _peopleDBContext
            //             .People
            //             .Include(x => x.City)
            //             .Where(x => x.Id == Id)
            //             .FirstOrDefault();
            //var cities = _peopleDBContext
            //            .Cities;
            //AddPersonViewModel addPersonViewModel = new AddPersonViewModel()
            //{
            //    Cities = cities,
            //    Person = person
            //};
            AddPersonViewModel addPersonViewModel;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44333/api/people/GetPersonViewModel/"+Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addPersonViewModel = JsonConvert.DeserializeObject<AddPersonViewModel>(apiResponse);
                }
            }
            return View("AddPerson", addPersonViewModel);

        }


        [HttpPost]
        public async Task<IActionResult> EditPerson(PersonModel person)
        {
            //if (!ModelState.IsValid)
            //{
            //    var cities = _peopleDBContext
            //           .Cities;
            //    AddPersonViewModel addPersonViewModel = new AddPersonViewModel()
            //    {
            //        Cities = cities,
            //        Person = person
            //    };
            //    return View("AddPerson", addPersonViewModel);
            //}
            //_peopleDBContext.Entry(person).State = EntityState.Modified;

            //await _peopleDBContext.SaveChangesAsync();

            string  retVal;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync<PersonModel>("https://localhost:44333/api/people",person))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    retVal = JsonConvert.DeserializeObject<string>(apiResponse);
                }
            }
           



            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePerson(int Id)
        {
            //var person = _peopleDBContext
            //                .People
            //                .Include(x => x.City)
            //                .Where(x => x.Id == Id)
            //                .FirstOrDefault();

            //_peopleDBContext.People.Remove(person);

            //await _peopleDBContext.SaveChangesAsync();

            string retVal;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44333/api/people?Id=" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    retVal = JsonConvert.DeserializeObject<string>(apiResponse);
                }
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ViewDocument(int Id)
        {
             AttachmentModel attachment= _peopleDBContext.Attachments.Where(x => x.Id==Id).FirstOrDefault();


            var memory = new MemoryStream();
            string contentType;
            using (var stream = new FileStream(attachment.Path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            new FileExtensionContentTypeProvider().TryGetContentType(attachment.Path,out contentType);
            return File(memory, contentType?? "application/octet-stream", Path.GetFileName(attachment.Path));
        }

        public async Task<IActionResult> Search(PeopleViewModel peopleViewModel) 
        {

            SearchModel searchModel = peopleViewModel.Search;
            PeopleViewModel people;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync("https://localhost:44333/api/people/Search", searchModel))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    people = JsonConvert.DeserializeObject<PeopleViewModel>(apiResponse);
                }
            }

            return View("Index", people);


        }









    }
}