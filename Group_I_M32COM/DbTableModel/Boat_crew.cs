using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // This is a table model to register the boat team name details
    public class Boat_crew
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter boat team name")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Boat Team Name")]
        public string Boat_crew_name { get; set; }

        [Required(ErrorMessage = "Please enter boat team address")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Boat Team Address")]
        public string Boat_crew_address { get; set; }


        public string Boat_crew_phone { get; set; }
        public string Boat_crew_logo { get; set; }

        [Required(ErrorMessage = "Please enter boat team allocation available")]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Boat Team Space Available")]
        public int Boat_crew_allocation { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? Created_At { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated_At { get; set; }


        // Foreign key definition one boat team can have many boat crew members
        public List<Members> Members { get; set; }

        // Foreign key definition one boat team can participate in many event participants
        public List<Event_participation> Event_Participations { get; set; }
    }
}
