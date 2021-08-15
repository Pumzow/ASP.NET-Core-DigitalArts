using System.Collections.Generic;
using System.Linq;
using DigitalArts.Data.Models;

namespace DigitalArts.Test.Data
{
    public static class Artists
    {

        public static IEnumerable<Artist> TenArtists
            => Enumerable.Range(0, 10).Select(a => new Artist { });
    }
}
