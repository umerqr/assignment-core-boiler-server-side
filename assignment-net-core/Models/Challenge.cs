using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_net_core.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
        public ICollection<Trick> Tricks { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
       
    }
}
