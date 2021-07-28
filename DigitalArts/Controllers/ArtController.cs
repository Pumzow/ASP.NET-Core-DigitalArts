using System;
using System.Linq;
using DigitalArts.Data;
using DigitalArts.Data.Models;
using DigitalArts.Infrastructure;
using DigitalArts.Models;
using DigitalArts.Services.Artist;
using DigitalArts.Services.Arts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalArts.Controllers
{
    public class ArtController : Controller
    {
        //private readonly DigitalArtsDbContext data;
        private readonly IArtService arts;
        private readonly IArtistService artists;

        public ArtController(IArtService arts, IArtistService artists)
        { 
            this.arts = arts;
            this.artists = artists;
        }
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(AddArtFormModel art)
        {
            var artistId = this.artists.IdByUser(this.User.GetId());

            if (String.IsNullOrWhiteSpace(artistId))
            {
                //return RedirectToAction(nameof(DealersController.Become), "Dealers");
                return Redirect("~/Identity/Account/Register");
            }

            var artistFullName = this.artists.FullNameByUser(this.User.GetId());
            if (String.IsNullOrWhiteSpace(artistId))
            {
                this.ModelState.AddModelError(nameof(artistFullName), "Artist does not have a name.");
            }

            if (!ModelState.IsValid)
            {
                return View(art);
            }

            this.arts.Post
                (
                    artistFullName,
                    art.Description,
                    art.Tags,
                    art.Image
                );

            /*var artData = new Art
            {
                Artist = "Ivan Petrov",
                Description = art.Description,
                Tags = art.Tags,
                Likes = -1,
                Dislikes = 0,
                DatePublished = DateTime.UtcNow,
                Image = art.Image
            };

            this.data.Arts.Add(artData);
            this.data.SaveChanges();*/

            return RedirectToAction("Index", "Gallery");
        }

        /*public ActionResult View(string Id, ArtViewModel art)
        {
            var artData = data.Arts
                .Where(a => a.Id == Id)
                .Select(a => new ArtViewModel
                {
                    Id = a.Id,
                    Artist = a.Artist,
                    Tags = a.Tags,
                    Likes = a.Likes,
                    DatePublished = a.DatePublished,
                    Image = a.Image
                })
                .FirstOrDefault();

            return View(artData);
        }*/
    }
}
