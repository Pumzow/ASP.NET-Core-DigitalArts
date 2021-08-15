using System;
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
        public static Art OneArt
            => new Art
            {
                Id = "RandomId",
                ArtistId = "RandomArtistId",
                ArtistFullName = "RandomArtistFullname",
                Description = "RandomDescription",
                Tags = "Perfect",
                DatePublished = DateTime.UtcNow,
                Image = "RandomImage",
                Likes = null
            };
    }
}
