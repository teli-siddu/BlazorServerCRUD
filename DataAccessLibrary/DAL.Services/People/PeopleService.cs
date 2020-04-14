using DataAccessLibrary;
using DataAccessLibrary.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.DAL.Services.People
{
    public class PeopleService
    {
        private PeopleDBContext _peopleDBContext;
        public PeopleService(PeopleDBContext peopleDBContext)
        {
            _peopleDBContext = peopleDBContext;
        }

        public IEnumerable<PersonModel> GetPeople()
        {
           
                return  _peopleDBContext.People ;
           
          

        }
        public  void InsertPerson(PersonModel person)
        {
          
            try
            {
                _peopleDBContext.People.Add(person);
                _peopleDBContext.SaveChangesAsync();
            }
            catch (Exception x)
            {
                //return 0;
            }

        }

        public void UpdatePerson(PersonModel person)
        {
            _peopleDBContext.Entry(person).State = EntityState.Modified;
        }

        public void DeletePerson(int id)
        {
            PersonModel person= _peopleDBContext.People.Find(id);
            _peopleDBContext.People.Remove(person);
          
        }

        public PersonModel GetPersonById(int Id)
        {
            return _peopleDBContext.People.Find(Id);

        }
    }
}
