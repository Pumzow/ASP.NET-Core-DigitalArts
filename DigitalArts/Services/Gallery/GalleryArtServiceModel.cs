using System;

namespace DigitalArts.Services.Gallery
{
    public class GalleryArtServiceModel
    {
        public string Id { get; set; }
        public string ArtistId { get; set; }
        public string ArtistFullName { get; set; }
        public string Tags { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
        public DateTime DatePublished { get; set; }
        public string Image { get; set; }
    }
}
