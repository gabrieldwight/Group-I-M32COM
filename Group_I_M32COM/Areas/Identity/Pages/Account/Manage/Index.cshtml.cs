using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Group_I_M32COM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_I_M32COM.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        private readonly Data.ApplicationDbContext _context;
        private readonly Helpers.ICountryDataService _countryData;

        public IndexModel(
            Helpers.ICountryDataService countryData,
            Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
            _countryData = countryData;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [DataType(DataType.Text)]
            [MaxLength(50)]
            public string Address { get; set; }

            [DataType(DataType.Text)]
            [MaxLength(50)]
            public string City { get; set; }

            [DataType(DataType.Text)]
            [MaxLength(15)]
            [Display(Name = "Postal Code")]
            public string PostalCode { get; set; }

            [DataType(DataType.Text)]
            [MaxLength(35)]
            public string Country { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["CountryList"] = _countryData.LoadCountryList();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var address = user.Address;
            var city = user.City;
            var postalcode = user.PostalCode;
            var country = user.Country;

            Username = userName;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                City = city,
                PostalCode = postalcode,
                Country = country
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        // The OnPostAsync on the manage user page is used to perform form input operations to the database
        public async Task<IActionResult> OnPostAsync(string CountryList)
        {
            string selected_country = CountryList.ToString().Trim();
            Console.WriteLine("Selected Country: " + selected_country);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            // This will use the async task to find the user.Id in the database to check if the user exists before performing a database update
            var userToUpdate = await _context.Users.FindAsync(user.Id);

            if (userToUpdate != null)
            {
                // To pass the object properties from the form input model.
                userToUpdate.Address = Input.Address;
                userToUpdate.City = Input.City;
                userToUpdate.PostalCode = Input.PostalCode;
                userToUpdate.Country = selected_country;
                userToUpdate.Updated_At = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim());

                if (await TryUpdateModelAsync<ApplicationUser>(
                    userToUpdate,
                    "user",
                    u => u.Address,
                    u => u.City,
                    u => u.PostalCode,
                    u => u.Country,
                    u => u.Updated_At))
                {
                    // The SaveChangesAsync is used to perform the update query operations from the user model.
                    await _context.SaveChangesAsync();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
