using DigitalArts.Data.Models;
using DigitalArts.Models;
using DigitalArts.Services.Artist;
using DigitalArts.Services.Arts;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static DigitalArts.Test.Data.Artists;

namespace DigitalArts.Test.Controllers
{
    public class ArtistController
    {

        [Fact]
        public void ViewShouldReturnView()
        {
            var artistsData = TenArtists;

            //var artistController = new DigitalArts.Controllers.ArtistController(ArtistService, ArtService);
        }
    }
}
