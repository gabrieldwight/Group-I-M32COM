using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // The properties to create Boat table in the database 
    public class Boat
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter boat name")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Boat Name")]
        public string Boat_name { get; set; }

        [Required(ErrorMessage = "Please enter boat top speed")]
        [DataType(DataType.Text)]
        [StringLength(10, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Boat Top Speed")]
        public string Boat_top_speed { get; set; }

        [Required(ErrorMessage = "Please enter voat weight")]
        [DataType(DataType.Text)]
        [StringLength(10, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Boat Weight")]
        public string Boat_weight { get; set; }

        [Required(ErrorMessage = "Please enter boat description")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Boat description")]
        public string Boat_description { get; set; }

        [Display(Name = "Boat Media Type")]
        public int Boat_media_type { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? Created_At { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated_At { get; set; }

        // Foreign key for boat categories and Sub boat categories types
        public Boat_type Boat_Types { get; set; }
        public Sub_boat_type Sub_Boat_Types { get; set; }

        // Foreign key definition one boat can have many boat media
        public List<Boat_media> Boat_Medias { get; set; }
    }
}
