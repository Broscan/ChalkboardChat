using ChalkboardChat.Data.AppDbContext;
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
		private readonly AppDbContext _context;

		public IdentityUser LoggedInUser { get; set; }
		public string? Username { get; set; }
		[BindProperty]
		public string? Message { get; set; }
		public List<ChalkboardModel>? Messages { get; set; }

		public MessagesModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IRepositoryMessage messageRepository, AppDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_messageRepository = messageRepository;
			_context = context;
		}

		public async Task OnGetAsync()
		{
			LoggedInUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

			// H�mta alla messages fr�n databasen och displaya direct i onget
			Messages = await _messageRepository.GetMessagesFromDatabase();

			// Fr�n Olivers Crypto
			Messages = Messages.OrderByDescending(m => m.Date).ToList();


		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (Message != null)
			{
				var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);


				ChalkboardModel newMessage = new ChalkboardModel
				{
					Date = DateTime.Now,
					Message = Message,
					Username = user.UserName, // <-2
					UserId = user.Id
				};


				await _messageRepository.AddMessageToDatabase(newMessage);
			}

			return RedirectToPage("/Member/Messages");
		}

		public async Task<IActionResult> OnPostSignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToPage("/Account/Login");
		}

		public async Task<IActionResult> OnPostDeleteUserAsync()
		{
			// Om användaren trycker på delete. Hämta User och delete.
			var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
			if (user != null)
			{

				await DeleteUserAsync(user.UserName);

				return RedirectToPage("/Account/Login");
			}

			return RedirectToPage("/Member/Messages");
		}


		public async Task DeleteUserAsync(string username)
		{
			IdentityUser? currentUser = _userManager.Users.FirstOrDefault(user => user.UserName == username);

			if (currentUser != null)
			{
				await _userManager.DeleteAsync(currentUser);

			}


			// Hämta meddelande från databasen och se om username stämmer överens med tidigare username annars heter den Deleted
			var allMessages = await _messageRepository.GetMessagesFromDatabase();
			foreach (var message in allMessages)
			{
				if (message.Username == username)
				{
					message.Username = "{deleted user}";
				}
			}

			await _context.SaveChangesAsync();
		}


	}
}

