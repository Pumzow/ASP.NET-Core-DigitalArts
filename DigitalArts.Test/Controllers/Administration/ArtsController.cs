using DigitalArts.Data.Models;
using DigitalArts.Services.Artist;
using DigitalArts.Services.Arts;
using DigitalArts.Services.Gallery;
using DigitalArts.Test.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace DigitalArts.Test.Controllers.Administration
{
    public class ArtsController
    {
        [Fact]
        public void IndexShouldReturnView()
               => MyController<Areas.Administration.Controllers.ArtsController>
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
        public void DeleteShouldDeleteArtFromDatabase()
        {
            //Arrange
            const string artId = "ArtId";
            const string artistId = "ArtistId";
            const string artistFullName = "Artist Artist";

            var admin = ClaimsPrincipalUser.Admin;

            using var data = DatabaseMock.Instance;

            data.Arts.Add(new Art { Id = artId, ArtistId = artistId, ArtistFullName = artistFullName });
            data.SaveChanges();

            var artService = new ArtService(data);
            var artistService = new ArtistService(data);
            var galleryService = new GalleryService(data, artistService);

            var artController = new Areas.Administration.Controllers.ArtsController(galleryService, artService);
            artController.ControllerContext = new ControllerContext();
            artController.ControllerContext.HttpContext = new DefaultHttpContext { User = admin };
            //Act
            var result = artController.Delete(artId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(0, data.Arts.Count());
        }
    }
}
