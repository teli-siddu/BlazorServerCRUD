using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DAL.Models
{
    public class CityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StateModel State { get; set; }
        public ICollection<PersonModel> People { get; set; }
       
        
    }
}
