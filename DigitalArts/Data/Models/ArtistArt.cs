namespace DigitalArts.Data.Models
{
    public class ArtistArt
    {
        public string ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string ArtId { get; set; }
        public Art Art { get; set; }
    }
}
