using System.Collections.Generic;

namespace DigitalArts.Services.Artist
{
    public interface IArtistService
    {
        public string IdByUser(string Id);
        public string FullNameByUser(string Id);

        public IEnumerable<string> AllByName();
        public IEnumerable<ArtistServiceModel> All();
        public ArtistServiceModel UserFullDetails(string Id);

        public bool Delete(string Id);
    }
}
