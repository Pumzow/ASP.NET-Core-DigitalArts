using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DigitalArts.Data;
using DigitalArts.Models;
using DigitalArts.Data.Models;
using DigitalArts.Models.Arts;

namespace DigitalArts.Controllers
{
    public class GalleryController : Controller
    {
        private readonly DigitalArtsDbContext data;

        public GalleryController(DigitalArtsDbContext data)
            => this.data = data;

        public ActionResult Arts([FromQuery] GalleryArtsQueryModel query)
        {
            var artsQuery = this.data.Arts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Artist))
            {
                artsQuery = artsQuery.Where(a => a.Artist == query.Artist);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTags))
            {
                var querySearchTags = query.SearchTags.Split(',').ToString().Trim().ToArray();

                var tags = query.SearchTags.Split(',').ToString().Trim().ToArray();

                foreach (var tag in tags)
                {
                    artsQuery = artsQuery.Where(a =>
                        a.Tags.ToLower().Contains(tag.ToString().ToLower()));
                }
            }

            artsQuery = query.Sorting switch
            {
                ArtSorting.DatePublished => artsQuery.OrderByDescending(c => c.DatePublished),
                ArtSorting.Likes => artsQuery.OrderByDescending(a => a.Likes),
                ArtSorting.Artist or _ => artsQuery.OrderBy(a => a.Artist)
            };

            var totalArts = artsQuery.Count();

            var arts = artsQuery
                .Skip((query.CurrentPage - 1) * GalleryArtsQueryModel.ArtsPerPage)
                .Take(GalleryArtsQueryModel.ArtsPerPage)
                .Select(a => new ArtListingViewModel
                {
                    Id = a.Id,
                    Artist = a.Artist,
                    Tags = a.Tags,
                    Likes = a.Likes,
                    DatePublished = a.DatePublished,
                    Image = a.Image
                })
                .ToList();

            var artArtist = this.data
                .Arts
                .Select(c => c.Artist)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            query.TotalArts = totalArts;
            query.Artists = artArtist;
            query.Arts = arts;

            return View(query);
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
                Tags = art.Tags,
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
