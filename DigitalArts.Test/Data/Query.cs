using DigitalArts.Models;

namespace DigitalArts.Test.Data
{
    public static class Query
    {
        public static GalleryArtsQueryModel GalleryQuery
              => new GalleryArtsQueryModel
              {
                  CurrentPage = 0,
                  Sorting = 0,
                  ArtistFullName = null,
                  SearchTag = null
              };
    }
}
