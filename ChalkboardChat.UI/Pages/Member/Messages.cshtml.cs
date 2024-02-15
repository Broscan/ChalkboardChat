using ChalkboardChat.Data.Model;
using ChalkboardChat.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages.Member
{
    public class MessagesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IRepositoryMessage _messageRepository;

        public string? Username { get; set; }

        // Denna property kanske inte behöver skickas med ?
        public string? Password { get; set; }
        [BindProperty]
        public string? Message { get; set; }

        public MessagesModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IRepositoryMessage messageRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _messageRepository = messageRepository;
        }
        public void OnGet()
        {
            _signInManager.UserManager.GetUserAsync(HttpContext.User);
        }

        public void OnPost()
        {

            Username = User.Identity.Name;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

                ChalkboardModel newMessage = new ChalkboardModel
                {
                    Date = DateTime.Now,
                    Message = Message,
                    Username = user.UserName
                };

                await _messageRepository.AddMessageToDatabase(newMessage);
            }

            return RedirectToPage("/Member/Messages");
        }
    }
}
