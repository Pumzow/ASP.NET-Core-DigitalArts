using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DigitalArts.Models.Arts;

namespace DigitalArts.Models
{
    public class GalleryArtsQueryModel
    {
        public const int ArtsPerPage = 3;

        public string ArtistId { get; init; }

        [Display(Name = "Tag")]
        public string SearchTag { get; init; }

        public GallerySorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalArts { get; set; }

        public IEnumerable<string> Artists { get; set; }

        public IEnumerable<GalleryListingViewModel> Arts { get; set; }
    }
}
