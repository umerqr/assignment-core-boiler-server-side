using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_net_core.Models
{
    public class Trick
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Challenge")]
        public int ChallengeId { get; set; }
        public virtual Challenge Challenge { get; set; }


    }
}
