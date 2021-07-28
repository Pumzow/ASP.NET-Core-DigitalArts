namespace DigitalArts.Services.Arts
{
    public interface IArtService
    {
        string Post(
            string ArtistId,
            string Description,
            string Tags,
            string Image);

        ArtViewServiceModel View(

            );
    }
}
