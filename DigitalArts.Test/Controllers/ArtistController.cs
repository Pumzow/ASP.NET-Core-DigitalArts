using DigitalArts.Test.Mocks;
using DigitalArts.Data.Models;
using DigitalArts.Services.Arts;
using DigitalArts.Services.Artist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace DigitalArts.Test.Controllers
{
    public class ArtistController
    {
        [Fact]
        public void ViewShouldReturnView()
        {
            //Arrange
            const string artistId = "ArtistId";
            const string artistUsername = "ArtistUsername";

            var user = ClaimsPrincipalUser.User;

            var data = DatabaseMock.Instance;
            data.Artists.Add(new Artist { Id = artistId, UserName = artistUsername });
            data.SaveChanges();

            var artService = new ArtService(data);
            var artistService = new ArtistService(data);

            var artistController = new DigitalArts.Controllers.ArtistController(artistService, artService);
            artistController.ControllerContext = new ControllerContext();
            artistController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = artistController.View(artistUsername);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
