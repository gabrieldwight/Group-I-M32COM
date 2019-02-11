using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_I_M32COM.Models
{
    // This class help in extending the IdentityRole class to expose the IdentityRole properties from the ASPNETCOREIdentity
    public class ApplicationRole : IdentityRole
    {
        // Constructor reference to the base class
        public ApplicationRole() : base()
        {

        }

        // Constructor to pass Role Name to the base class
        public ApplicationRole(string roleName) : base(roleName)
        {

        }

        // Constructor to pass two arguments such as Role Name, description and creation date to the base class from the class local properties
        public ApplicationRole(string roleName, string description, DateTime creationDate) : base(roleName)
        {
            this.Description = description;
            this.CreationDate = creationDate;
        }

        // Local properties for extension to the identity role class
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
