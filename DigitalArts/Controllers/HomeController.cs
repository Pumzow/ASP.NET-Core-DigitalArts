using DigitalArts.Data;
using DigitalArts.Models.Home;
using DigitalArts.Services.Home;
using Microsoft.AspNetCore.Mvc;

namespace DigitalArts.Controllers
{
    public class HomeController : Controller
    {
        private readonly DigitalArtsDbContext data;
        private readonly IHomeService home;

        public HomeController(IHomeService home)
            => this.home = home;

        public IActionResult Index()
        {
            var arts = this.home.TopArts();

            /*var arts = this.data
                .Arts
                .OrderByDescending(c => c.Id)
                .Select(a => new ArtIndexViewModel
                {
                    Id = a.Id,
                    ArtistFullName = a.ArtistFullName,
                    Tags = a.Tags,
                    Likes = a.Likes,
                    DatePublished = a.DatePublished,
                    Image = a.Image
                })
                .OrderByDescending(a => a.Likes)
                .ThenBy(a => a.DatePublished)
                .Take(3)
                .ToList();*/

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
