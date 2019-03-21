using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.Models
{
    public class Boat_CrewLeaderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Team Leader")]
        public string User_Id { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? Created_At { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? Updated_At { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Team Leader Name")]
        public string Full_Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }
    }
}
