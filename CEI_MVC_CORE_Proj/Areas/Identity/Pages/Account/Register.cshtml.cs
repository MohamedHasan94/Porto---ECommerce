using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CEI_MVC_CORE_Proj.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CEI_MVC_CORE_Proj.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unit;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, IUnitOfWork unit)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _unit = unit;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            /*Custom Attributes Section */
            [MaxLength(50), Display(Name = "First Name"), Required]
            public string FirstName { get; set; }

            [MaxLength(50), Display(Name = "Last Name"), Required]
            public string LastName { get; set; }

            [MaxLength(50), Display(Name = "User Name")] //Not Required
            public string UserName { get; set; }

            [MaxLength(100), Display(Name = "Profile Picture")] //Not Required
            public string ProfilePictureLink { get; set; }

            public IFormFile Image { get; set; }

            [Display(Name = " Vendor Account")] //Not Required
            public bool RequestVendor { get; set; }
            /*Custom Attributes Section */

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = Input.UserName, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, ProfilePictureLink = GetUserImage(Input.Image) };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);


                    //Check if requested vendor role and create the request to the admin
                    if (Input.RequestVendor == true)
                    {
                        _unit.Requests.Add(new RequestToAdmin
                        {
                            User = user,
                            Status = RequestStatus.Pending,
                            Type = RequestType.RequestVendorRole,
                            Data = "The user is requesting vendor Role "
                        });
                    }

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private string GetUserImage(IFormFile image)
        {
            string path = null;
            if (image != null)
            {
                path = $"/images/UserImages/{Input.UserName}." + Path.GetFileName(image.FileName).Split('.')[1];
                using (var stream = System.IO.File.Create(@"./wwwroot" + path))
                {
                    image.CopyTo(stream);
                }
            }
            return path;
        }
    }
}
