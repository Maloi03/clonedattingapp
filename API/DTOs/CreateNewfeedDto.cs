using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class CreateNewfeedDto
    {
        public string Content{get; set;}
         public ICollection<Photo> Photos { get; set; }
        public string Feeling{get; set;}
    }
}