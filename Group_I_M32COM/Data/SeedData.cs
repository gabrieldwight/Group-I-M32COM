using Group_I_M32COM.DbTableModel;
using Group_I_M32COM.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static Group_I_M32COM.Helpers.Data_RolesEnum;

namespace Group_I_M32COM.Data
{
    public class SeedData
    {
        // The seed data expects three arguments: context, usermanager object and role manager object
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated(); // this is to ensure that the database is created 

            string adminId1 = string.Empty;
            string adminId2 = string.Empty;

            string role1 = "Admin";
            string desc1 = "This is the administrator role";

            string role2 = "Member";
            string desc2 = "This is the members role";

            string password = "Admin_1";

            // To check if the admin role exists in the database
            /*if (await roleManager.FindByNameAsync(role1) == null)
            {
                // Instantiate the new admin role through the constructor linked in the Application Role class
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }

            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }*/

            /*List<Data_Roles> data_Roles = new List<Data_Roles>()
            {
                new Data_Roles { Role_Name = Role_Enum.Admin, Role_Description = RoleDescriptionNames.GetDescription(Role_Enum.Admin)},
                new Data_Roles { Role_Name = Role_Enum.Member, Role_Description = RoleDescriptionNames.GetDescription(Role_Enum.Member)},
                new Data_Roles { Role_Name = Role_Enum.User, Role_Description = RoleDescriptionNames.GetDescription(Role_Enum.User)},
                new Data_Roles { Role_Name = Role_Enum.Crew, Role_Description = RoleDescriptionNames.GetDescription(Role_Enum.Crew)}
            };*/

            // Seeding roles sample data to database
            List<Data_Roles> data_Roles = new List<Data_Roles>();

            foreach (Role_Enum role_enum in Enum.GetValues(typeof(Role_Enum)))
            {
                data_Roles.Add(new Data_Roles
                {
                    Role_Name = role_enum,
                    Role_Description = RoleDescriptionNames.GetDescription(role_enum)
                });
            }

            foreach (var roles in data_Roles)
            {
                await CreateRole(roles.Role_Name.ToString(), roles.Role_Description);
            }

            //await CreateRole("Admin", "This is the administrator role");
            //await CreateRole("Member", "This is the members role");
            //await CreateRole("User", "This is the user role");

            // A created method assigned to use role_name and role_description as a parameter to check if the role exists before inserting to database.
            async Task CreateRole(string role_name, string role_description)
            {
                if (!(await roleManager.RoleExistsAsync(role_name)))
                {
                    await roleManager.CreateAsync(new ApplicationRole(role_name, role_description, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())));
                }
            }

            // To check if the user exists in the database
            /*if (await userManager.FindByNameAsync("rtesting@test.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "rtesting@test.com",
                    Email = "rtesting@test.com",
                    FirstName = "Adam",
                    LastName = "Aldridge",
                    Address = "Fake St",
                    City = "Vancouver",
                    PostalCode = "VSU K8I",
                    Country = "Canada",
                    PhoneNumber = "6902341234",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                };

                // create the user through the user manager
                var result = await userManager.CreateAsync(user);
                // If the user is successfully created. We assign the user a password and a role through the user manager object.
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }

            // To check if the second user exists in the database
            if (await userManager.FindByNameAsync("btesting@test.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "btesting@test.com",
                    Email = "btesting@test.com",
                    FirstName = "Bob",
                    LastName = "Parker",
                    Address = "Vermount St",
                    City = "Surrey",
                    PostalCode = "VSU K8I",
                    Country = "Canada",
                    PhoneNumber = "6702341234",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                };

                // create the user through the user manager
                var result = await userManager.CreateAsync(user);
                // If the user is successfully created. We assign the user a password and a role through the user manager object.
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }

            // to check for the third user
            if (await userManager.FindByNameAsync("ctesting@test.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "ctesting@test.com",
                    Email = "ctesting@test.com",
                    FirstName = "Smith",
                    LastName = "Aldridge",
                    Address = "Yew St",
                    City = "Vancouver",
                    PostalCode = "VSU K8I",
                    Country = "Canada",
                    PhoneNumber = "6905341234",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                };

                // create the user through the user manager
                var result = await userManager.CreateAsync(user);
                // If the user is successfully created. We assign the user a password and a role through the user manager object.
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }

            // to check for the fourth user
            if (await userManager.FindByNameAsync("dtesting@test.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "dtesting@test.com",
                    Email = "dtesting@test.com",
                    FirstName = "Chris",
                    LastName = "Aldridge",
                    Address = "Fake St",
                    City = "Vancouver",
                    PostalCode = "VSU K8I",
                    Country = "Canada",
                    PhoneNumber = "6901521234",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                };

                // create the user through the user manager
                var result = await userManager.CreateAsync(user);
                // If the user is successfully created. We assign the user a password and a role through the user manager object.
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }*/

