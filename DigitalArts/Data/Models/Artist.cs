using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static DigitalArts.Data.DataConstants;

namespace DigitalArts.Data.Models
{

    public class Artist : IdentityUser
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
        public string ProfileImage { get; set; }
        [Required]
        [RegularExpression(EmailAddresslRegularExpression)]
        public string Email { get; set; }
        [Required]
        [MaxLength(UsernameMaxLength)]
        public string ArtistUsername { get; set; }
        [Required]
        [NotMapped]
        public IEnumerable<string> LikedArts { get; set; } = new List<string>();

        public IEnumerable<ArtistArt> ArtistArts { get; set; } = new List<ArtistArt>();

    }
}
