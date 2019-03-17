using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // This is a table model to register the boat team participating in the event.
    public class Event_participation
    {
        public int Id { get; set; }

        public string position { get; set; }

        public int points_awarded { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? Created_At { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated_At { get; set; }

        // Foreign key for Event table
        [Display(Name = "Type of event")]
        public Event Event { get; set; }

        // Foreign key for Boat Crew table
        [Display(Name = "Boat Team")]
        public Boat_crew boat_Crew { get; set; }
    }
}
