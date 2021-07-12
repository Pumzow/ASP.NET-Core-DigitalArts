using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalArts.Data.Models
{
    using static DataConstants;

    public class Artist
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }
        [Required]
        public IFormFile ProfileImage { get; set; }
        [Required]
        [RegularExpression(EmailAddresslRegularExpression)]
        public string Email { get; set; }
        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }
        [Required]
        [MaxLength(PasswordMaxLegth)]
        public string  Password { get; set; }
        [Required]
        public int TotalArtLikes { get; set; }

        public IEnumerable<ArtistArt> ArtistArts { get; set; } = new List<ArtistArt>();

    }
}
