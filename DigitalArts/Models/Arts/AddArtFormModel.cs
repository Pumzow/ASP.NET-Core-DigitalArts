using System.ComponentModel.DataAnnotations;
using static DigitalArts.Data.DataConstants;

namespace DigitalArts.Models
{

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
