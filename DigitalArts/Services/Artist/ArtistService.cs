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

        public IEnumerable<string> AllByName()
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

        public IEnumerable<ArtistServiceModel> All()
        {
            var artists = this.data
                .Artists
                .Select(a => new ArtistServiceModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    ProfileImage = a.ProfileImage,
                    Email = a.Email,
                    ArtistUsername = a.ArtistUsername
                })
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();

            return artists;
        }

        public ArtistServiceModel UserFullDetails(string Id)
        {

            var artistData = this.data.Artists
                .FirstOrDefault(a => a.Id == Id);

            if (artistData == null)
            {
                return null;
            }

            ArtistServiceModel artist = new ArtistServiceModel
            {
                Id = artistData.Id,
                FirstName = artistData.FirstName,
                LastName = artistData.LastName,
                ProfileImage = artistData.ProfileImage,
                Email = artistData.Email,
                ArtistUsername = artistData.ArtistUsername
            };

            return artist;
        }

        public bool Delete(string Id)
        {
            var artist = this.data
                .Artists
                .FirstOrDefault(a => a.Id == Id);

            if (artist != null)
            {
                var arts = data.Arts
                    .Where(a => a.ArtistId == artist.Id)
                    .ToList();

                foreach (var art in arts)
                {
                    data.Arts.Remove(art);
                }

                data.Artists.Remove(artist);

                data.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
