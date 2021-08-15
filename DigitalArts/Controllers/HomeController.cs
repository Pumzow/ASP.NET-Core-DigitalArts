using DigitalArts.Models.Home;
using DigitalArts.Services.Home;
using Microsoft.AspNetCore.Mvc;

namespace DigitalArts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService home;

        public HomeController(IHomeService home)
            => this.home = home;

        public IActionResult Index()
        {
            var arts = this.home.TopArts();


            return View(new IndexViewModel
            { 
                Arts = arts
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
