using System.Collections.Generic;

namespace DigitalArts.Services.Gallery
{
    public class GalleryQueryServiceModel
    {
        public int ArtsPerPage {get; set;}

        public int CurrentPage { get; init; }

        public int TotalArts { get; set; }

        public IEnumerable<GalleryArtServiceModel> Arts { get; set; }
    }
}
