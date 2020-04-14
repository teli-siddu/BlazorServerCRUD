using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLibrary.DAL.Models
{
    public class AttachmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        [ForeignKey("PersonId")]
        public int PersonId { get; set; }

        
        public PersonModel Person { get; set; }



    }
}
