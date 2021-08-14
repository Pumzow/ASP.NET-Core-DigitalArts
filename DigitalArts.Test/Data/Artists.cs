using DigitalArts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalArts.Test.Data
{
    public static class Artists
    {

        public static IEnumerable<Artist> TenArtists
            => Enumerable.Range(0, 10).Select(a => new Artist { });
    }
}
