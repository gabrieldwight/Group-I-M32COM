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
                },
                //Fifth User
                new ApplicationUser
                {
                    UserName="Michalis",
                    Email="michalis@testing.com",
                    FirstName="Michalis",
                    LastName="Sofroni",
                    Address="Coventry Street",
                    City="Coventry",
                    PostalCode="CV1 5QQ",
                    Country="Cyprus",
                    PhoneNumber="0123456789",
                    Created_At=DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },

                //Sixth User
                new ApplicationUser
                {
                    UserName="Ted",
                    Email="ted@testing.com",
                    FirstName="Ted",
                    LastName="Teddie",
                    Address="London Street",
                    City="London",
                    PostalCode="LN1 5QQ",
                    Country="Greece",
                    PhoneNumber="0456721398",
                    Created_At=DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },

                //Seventh User
                new ApplicationUser
                {
                    UserName="Nate",
                    Email="nate@testing.com",
                    FirstName="Nate",
                    LastName="Nate",
                    Address="Reading Street",
                    City="Reading",
                    PostalCode="RG4 5QQ",
                    Country="United Kingdom",
                    PhoneNumber="0987654321",
                    Created_At=DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },

                //Eigth User
                new ApplicationUser
                {
                    UserName="Natasha",
                    Email="natasha@testing.com",
                    FirstName="Natasha",
                    LastName="Natasha",
                    Address="Liverpool Street",
                    City="Liverpool",
                    PostalCode="LV1 5QQ",
                    Country="Germany",
                    PhoneNumber="0144568919",
                    Created_At=DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
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

            //Creating a list sample of data for the event timetable
            List<Event_type> event_Types = new List<Event_type>()
            {
                //Fast events type
                new Event_type
                {
                    Event_type_name = "Charity Ball",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },
                new Event_type
                {
                    Event_type_name="Thames round",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },

                new Event_type
                {
                    Event_type_name="Faster round",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },

                new Event_type
                {
                    Event_type_name="Maximum speed round",
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                }

            };

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

            //Creating sample about events table
            List<Event> events_ = new List<Event>()
            {
                // First event
                new Event
                {
                    Event_name = "Faster race",
                    Event_description = "The faster boat is the winning.",
                    Event_Types = new Event_type()
                    {
                        Event_type_name = "Faster round"
                    },
                    Event_Start_date = DateTime.ParseExact("2019-07-20 15:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    Event_End_date = DateTime.ParseExact("2019-07-20 12:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },
                // Second event
                new Event
                {
                    Event_name = "Thames race",
                    Event_description = "Enjoy a 4 day's race in Thames.",
                    Event_Start_date = DateTime.ParseExact("2019-08-23 08:30:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    Event_End_date = DateTime.ParseExact("2019-08-27 08:30:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },
                // Third event
                new Event
                {
                    Event_name = "Charity Ball",
                    Event_description = "An hour race where th rewards of the winnings are donated to charity",
                    Event_Start_date = DateTime.ParseExact("2019-04-05 20:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    Event_End_date = DateTime.ParseExact("2019-04-05 21:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                }
            };

            // seeding boat crew teams
            List<Boat_crew> boat_Crews = new List<Boat_crew>()
            {
                // first boat crew
                new Boat_crew
                {
                    Boat_crew_name = "Coventry Pacers",
                    Boat_crew_address = "CV1",
                    Boat_crew_phone = "8392920022",
                    Boat_crew_allocation = 3,
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },

                // second boat crew
                new Boat_crew
                {
                    Boat_crew_name = "Coventry Godiva",
                    Boat_crew_address = "CV1",
                    Boat_crew_phone = "6392920022",
                    Boat_crew_allocation = 10,
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },

                // third boat crew
                new Boat_crew
                {
                    Boat_crew_name = "Warwick Pacers",
                    Boat_crew_address = "WW1",
                    Boat_crew_phone = "3392920022",
                    Boat_crew_allocation = 10,
                    Created_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim())
                },

                // fourth boat crew
                new Boat_crew
                {
                    Boat_crew_name = "Warwick Blazers",
                    Boat_crew_address = "WW1",
                    Boat_crew_phone = "8262920022",
                    Boat_crew_allocation = 3,
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

                    //Seeding the event type sample data 
                    foreach (var event_type in event_Types)
                    {
                        //To check if the event type exists on runtime to avoid duplicate entry in the database
                        if (context.Event_Types.SingleOrDefault(e => e.Event_type_name == event_type.Event_type_name) == null)
                        {
                            context.Add(event_type);
                        }
                    }

                    // Seeding the event sample data
                    foreach (var events in events_)
                    {
                        // To check if the event data exists on runtime to avoid duplicate entry in the database
                        if (context.Events.SingleOrDefault(es => es.Event_name == events.Event_name) == null)
                        {
                            context.Add(events);
                        }
                    }

                    // Seeding the boat crew sample data
                    foreach (var boatcrew in boat_Crews)
                    {
                        // To check if the boat crew data exists on runtime to avoid duplicate entry in the database
                        if (context.Boat_Crews.SingleOrDefault(bc => bc.Boat_crew_name == boatcrew.Boat_crew_name) == null)
                        {
                            context.Add(boatcrew);
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
