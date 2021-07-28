using DigitalArts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
