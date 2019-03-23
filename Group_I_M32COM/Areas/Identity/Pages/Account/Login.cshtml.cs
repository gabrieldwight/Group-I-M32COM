using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Group_I_M32COM.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using static Group_I_M32COM.Helpers.Data_RolesEnum;
using Microsoft.EntityFrameworkCore;

namespace Group_I_M32COM.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        private readonly Data.ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public LoginModel(Data.ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, RoleManager<ApplicationRole> roleManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Email or Username")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                ApplicationUser user;

                /* To allow the user to login with email or password
                 */
                if (Input.Email.Contains("@"))
                {
                    user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                }
                else
                {
                    user = await _signInManager.UserManager.FindByNameAsync(Input.Email);
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //var user_details = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        var user_roles = await _signInManager.UserManager.GetRolesAsync(user);
                        if (user_roles != null)
                        {
                            // to redirect logged user to admin page based on the user assigned role
                            if (user_roles.Single().Equals(Role_Enum.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                user.Login_Status = true;
                                await _signInManager.UserManager.UpdateAsync(user);
                                // The use of the method below is to pass the action method name and the name of the controller
                                //return RedirectToAction("Index","Boats");
                                return RedirectToAction("AdminIndex", "Admin");
                            }

                            // to redirect logged user to home page based on the user assigned role
                            if (user_roles.Single().Equals(Role_Enum.User.ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                user.Login_Status = true;
                                await _signInManager.UserManager.UpdateAsync(user);
                                return LocalRedirect(returnUrl);
                            }

                            // to redirect logged user to competitor page based on the user assigned role
                            if (user_roles.Single().Equals(Role_Enum.TeamLeader.ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                var team_data = await _context.Boat_crew_leader
                                                       .Include(bc => bc.boat_Crew)
                                                       .FirstOrDefaultAsync(m => m.User_Id == user.Id);

                                if (team_data!= null)
                                {
                                    user.Login_Status = true;
                                    await _signInManager.UserManager.UpdateAsync(user);
                                    return RedirectToAction("Index", "Members", new { user.Id });
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, "Invalid login attempt, The user does not have a boat team to manage.");

                                    // using the identityframework object of _signInManager to logout the user
                                    await _signInManager.SignOutAsync();
                                }


                                /*user.Login_Status = true;
                                await _signInManager.UserManager.UpdateAsync(user);
                                return RedirectToAction("Index", "Members", new { user.Id });*/
                            }
                        }
                    }
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
