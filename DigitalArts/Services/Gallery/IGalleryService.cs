using DigitalArts.Models.Arts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
