using System;
using DigitalArts.Infrastructure;
using DigitalArts.Models;
using DigitalArts.Models.Arts;
using DigitalArts.Services.Artist;
using DigitalArts.Services.Arts;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

        [Authorize]
        public ActionResult View(string id)
        {
            var artistId = this.artists.IdByUser(this.User.GetId());

            if (String.IsNullOrWhiteSpace(artistId))
            {
                this.ModelState.AddModelError(nameof(artistId), "Artist is not authorized.");
                return Redirect("~/Identity/Account/Register");
            }

            var artData = this.arts.View(id);

            if (artData == null)
            {
                this.ModelState.AddModelError(nameof(artData), "Art does not exist.");
                return RedirectToAction("Index", "Gallery");
            }

            var artistFullName = this.artists.FullNameByUser(artData.ArtistId);
            if (String.IsNullOrWhiteSpace(artistFullName))
            {
                this.ModelState.AddModelError(nameof(artistFullName), "Artist does not have a name.");
                return RedirectToAction("Index", "Gallery");
            }

            return View(new ArtViewModel
            {
                Id = artData.Id,
                ArtistFullName = artistFullName,
                ArtistId = artData.ArtistId,
                Description = artData.Description,
                Tags = artData.Tags,
                Likes = artData.Likes,
                DatePublished = artData.DatePublished,
                Image = artData.Image
            });
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var artistId = this.User.GetId();

            if (String.IsNullOrWhiteSpace(artistId))
            {
                this.ModelState.AddModelError(nameof(artistId), "Artist is not authorized.");
                return Redirect("~/Identity/Account/Register");
            }

            var artData = this.arts.View(id);

            if (artData == null)
            {
                this.ModelState.AddModelError(nameof(artData), "Art does not exist.");
                return RedirectToAction("Index", "Gallery");
            }
            else if (artData.ArtistId != artistId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new EditArtFormModel
            {
                Description = artData.Description,
                Tags = artData.Tags
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(string id, EditArtFormModel art)
        {
            if (!ModelState.IsValid)
            {
                return View(art);
            }

            this.arts.Edit(
                id,
                art.Description,
                art.Tags);

            return RedirectToAction("View", new { id = id });
        }
        [Authorize]
        public IActionResult Delete(string id)
        {
            var artistId = this.User.GetId();

            if (String.IsNullOrWhiteSpace(artistId))
            {
                this.ModelState.AddModelError(nameof(artistId), "Artist is not authorized.");
                return Redirect("~/Identity/Account/Register");
            }

            var artData = this.arts.View(id);

            if (artData == null)
            {
                this.ModelState.AddModelError(nameof(artData), "Art does not exist.");
                return RedirectToAction("Index", "Gallery");
            }
            else if (artData.ArtistId != artistId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(nameof(id), "Art with such id does not exist.");

                return RedirectToAction("Index", "Gallery");
            }

            this.arts.Delete(id);

            return RedirectToAction("Index", "Gallery");
        }
        [Authorize]
        public IActionResult Like(string id)
        {
            var artistId = this.User.GetId();

            if (String.IsNullOrWhiteSpace(artistId))
            {
                this.ModelState.AddModelError(nameof(artistId), "Artist is not authorized.");
                return Redirect("~/Identity/Account/Register");
            }

            var artData = this.arts.View(id);

            if (artData == null)
            {
                this.ModelState.AddModelError(nameof(artData), "Art does not exist.");
                return RedirectToAction("Index", "Gallery");
            }

            var art = this.arts.Like(id, artistId);

            return RedirectToAction("View", new { id = id });
        }
    }
}
