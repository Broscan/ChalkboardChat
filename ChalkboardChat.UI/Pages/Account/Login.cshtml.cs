using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Account
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public string? ErrorMessage { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public LoginModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            // Fick null error innan, lade till en check
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Username and password are required.";
                return RedirectToPage("/Account/Login");
            }

            IdentityUser? userToLogIn = await _userManager.FindByNameAsync(Username!);

            if (userToLogIn != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(userToLogIn, Password!, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/Messages");
                }
                else
                {
                    ErrorMessage = "Wrong username or password";
                    return RedirectToPage("/Account/Login");
                }
            }

            return Page();
        }
    }
}


