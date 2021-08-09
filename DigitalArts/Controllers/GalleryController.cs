using DigitalArts.Models;
using DigitalArts.Services.Gallery;
using DigitalArts.Services.Artist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
        }
    }
}
