using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PtixiakiReservations.Models;

namespace PtixiakiReservations.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

       public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null) return RedirectToPage("./ForgotPasswordConfirmation");

            // 1. Generate 2FA Code
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            // 2. Print to Terminal (Debug)
            Console.WriteLine($"\n2FA RESET CODE: {code}\n");

            // 3. Save the email so the next page knows who we are verifying
            TempData["ResetEmail"] = Input.Email;

            return RedirectToPage("./ResetPasswordWith2fa");
        }
    }
}
