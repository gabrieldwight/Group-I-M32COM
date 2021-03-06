﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.DbTableModel
{
    // This a table model to register the remote boat class sub categories
    public class Sub_boat_type
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter sub boat category")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The minimum {2} and Maximum {1} characters are allowed", MinimumLength = 3)]
        [Display(Name = "Sub Boat Class Category")]
        public string Sub_boat_class_type { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        // Foreign key for Sub boat categories types
        public Boat_type Boat_Types { get; set; }

        // Foreign key definition one boat sub class category can have many boats manufacturer
        public List<Boat> Boats { get; set; }
    }
}
