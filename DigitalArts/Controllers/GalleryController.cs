using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DigitalArts.Data;
using DigitalArts.Models;
using DigitalArts.Models.Arts;

namespace DigitalArts.Controllers
{
    public class GalleryController : Controller
    {
        private readonly DigitalArtsDbContext data;

        public GalleryController(DigitalArtsDbContext data)
            => this.data = data;

        public ActionResult Index([FromQuery] GalleryArtsQueryModel query)
        {
            var artsQuery = this.data.Arts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Artist))
            {
                artsQuery = artsQuery.Where(a => a.Artist == query.Artist);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTag))
            {
                artsQuery = artsQuery.Where(a =>
                    a.Tags.ToLower().Contains(query.SearchTag.ToLower()));
            }

            artsQuery = query.Sorting switch
            {
                GallerySorting.DatePublished => artsQuery.OrderByDescending(c => c.DatePublished),
                GallerySorting.Likes => artsQuery.OrderByDescending(a => a.Likes)
            };

            var totalArts = artsQuery.Count();

            var arts = artsQuery
                .Skip((query.CurrentPage - 1) * GalleryArtsQueryModel.ArtsPerPage)
                .Take(GalleryArtsQueryModel.ArtsPerPage)
                .Select(a => new GalleryListingViewModel
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
    }
}
