using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalArts.Data.Models
{
    using static DataConstants;

    public class Art
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(ArtDescriptionLegth)]
        public string ArtistId { get; set; }
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