            List<ApplicationUser> applicationUsers = new List<ApplicationUser>()
            {
                // First User
                new ApplicationUser
                {
                    UserName = "gabriel",
                    Email = "gabriel@test.com",
                    FirstName = "Gabriel",
                    LastName = "Odero",
                    Address = "Fake St",
                    City = "Vancouver",
                    PostalCode = "VSU K8I",
                    Country = "Canada",
                    PhoneNumber = "6902341234",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },
                // Second User
                new ApplicationUser
                {
                    UserName = "yamini",
                    Email = "yamini@test.com",
                    FirstName = "Yamini",
                    LastName = "Rathi",
                    Address = "Vermount St",
                    City = "Surrey",
                    PostalCode = "VSU K8I",
                    Country = "Canada",
                    PhoneNumber = "6702341234",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },
                // Third User
                new ApplicationUser
                {
                    UserName = "nathan",
                    Email = "nathan@test.com",
                    FirstName = "Nathan",
                    LastName = "Zenga",
                    Address = "Yew St",
                    City = "Vancouver",
                    PostalCode = "VSU K8I",
                    Country = "Canada",
                    PhoneNumber = "6905341234",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },
                // Fourth User
                new ApplicationUser
                {
                    UserName = "umair",
                    Email = "umair@test.com",
                    FirstName = "Umair",
                    LastName = "Zia",
                    Address = "Fake St",
                    City = "Vancouver",
                    PostalCode = "VSU K8I",
                    Country = "Canada",
                    PhoneNumber = "6901521234",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                }
            };

            // Await method to create all the applicationUsers from the list
            foreach (var u in applicationUsers)
            {
                await CreateUser(u, u.UserName);
            }

            async Task CreateUser(ApplicationUser user, string username)
            {
                // To check if the number of users exists in the database before adding them to the user table
                if (await userManager.FindByNameAsync(username) == null)
                {
                    // create the users through the user manager
                    var result = await userManager.CreateAsync(user);
                    // If all the users are successfully created. We assign all the user a password and a role through the userManager object.
                    if (result.Succeeded)
                    {
                        await userManager.AddPasswordAsync(user, password);
                        await userManager.AddToRoleAsync(user, Role_Enum.Admin.ToString());
                    }
                }
            }

            // Creating a list sample data to the respective table
            List<Boat_media_type> boat_Media_Types = new List<Boat_media_type>()
            {
                // First media
                new Boat_media_type
                {
                    Boat_media_type_name = "Text",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },
                // Second media
                new Boat_media_type
                {
                    Boat_media_type_name = "Image",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },
                // Third media
                new Boat_media_type
                {
                    Boat_media_type_name = "Video",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                }
            };

            // Seeding the created multiple list data into multiple tables
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Seeding the boat media type sample data
                    foreach (var bm in boat_Media_Types)
                    {
                        // To check if the boat media type name exists on runtime to avoid duplicate entry in the database
                        if (context.Boat_Media_Types.SingleOrDefault(b => b.Boat_media_type_name == bm.Boat_media_type_name) == null)
                        {
                            context.Add(bm);
                        }
                    }                
                    context.SaveChanges();

                    // Commit the transaction in the above number operations of the database context
                    dbContextTransaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    // In case of errors committed in the transaction. Changes will be rollback to the previous state
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}
