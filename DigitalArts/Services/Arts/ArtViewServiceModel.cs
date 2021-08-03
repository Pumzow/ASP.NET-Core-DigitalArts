using System;

namespace DigitalArts.Services.Arts
{
    public class ArtViewServiceModel
    {
        public string Id { get; set; }
        public string ArtistId { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime DatePublished { get; set; }
        public string Image { get; set; }
    }
}
