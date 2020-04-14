using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessAccessLibrary.BAL.Interface;
using DataAccessLibrary.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http;
using DataAccessLibrary;
using Microsoft.EntityFrameworkCore;
using Repository.Repository.Interface;
using Newtonsoft.Json;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleData _peopleData;
        private IPeopleRepository _peopleRepository;
        private readonly IWebHostEnvironment _environment;
        private PeopleDBContext _peopleDBContext;
        public PeopleController(IPeopleData peopleData, IWebHostEnvironment environment,PeopleDBContext peopleDBContext,IPeopleRepository peopleRepository)
        {
            _peopleData = peopleData;
            _environment = environment;
            _peopleDBContext = peopleDBContext;
            _peopleRepository = peopleRepository;

        }
        

        /// <summary>
        /// Gets all Person list
        /// </summary>
        /// <returns>A list of Persons</returns>
        [HttpGet]
        public  async Task<IActionResult> Get() 
        
        {
            try
            {
                /// <summary>Use of ADO.Net
                /// <para>Uses Business layer to fetch data</para>
                /// </summary>
                //return Ok( _peopleData.GetPeople());

                //return  await _peopleDBContext
                //              .People
                //              .Include(x=>x.City)
                //              .ToListAsync();

                /// <summary>Use of Entity Framework
                /// <para>Uses Repository class  to fetch data using Entity Framework</para>
                /// </summary>
               
                return Ok( await _peopleRepository.GetPeople());
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data from db");
            }
               


        }

        /// <summary>
        /// Adds new Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<IActionResult> Post([FromForm]PersonModel person) 
        {
           
          
            if (!ModelState.IsValid) 
            {
                return StatusCode(StatusCodes.Status400BadRequest,"Validation Failed") ;
            }




            //  retVal = await _peopleData.InsertPerson(person);//BL
            try
            {
               
                return  Ok(await _peopleRepository.InsertPerson(person));
               // return Ok();
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding person");
            }
           
         
            //return retVal;
        
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id) 
        {
            //int retVal= await _peopleData.DeletePerson(Id);

            //return retVal;
            //PersonModel person = _peopleDBContext.People.Find(Id);
            //_peopleDBContext.People.Remove(person);
            //await _peopleDBContext.SaveChangesAsync();
            try
            {
                return Ok(await _peopleRepository.DeletePerson(Id));
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting person");
            }
           

            

         }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm]PersonModel person) 
        {

            //return await _peopleData.UpdatePerson(person);
            //_peopleDBContext.Entry(person).State = EntityState.Modified;
            //await _peopleDBContext.SaveChangesAsync();

            //return 1;

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Validation Failed");
            }


            try
            {
                //return Ok( await _peopleData.UpdatePerson(person));
                return Ok(await _peopleRepository.UpdatePerson(person));
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating person");
            }


        }
        [HttpGet("GetPersonById/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            //return await _peopleData.GetPersonById(Id);
            //  return _peopleDBContext.People.Find(Id);
            try
            {
                //return Ok( await _peopleData.UpdatePerson(person));
                return Ok(await _peopleRepository.GetPersonById(Id));
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while getting person");
            }
        }
        
        [HttpGet("GetCities")]

        public async Task<IActionResult> GetCities() 
        {
            try
            {
                return Ok( await _peopleRepository.GetCities());
            }
            catch (Exception x) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while getting cities form database");
            }
           
        }

        [HttpGet]
        [Route("GetPersonViewModel/{Id}")]
        public async Task<IActionResult> GetPersonViewModel(int id) 
        {
            try
            {
                return Ok( await _peopleRepository.GetPersonViewModel(id));

            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while getting peson form database");
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task upload() 
        {
            
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (var file in HttpContext.Request.Form.Files)
                {

                    var FolderPath = Path.Combine(_environment.ContentRootPath, "uploads");
                    if (!Directory.Exists(FolderPath)) 
                    {
                        Directory.CreateDirectory(FolderPath);
                    }
                    var path = Path.Combine(_environment.ContentRootPath, "uploads", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
        }

        [HttpPost("Search")]
        public async Task<PeopleViewModel> Search(SearchModel searchModel)
        {
         
              return await _peopleRepository.Search(searchModel);
         

        
        }




        }
    }