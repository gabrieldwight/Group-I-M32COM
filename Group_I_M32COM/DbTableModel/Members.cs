using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // This is a table model to register the members into the boat team.
    public class Members
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter member name")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Member Name")]
        public string Member_name { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? Created_At { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated_At { get; set; }

        // Foreign key for Boat crew team
        [Display(Name = "Boat Team")]
        public Boat_crew Boat_Crew { get; set; }
    }
}
