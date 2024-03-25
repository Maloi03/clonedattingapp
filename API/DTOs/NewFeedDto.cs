using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class NewFeedDto
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string CreatorUserName { get; set; }
        public string CreatorPhotoUrl { get; set; }
        public string Content { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public string Feeling { get; set; }
        public DateTime PostedTime { get; set; }
        public DateTime lastModifiedTime { get; set; }
    }
}