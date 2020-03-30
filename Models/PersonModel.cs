using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DAL.Models
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public IEnumerable<CityModel> Cities { get; set; }

    }
}
