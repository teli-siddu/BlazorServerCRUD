using DataAccessLibrary.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.DAL.Models
{
    public class AddPersonViewModel
    {
        public PersonModel Person { get; set; }
        public IEnumerable<CityModel> Cities { get; set; }

        public SearchModel Search { get; set; }
    }
}
