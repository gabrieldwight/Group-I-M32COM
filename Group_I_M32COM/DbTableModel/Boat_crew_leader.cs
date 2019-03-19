using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // This is a table to store the assigned boat crew leader to the team
    public class Boat_crew_leader
    {
        public int Id { get; set; }

        [Display(Name = "Team Leader")]
        public string User_Id { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? Created_At { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated_At { get; set; }

        // Foreign key for Boat Crew table
        [Display(Name = "Boat Team")]
        public Boat_crew boat_Crew { get; set; }
    }
}
