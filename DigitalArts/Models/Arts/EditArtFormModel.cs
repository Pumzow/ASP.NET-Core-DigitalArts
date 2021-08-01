using System.ComponentModel.DataAnnotations;
using static DigitalArts.Data.DataConstants;

namespace DigitalArts.Models.Arts
{
    public class EditArtFormModel
    {
        [MaxLength(ArtDescriptionLegth)]
        public string Description { get; set; }
        public string Tags { get; set; }
    }
}
