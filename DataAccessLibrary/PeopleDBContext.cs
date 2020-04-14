using DataAccessLibrary.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class PeopleDBContext: DbContext
    {
        public PeopleDBContext(DbContextOptions<PeopleDBContext> options):base(options)
        {

        }

        public DbSet<CityModel>  Cities { get; set; }
        public DbSet<PersonModel> People { get; set; }
        public DbSet<StateModel> States { get; set; }
        public DbSet<AttachmentModel> Attachments { get; set; }
    }
}

