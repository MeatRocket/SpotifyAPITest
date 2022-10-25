using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotifyAPI.Web;
using Swan.Parsers;

namespace SpotifyAPITest.Pages
{
    public class CallbackModel : PageModel
    {
        public void OnGet()
        {

			Spotify = new(Token);

		}
    }
}
