using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // This is a table model to register the crew name details
    public class Boat_crew
    {
        public int Id { get; set; }
        public string Boat_crew_name { get; set; }
        public string Boat_crew_address { get; set; }
        public string Boat_crew_phone { get; set; }
        public string Boat_crew_logo { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
