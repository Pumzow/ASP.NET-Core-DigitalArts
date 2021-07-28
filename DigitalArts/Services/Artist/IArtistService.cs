using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalArts.Services.Artist
{
    public interface IArtistService
    {
        public string IdByUser(string Id);
        public string FullNameByUser(string Id);
    }
}
