using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardChat.UI.Pages
{
    public class IndexModel : PageModel
    {

        public void OnGet()
        {
            RedirectToPage("/Privacy");

        }
    }
}
