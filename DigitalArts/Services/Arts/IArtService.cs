using DigitalArts.Models.Artist;

namespace DigitalArts.Services.Arts
{
    public interface IArtService
    {
        string Post(
            string ArtistId,
            string ArtistFullName,
            string Description,
            string Tags,
            string Image);

        ArtViewServiceModel View(string Id);

        bool Edit(
            string Id,
            string Description,
            string Tags);
        bool Delete(
            string Id);

        int Like(
            string ArtId,
            string ArtistId);

        ArtistArtsQueryModel AllById(
            string Id,
            int artsPerPage,
            int currentPage);
    }
}
