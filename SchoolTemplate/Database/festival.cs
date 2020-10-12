using System;

namespace SchoolTemplate.Database
{
    public class Festival
    {


        public int id { get; set; }

        public string naam { get; set; }

        public string plaats { get; set; }

        public string beschrijving { get; set; }

        public DateTime start_dt { get; set; }

        public DateTime eind_dt { get; set; }

        public string prijs { get; set; }
        public global::System.Object plaatje { get; set; }
    }
}
