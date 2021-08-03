using DigitalArts.Data;
using System.Collections.Generic;
using System.Linq;

namespace DigitalArts.Services.Artist
{

    public class ArtistService : IArtistService
    {
        private readonly DigitalArtsDbContext data;

        public ArtistService(DigitalArtsDbContext data)
            => this.data = data;

        public string IdByUser(string Id)
            => this.data
                .Artists
                .Where(a => a.Id == Id)
                .Select(a => a.Id)
                .FirstOrDefault();

        public string FullNameByUser(string Id)
        { 
            var artist = this.data
                .Artists
                .Where(a => a.Id == Id)
                .Select(a => new ArtistServiceModel
                { 
                    FirstName = a.FirstName,
                    LastName = a.LastName
                })
                .FirstOrDefault();

            var fullName = artist.FirstName + " " + artist.LastName;

            return fullName;
        }

        public IEnumerable<string> All()
        {
            var artists = this.data
                .Artists
                .Select(a => new ArtistServiceModel
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName
                })
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();

            var resultArtists = new List<string>();

            foreach (var artist in artists)
            {
                resultArtists.Add(artist.FirstName + " " + artist.LastName);
            }

            return resultArtists;
        }
    }
}
