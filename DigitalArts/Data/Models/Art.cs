using System;
using System.ComponentModel.DataAnnotations;
using static DigitalArts.Data.DataConstants;

namespace DigitalArts.Data.Models
{

    public class Art
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(ArtDescriptionLegth)]
        public string ArtistId { get; set; }
        public string ArtistFullName { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        [Required]
        public DateTime DatePublished { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
