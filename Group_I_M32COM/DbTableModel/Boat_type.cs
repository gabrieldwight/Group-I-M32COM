using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // This a table model to register the remote boat class categories
    public class Boat_type
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter boat category")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Boat Class Category")]
        public string Boat_class_type { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? Created_At { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated_At { get; set; }

        // Foreign key definition one boat class category can have many sub boat class category
        public List<Sub_boat_type> Sub_Boat_Types { get; set; }

        // Foreign key definition one boat class category can have many events
        public List<Event> Event { get; set; }
        // Foreign key definition one boat class category can have many boats manufacturer
        public List<Boat> Boats { get; set; }
    }
}
