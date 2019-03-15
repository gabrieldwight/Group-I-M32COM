using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name of the event")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Event Name")]
        public string Event_name { get; set; }

        [Required(ErrorMessage = "Please enter the description of the event")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Event Description")]
        public string Event_description { get; set; }

        [Required(ErrorMessage = "Please enter the event start date")]
        [DataType(DataType.Date)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Event Start Date")]
        public DateTime? Event_Start_date { get; set; }

        [Required(ErrorMessage = "Please enter the event end date")]
        [DataType(DataType.Date)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Event End Date")]
        public DateTime? Event_End_date { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        // Foreign key for Event types
        public Event_type Event_Types { get; set; }
        // Foreign key for boat categories types
        public Boat_type Boat_Types { get; set; }
    }
}
