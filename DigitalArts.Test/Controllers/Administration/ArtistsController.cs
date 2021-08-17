using System.Linq;
using DigitalArts.Data.Models;
using DigitalArts.Services.Artist;
using DigitalArts.Test.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers.Administration
{
    public class ArtistsController
    {
        [Fact]
        public void IndexShouldReturnView()
               => MyController<Areas.Administration.Controllers.ArtistsController>
                .Instance()
                .Calling(a => a.Index())
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
            .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void DeleteShouldDeleteArtistFromDatabase()
        {
            //Arrange
            const string artId = "ArtId";
            const string artistId = "ArtistId";
            const string artistFullName = "Artist Artist";

            var admin = ClaimsPrincipalUser.Admin;

            var artist = new Artist { Id = artistId, FirstName = "Artist", LastName = "Artist" };

            using var data = DatabaseMock.Instance;

            data.Arts.Add(new Art { Id = artId, ArtistId = artistId, ArtistFullName = artistFullName });
            data.Artists.Add(artist);
            data.SaveChanges();

            var artistService = new ArtistService(data);

            var artistsController = new Areas.Administration.Controllers.ArtistsController(artistService);
            artistsController.ControllerContext = new ControllerContext();
            artistsController.ControllerContext.HttpContext = new DefaultHttpContext { User = admin };
            //Act
            var result = artistsController.Delete(artistId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(0, data.Artists.Count());
            Assert.Equal(0, data.Arts.Count());
        }
    }
}
