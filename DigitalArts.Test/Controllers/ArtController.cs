using System.Linq;
using DigitalArts.Test.Mocks;
using DigitalArts.Data.Models;
using DigitalArts.Services.Arts;
using DigitalArts.Services.Artist;
using DigitalArts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers
{
    public class ArtController
    {
        [Fact]
        public void PostShouldReturnView()
            => MyController<DigitalArts.Controllers.ArtController>
                .Instance()
                .Calling(a => a.Post())
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
            .AndAlso()
                .ShouldReturn()
                .View();
        [Fact]
        public void PostShouldAddArtToDatabase()
        {
            //Arrange
            const string artistId = "ArtistId";
            const string artistFullName = "Artist Artist";

            var artFormModel = new AddArtFormModel
            {
                Artist = artistFullName,
                Tags = null,
                Image = ""
            };

            var user = ClaimsPrincipalUser.User;

            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist { Id = artistId, FirstName = "Artist", LastName = "Artist" });
            data.SaveChanges();

            var artService = new ArtService(data);
            var artistService = new ArtistService(data);

            var artController = new DigitalArts.Controllers.ArtController(artService, artistService);
            artController.ControllerContext = new ControllerContext();
            artController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = artController.Post(artFormModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(1, data.Artists.Count());
        }
        [Fact]
        public void DeleteShouldDeleteArtFromDatabase()
        {
            //Arrange
            const string artId = "ArtId";
            const string artistId = "ArtistId";
            const string artistFullName = "Artist Artist";

            var user = ClaimsPrincipalUser.User;

            using var data = DatabaseMock.Instance;

            data.Arts.Add(new Art { Id = artId, ArtistId = artistId,ArtistFullName = artistFullName });
            data.Artists.Add(new Artist { Id = artistId, FirstName = "Artist", LastName = "Artist" });
            data.SaveChanges();

            var artService = new ArtService(data);
            var artistService = new ArtistService(data);

            var artController = new DigitalArts.Controllers.ArtController(artService, artistService);
            artController.ControllerContext = new ControllerContext();
            artController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = artController.Delete(artId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(0, data.Arts.Count());
        }
        [Fact]
        public void ViewShouldReturnView()
        {
            //Arrange
            const string artId = "ArtId";
            const string artistId = "ArtistId";
            const string artistFullName = "Artist Artist";

            var user = ClaimsPrincipalUser.User;

            using var data = DatabaseMock.Instance;

            data.Arts.Add(new Art { Id = artId, ArtistId = artistId, ArtistFullName = artistFullName });
            data.Artists.Add(new Artist { Id = artistId, FirstName = "Artist", LastName = "Artist" });
            data.SaveChanges();

            var artService = new ArtService(data);
            var artistService = new ArtistService(data);

            var artController = new DigitalArts.Controllers.ArtController(artService, artistService);
            artController.ControllerContext = new ControllerContext();
            artController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = artController.View(artId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditSHouldReturnView()
        {
            //Arrange
            const string artId = "ArtId";
            const string artistId = "ArtistId";

            var user = ClaimsPrincipalUser.User;

            using var data = DatabaseMock.Instance;
            data.Arts.Add(new Art { Id = artId, ArtistId = artistId });
            data.Artists.Add(new Artist { Id = artistId });
            data.SaveChanges();

            var artService = new ArtService(data);
            var artistService = new ArtistService(data);

            var artController = new DigitalArts.Controllers.ArtController(artService, artistService);
            artController.ControllerContext = new ControllerContext();
            artController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = artController.Edit(artId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void LikeShouldLikeArtIfItIsNotYetLiked()
        {
            //Arrange
            const string artId = "ArtId";
            const string artistId = "ArtistId";
            const string artistFullName = "Artist Artist";

            var user = ClaimsPrincipalUser.User;

            var art = new Art { Id = artId, ArtistId = artistId, ArtistFullName = artistFullName };

            using var data = DatabaseMock.Instance;

            data.Arts.Add(art);
            data.Artists.Add(new Artist { Id = artistId, FirstName = "Artist", LastName = "Artist" });
            data.SaveChanges();

            var artService = new ArtService(data);
            var artistService = new ArtistService(data);

            var artController = new DigitalArts.Controllers.ArtController(artService, artistService);
            artController.ControllerContext = new ControllerContext();
            artController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = artController.Like(artId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(1, art.Likes.Count);
        }
        [Fact]
        public void LikeShouldDislikeArtIfItIsAlreadyLiked()
        {
            //Arrange
            const string artId = "ArtId";
            const string artistId = "ArtistId";
            const string artistFullName = "Artist Artist";

            var user = ClaimsPrincipalUser.User;

            var art = new Art { Id = artId, ArtistId = artistId, ArtistFullName = artistFullName };

            using var data = DatabaseMock.Instance;

            data.Arts.Add(art);
            data.Artists.Add(new Artist { Id = artistId, FirstName = "Artist", LastName = "Artist" });
            data.SaveChanges();

            var artService = new ArtService(data);
            var artistService = new ArtistService(data);

            var artController = new DigitalArts.Controllers.ArtController(artService, artistService);
            artController.ControllerContext = new ControllerContext();
            artController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = artController.Like(artId);
            result = artController.Like(artId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(0, art.Likes.Count);
        }
    }
}
