using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net.Http;
using System.Text;

namespace DataAccessLibrary.DAL.Models
{
    public class PersonModel
    {

        public int Id { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "First Name is too long.")]
        [MinLength(2, ErrorMessage = "First Name is too short.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Last Name is too long.")]
        [MinLength(2, ErrorMessage = "Last Name is too short.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFileCollection Files { get; set; }
        public int CityId { get; set; }
        public  CityModel City { get; set; }

        

        public virtual ICollection<AttachmentModel> Attachments { get; set; }

        
        







    }
}
