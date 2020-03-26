using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_net_core.DTO
{
    public class TrickRow
    {
        public int Id { get; set; }
        public String Content { get; set; }
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
    }
}
