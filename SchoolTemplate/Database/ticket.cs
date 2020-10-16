using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTemplate.Database
{
    public class Ticket
    {
        public int beschikbaarheid { get; set; }

        public DateTime begin_dt { get; set; }

        public DateTime eind_dt { get; set; }
    }
}
