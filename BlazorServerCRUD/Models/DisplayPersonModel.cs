﻿using DataAccessLibrary.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerCRUD.Models
{
    public class DisplayPersonModel
    {

        public int Id { get; set; }
        [Required]
        [StringLength(15,ErrorMessage = "First Name is too long.")]
        [MinLength(5,ErrorMessage ="First Name is too short.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Last Name is too long.")]
        [MinLength(5, ErrorMessage = "Last Name is too short.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }

        public string City { get; set; }
        

    }
}
