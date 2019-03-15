using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    public class Event_type
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter type of event")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Event Type Name")]
        public string Event_type_name { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        // Foreign key definition one event type can have many events
        public List<Event> Events { get; set; }
    }
}
