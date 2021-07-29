using System.Linq;
using DigitalArts.Models;
using DigitalArts.Services.Gallery;
using DigitalArts.Services.Artist;
using Microsoft.AspNetCore.Mvc;

namespace DigitalArts.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryService gallery;
        private readonly IArtistService artists;

        public GalleryController(IGalleryService gallery, IArtistService artists)
        { 
           this.gallery = gallery;
           this.artists = artists;
        }

        public ActionResult Index([FromQuery] GalleryArtsQueryModel query)
        {

            var queryResult = this.gallery.All(
                GalleryArtsQueryModel.ArtsPerPage,
                query.CurrentPage,
                query.Sorting,
                query.ArtistFullName,
                query.SearchTag);

            var artists = this.artists.All();

            query.Artists = artists;
            query.TotalArts = queryResult.TotalArts;
            query.Arts = queryResult.Arts;

            return View(query);

            /*var artsQuery = this.data.Arts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.ArtistId))
            {
                artsQuery = artsQuery.Where(a => a.ArtistId == query.ArtistId);
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
                    ArtistId = a.ArtistId,
                    Tags = a.Tags,
                    Likes = a.Likes,
                    DatePublished = a.DatePublished,
                    Image = a.Image
                })
                .ToList();

            var artArtist = this.data
                .Arts
                .Select(c => c.ArtistId)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            query.TotalArts = totalArts;
            query.Artists = artArtist;
            query.Arts = arts;

            return View(query);*/
        }
    }
}
