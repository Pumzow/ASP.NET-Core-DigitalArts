using System;
using System.Collections.Generic;
using System.Linq;
using DigitalArts.Data;
using DigitalArts.Data.Models;
using DigitalArts.Models.Arts;
using DigitalArts.Services.Artist;

namespace DigitalArts.Services.Gallery
{
    public class GalleryService : IGalleryService
    {
        private readonly DigitalArtsDbContext data;
        private readonly IArtistService artists;

        public GalleryService(DigitalArtsDbContext data, IArtistService artists)
        {
            this.data = data;
            this.artists = artists;
        }

        public GalleryQueryServiceModel All(int artsPerPage, int currentPage, GallerySorting sorting, string artistFullName, string searchTag)
        {
            var artsQuery = this.data.Arts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(artistFullName))
            {
                artsQuery = artsQuery.Where(c => c.ArtistFullName == artistFullName);
            }

            if (!string.IsNullOrWhiteSpace(searchTag))
            {
                artsQuery = artsQuery.Where(a =>
                    a.Tags.ToLower().Contains(searchTag.ToLower()));
            }

            artsQuery = sorting switch
            {
                GallerySorting.DatePublished => artsQuery.OrderByDescending(c => c.DatePublished),
                GallerySorting.Likes => artsQuery.OrderByDescending(a => a.Likes.Count())
            };

            var totalArts = artsQuery.Count();

            var arts = GetArts(artsQuery
                .Skip((currentPage - 1) * artsPerPage)
                .Take(artsPerPage));

            return new GalleryQueryServiceModel
            {
                TotalArts = totalArts,
                CurrentPage = currentPage,
                ArtsPerPage = artsPerPage,
                Arts = arts
            };
        }

        private IEnumerable<GalleryArtServiceModel> GetArts(IQueryable<Art> artQuery)
        { 
            var arts = artQuery
                .Select(a => new GalleryArtServiceModel
                {
                    Id = a.Id,
                    ArtistId = a.ArtistId,
                    Tags = a.Tags,
                    Likes = a.Likes.Count(),
                    DatePublished = a.DatePublished,
                    Image = a.Image
                })
                .ToList();

            foreach (var art in arts)
            {
                var artistFullName = this.artists.FullNameByUser(art.ArtistId);
                if (String.IsNullOrWhiteSpace(artistFullName))
                {
                    //this.ModelState.AddModelError(nameof(artistFullName), "Artist does not have a name.");
                }

                art.ArtistFullName = artistFullName;
            }

            return arts;
        }
    }
}
