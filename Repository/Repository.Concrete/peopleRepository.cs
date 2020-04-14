using DataAccessLibrary;
using DataAccessLibrary.DAL.Models;

using Microsoft.EntityFrameworkCore;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Repository.Repository.Concrete
{
    public class PeopleRepository : IPeopleRepository
    {
        private PeopleDBContext _peopleDBContext;
        public PeopleRepository(PeopleDBContext peopleDBContext)
        {
            _peopleDBContext = peopleDBContext;
        }
        public async Task<int> DeletePerson(int id)
        {
            PersonModel person = _peopleDBContext.People.Find(id);
            _peopleDBContext.People.Remove(person);
           return  await _peopleDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CityModel>> GetCities()
        {
            return await _peopleDBContext.Cities.ToListAsync();
        }

        public async Task<IEnumerable<PersonModel>> GetPeople()
        {
           
                return  await  _peopleDBContext.People
                                               .Include(x => x.City)
                                               .Include(y => y.Attachments)
                                               .ToListAsync();


         }

        public async Task<PersonModel> GetPersonById(int Id)
        {
           return await _peopleDBContext.People.FirstOrDefaultAsync(x => x.Id == Id);
        }
         
        public async Task<AddPersonViewModel> GetPersonViewModel(int Id)
        {
            IEnumerable<CityModel> cities = _peopleDBContext.Cities;
            PersonModel person = await _peopleDBContext.People.FirstOrDefaultAsync(x => x.Id == Id);
            return new AddPersonViewModel()
            {
                Cities = cities,
                Person = person
            };
              

        }

        public async Task<int> InsertPerson(PersonModel person)
        {


            _peopleDBContext.People.Add(person);
            int retVal = await _peopleDBContext.SaveChangesAsync();
            int Id = person.Id;

            if (person.Files is null || person.Files.Count() == 0)
            {
                return retVal;
            }

            var currentPath = Directory.GetCurrentDirectory();

            var FolderPath = Path.Combine(currentPath, "uploads");
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            foreach (var file in person.Files)
            {
                var path = Path.Combine(currentPath, "uploads", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                _peopleDBContext.Attachments.Add(new AttachmentModel()
                {
                    Name = file.FileName,
                    Path = path,
                    PersonId = Id
                });
            }
            await _peopleDBContext.SaveChangesAsync();
            return 0;
        }

        public async Task<int> UpdatePerson(PersonModel person)
        {
             _peopleDBContext.Entry(person).State = EntityState.Modified;
            return await _peopleDBContext.SaveChangesAsync();
        }

        public async Task<PeopleViewModel> Search(SearchModel search)
        {
            IQueryable<PersonModel> query = _peopleDBContext.People
                                                            .Include(x=>x.City)
                                                            .Include(y=>y.Attachments);
            if (!string.IsNullOrEmpty(search.Name)) 
            {
               query= query.Where(x => x.FirstName.Contains(search.Name) || x.LastName.Contains(search.Name));
            }

            IEnumerable<PersonModel> people = await query.ToListAsync();
            return new PeopleViewModel
            {
                People=people,
                Search=search
            };

        }

      
    }
}
