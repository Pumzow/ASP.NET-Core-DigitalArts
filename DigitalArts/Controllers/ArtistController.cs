using DigitalArts.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigitalArts.Controllers
{
    public class ArtistController : Controller
    {
        private readonly UserManager<Artist> artistManager;
        private readonly SignInManager<Artist> signInManager;

        public ArtistController(UserManager<Artist> artistManager, SignInManager<Artist> signInManager)
        {
            this.artistManager = artistManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
