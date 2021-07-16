using System.ComponentModel.DataAnnotations;
using DigitalArts.Data;

namespace DigitalArts.Models
{
    using static DataConstants;

    public class AddArtFormModel
    {
        [MinLength(ArtArtistFullnameMinLength)]
        [MaxLength(ArtArtistFullnameMaxLength)]
        public string Artist { get; set; }
        [MaxLength(ArtDescriptionLegth)]
        public string Description { get; set; }
        public string Tags { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
