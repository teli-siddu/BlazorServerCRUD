using DataAccessLibrary.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCRUD.Models.People
{
    public class AddPersonViewModel
    {
        public PersonModel Person { get; set; }
        public IEnumerable<CityModel> Cities { get; set; }
    }
}
