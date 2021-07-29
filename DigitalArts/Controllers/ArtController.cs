using System;
using DigitalArts.Infrastructure;
using DigitalArts.Models;
using DigitalArts.Services.Artist;
using DigitalArts.Services.Arts;
using Microsoft.AspNetCore.Mvc;

namespace DigitalArts.Controllers
{
    public class ArtController : Controller
    {
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
                return Redirect("~/Identity/Account/Register");
            }

            var artistFullName = this.artists.FullNameByUser(artistId);
            if (String.IsNullOrWhiteSpace(artistFullName))
            {
                this.ModelState.AddModelError(nameof(artistFullName), "Artist does not have a name.");
            }

            if (!ModelState.IsValid)
            {
                return View(art);
            }

            this.arts.Post
                (
                    artistId,
                    artistFullName,
                    art.Description,
                    art.Tags,
                    art.Image
                );

            return RedirectToAction("Index", "Gallery");
        }

        public ActionResult View(string Id)
        {
            var artistId = this.artists.IdByUser(this.User.GetId());

            if (String.IsNullOrWhiteSpace(artistId))
            {
                return Redirect("~/Identity/Account/Register");
            }

            var artData = this.arts.View(Id);

            if (artData.ArtistId != artistId/* && !User.IsAdmin()*/)
            {
                return Unauthorized();
            }

            var artistFullName = this.artists.FullNameByUser(artData.ArtistId);
            if (String.IsNullOrWhiteSpace(artistFullName))
            {
                this.ModelState.AddModelError(nameof(artistFullName), "Artist does not have a name.");
            }

            return View(new ArtViewModel
            {
                Id = artData.Id,
                ArtistFullName = artistFullName,
                Tags = artData.Tags,
                Likes = artData.Likes,
                DatePublished = artData.DatePublished,
                Image = artData.Image
            });

            /*var artData = data.Arts
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

            return View(artData);*/
        }
    }
}
