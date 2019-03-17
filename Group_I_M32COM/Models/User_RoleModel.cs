using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.Models
{
    public class User_RoleModel
    {
        public string Id { get; set; }

        [Display(Name = "Role")]
        public string Role_Name { get; set; }

        public string User_Id { get; set; }

        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        [Display(Name = "Name")]
        public string Full_Name
        {
            get
            {
                return First_Name + " " + Last_Name;
            }
        }
        public string Email { get; set; }
        public string Address { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? Created_At { get; set; }

        public DateTime? Updated_At { get; set; }

        [Display(Name = "Last Seen")]
        public DateTime? Last_Login { get; set; }

        [Display(Name = "Status")]
        public bool Login_Status { get; set; }
    }
}
