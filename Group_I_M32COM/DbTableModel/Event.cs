using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    public class Event
    {
        public int Id { get; set; }
        public string Event_name { get; set; }
        public string Event_description { get; set; }
        public DateTime? Event_Start_date { get; set; }
        public DateTime? Event_End_date { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        // Foreign key for Event types
        public Event_type Event_Types { get; set; }
        // Foreign key for boat categories types
        public Boat_type Boat_Types { get; set; }
    }
}
