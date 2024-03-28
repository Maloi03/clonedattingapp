using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class NewFeed
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string CreatorUserName { get; set; }
        public string Content { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public string Feeling { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.UtcNow;
        public DateTime lastModifiedTime { get; set; }
        public AppUser Creator {  get; set; }
    }
}