using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccessLibrary.DAL.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
       public string Name { get; set; }
       public long Size { get; set; }
     public  string Type { get; set; }
   
    }
}
