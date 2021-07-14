using System.ComponentModel.DataAnnotations;
using DigitalArts.Data;

namespace DigitalArts.Models
{
    using static DataConstants;

    public class AddArtFormModel
    {
        [MaxLength(ArtDescriptionLegth)]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
