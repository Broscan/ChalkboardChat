using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Member
{

    public class ManageUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        [BindProperty]
        public string? NewUsername { get; set; }

        [BindProperty]
        public string? NewPassword { get; set; }

        public ManageUserModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {

                if (!string.IsNullOrEmpty(NewUsername))
                {
                    currentUser.UserName = NewUsername;
                    await _userManager.UpdateAsync(currentUser);
                }

                // Refresh finns i SigninManager
                await _signInManager.RefreshSignInAsync(currentUser);


                return RedirectToPage("/Member/Messages");

            }
            return Page();
        }


    }

}
