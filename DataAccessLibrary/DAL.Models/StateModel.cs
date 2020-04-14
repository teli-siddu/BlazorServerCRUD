using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DAL.Models
{
    public class StateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CityModel> Cities { get; set; }
    }
}
