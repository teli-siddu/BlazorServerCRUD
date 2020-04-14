using DataAccessLibrary.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.DAL.Models
{
    public class PeopleViewModel
    {
        public IEnumerable<PersonModel>  People { get; set; }
        public SearchModel Search { get; set; }

    }
}
