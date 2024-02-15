using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Member
{
	public class ManageUserModel : PageModel
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public string? Username { get; set; }
		public string? Password { get; set; }

		public ManageUserModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task OnGet()
		{
			await _signInManager.UserManager.GetUserAsync(HttpContext.User);
		}

		public async Task OnPost()
		{

		}

	}
}
