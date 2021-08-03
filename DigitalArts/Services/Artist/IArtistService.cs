using System.Collections.Generic;

namespace DigitalArts.Services.Artist
{
    public interface IArtistService
    {
        public string IdByUser(string Id);
        public string FullNameByUser(string Id);

        public IEnumerable<string> All();
    }
}
