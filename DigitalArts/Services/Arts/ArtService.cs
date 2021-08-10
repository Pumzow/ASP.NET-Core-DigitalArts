using System;
using System.Collections.Generic;
using System.Linq;
using DigitalArts.Data;
using DigitalArts.Data.Models;
using DigitalArts.Models.Arts;
using DigitalArts.Services.Gallery;

namespace DigitalArts.Services.Arts
{
    public class ArtService : IArtService
    {
        private readonly DigitalArtsDbContext data;

        public ArtService(DigitalArtsDbContext data)
            => this.data = data;

        public string Post(string ArtistId, string ArtistFullName, string Description, string Tags, string Image)
        {
            var artData = new Art
            {
                ArtistId = ArtistId,
                ArtistFullName = ArtistFullName,
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
                    Likes = a.Likes.Count(),
                    DatePublished = a.DatePublished,
                    Image = a.Image
                })
                .FirstOrDefault();
        public bool Edit(string Id, string Description, string Tags)
        {
            var artData = this.data.Arts.Find(Id);

            if (artData == null)
            {
                return false;
            }

            artData.Description = Description;
            artData.Tags = Tags;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(string Id)
        {
            var artData = this.data.Arts.FirstOrDefault(a => a.Id == Id);

            if (artData != null)
            {

                this.data.Remove(artData);

                this.data.SaveChanges();

                return true;
            }
            return false;
        }

        public int Like(string ArtId, string ArtistId)
        {
            var art = this.data.Arts
                .FirstOrDefault(a => a.Id == ArtId);
            var artist = this.data.Artists
                .FirstOrDefault(a => a.Id == ArtistId);
            var dataLike = this.data.Likes
                .FirstOrDefault(l => l.ArtId == ArtId && l.ArtistId == ArtistId);

            var localLike = new Likes();
            localLike.Art = art;
            localLike.ArtId = ArtId;
            localLike.Artist = artist;
            localLike.ArtistId = ArtistId;

            if (art != null)
            {
                if (localLike != null && dataLike != null)
                {
                    art.Likes.Remove(dataLike);
                }
                else
                {
                    art.Likes.Add(localLike);
                }
            }
            else
            {
                return -1;
            }

            data.SaveChanges();

            return art.Likes.Count();
        }
    }
}
