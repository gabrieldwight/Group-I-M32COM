using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Specialization class for the identityuser class 
        // Constructor to reference the base class

        public ApplicationUser() : base()
        {

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime? Last_Login { get; set; }
        public bool Login_Status { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
