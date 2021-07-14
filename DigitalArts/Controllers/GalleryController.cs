using Microsoft.AspNetCore.Mvc;
using DigitalArts.Data;
using DigitalArts.Models;
using DigitalArts.Data.Models;

namespace DigitalArts.Controllers
{
    public class GalleryController : Controller
    {
        private readonly DigitalArtsDbContext data;

        public GalleryController(DigitalArtsDbContext data)
            => this.data = data;

        public ActionResult Arts()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddArtFormModel art)
        {
            if (!ModelState.IsValid)
            {
                return View(art);
            }

            var artData = new Art
            {
                Description = art.Description,
                Likes = 0,
                Dislikes = 0,
                Image = art.Image
            };

            this.data.Arts.Add(artData);
            this.data.SaveChanges();

            return RedirectToAction("Arts", "Gallery");
        }
    }
}
