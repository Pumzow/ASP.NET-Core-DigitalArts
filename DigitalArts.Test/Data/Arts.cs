using System.Collections.Generic;
using System.Linq;
using DigitalArts.Data.Models;
using DigitalArts.Services.Gallery;

namespace DigitalArts.Test.Data
{
    public static class Arts
    {
        public static IEnumerable<Art> TenArts
            => Enumerable.Range(0, 10).Select(a => new Art { });
        public static IEnumerable<GalleryArtServiceModel> TenGalleryArts
            => Enumerable.Range(0, 10).Select(a => new GalleryArtServiceModel { });
    }
}
