using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Group_I_M32COM.Helpers
{
    public class Data_RolesEnum
    {
        public class Data_Roles
        {
            public Role_Enum Role_Name { get; set; }
            public string Role_Description { get; set; }
        }

        public enum Role_Enum
        {
            [Description("This is the administrator role")]
            Admin,
            [Description("This is the team leader role")]
            TeamLeader,
            [Description("This is the user role")]
            User,
            [Description("This is the crew member role")]
            CrewMember
        }

        public static class RoleDescriptionNames
        {
            public static string GetDescription(Enum value)
            {
                string description = value.ToString();
                FieldInfo fi = value.GetType().GetField(value.ToString());
                var attribute = (DescriptionAttribute)fi.GetCustomAttribute(typeof(DescriptionAttribute));
                return attribute.Description;
            }
        }
    }
}
