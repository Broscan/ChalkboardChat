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
        public string? Username { get; set; }
        public string? Password { get; set; }

        public LoginModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void OnGet()
        {
            // hejj
        }

        public async Task<IActionResult> OnPost()
        {
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
                    // Fel lösenord eller username
                    // Testar error message
                    //TempData["error"] = "TEST ERROR MESSAGE";

                }
            }

            return Page();
        }


    }
}


