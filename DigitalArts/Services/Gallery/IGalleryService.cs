using DigitalArts.Models.Arts;

namespace DigitalArts.Services.Gallery
{
    public interface IGalleryService
    {
        GalleryQueryServiceModel All(
            int artsPerPage,
            int currentPage,
            GallerySorting sorting,
            string artist,
            string searchTag
            );
    }
}
