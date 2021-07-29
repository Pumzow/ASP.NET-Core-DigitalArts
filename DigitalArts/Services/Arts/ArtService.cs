using System;
using System.Linq;
using DigitalArts.Data;
using DigitalArts.Data.Models;

namespace DigitalArts.Services.Arts
{
    public class ArtService : IArtService
    {
        private readonly DigitalArtsDbContext data;

        public ArtService(DigitalArtsDbContext data)
            => this.data = data;

        public string Post(string ArtistId, string Description, string Tags, string Image)
        {
            var artData = new Art
            {
                ArtistId = ArtistId,
                Description = Description,
                Tags = Tags,
                DatePublished = DateTime.UtcNow,
                Image = Image
            };

            this.data.Arts.Add(artData);
            this.data.SaveChanges();

            return artData.Id;
        }

        public ArtViewServiceModel View(string Id)
            => this.data
                .Arts
                .Where(a => a.Id == Id)
                .Select(a => new ArtViewServiceModel
                {
                    Id = a.Id,
                    ArtistId = a.ArtistId,
                    Description = a.Description,
                    Tags = a.Tags,
                    Likes = a.Likes,
                    Dislikes = a.Dislikes,
                    DatePublished = a.DatePublished,
                    Image = a.Image
                })
                .FirstOrDefault();
    }
}
