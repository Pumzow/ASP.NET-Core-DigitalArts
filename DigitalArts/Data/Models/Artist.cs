using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static DigitalArts.Data.DataConstants;

namespace DigitalArts.Data.Models
{

    public class Artist : IdentityUser
    {
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
        public ICollection<Likes> Likes { get; set; } = new List<Likes>();

    }
}
