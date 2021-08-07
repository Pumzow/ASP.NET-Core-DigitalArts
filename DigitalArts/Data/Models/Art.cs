using System;
using System.Collections.Generic;
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
        [Required]
        public DateTime DatePublished { get; set; }
        [Required]
        public string Image { get; set; }
        public ICollection<Likes> Likes { get; set; } = new List<Likes>();
    }
}
