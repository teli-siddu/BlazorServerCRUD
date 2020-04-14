using DataAccessLibrary.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCRUD.Models.People
{
    public class PeopleViewModel
    {
        public IEnumerable<PersonModel>  People { get; set; }

    }
}
