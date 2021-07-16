using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalArts.Models.Arts
{
    public class ArtListingViewModel
    {
        public string Id { get; set; }
        public string Artist { get; set; }
        public string Tags { get; set; }
        public int Likes { get; set; }
        public DateTime DatePublished { get; set; }
        public string Image { get; set; }
    }
}
