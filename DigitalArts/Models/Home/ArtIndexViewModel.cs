using System;

namespace DigitalArts.Models.Home
{
    public class ArtIndexViewModel
    {
        public string Id { get; set; }
        public string ArtistFullName { get; set; }
        public string Tags { get; set; }
        public int Likes { get; set; }
        public DateTime DatePublished { get; set; }
        public string Image { get; set; }
    }
}
