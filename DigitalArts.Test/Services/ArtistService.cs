using DigitalArts.Data.Models;
using DigitalArts.Services.Artist;
using DigitalArts.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DigitalArts.Test.Services
{
    public static class ArtistService
    {
        [Fact]
        public static void FullNameByUserShouldReturnArtistFullname()
        {

            //Arrange
            const string artistId = "ArtistId";
            const string artistFullName = "Artist Artist";

            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist { Id = artistId, FirstName = "Artist", LastName = "Artist" });
            data.SaveChanges();

            var artistService = new DigitalArts.Services.Artist.ArtistService(data);

            //Act
            var result = artistService.FullNameByUser(artistId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(artistFullName, result);
        }
        [Fact]
        public static void IdByUsernameShouldReturnArtistId()
        {

            //Arrange
            const string artistId = "ArtistId";
            const string artistUsername = "ArtistUsername";

            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist { Id = artistId, UserName = artistUsername });
            data.SaveChanges();

            var artistService = new DigitalArts.Services.Artist.ArtistService(data);

            //Act
            var result = artistService.IdByUsername(artistUsername);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(artistId, result);
        }
        [Fact]
        public static void IdByUserShouldReturnArtistId()
        {

            //Arrange
            const string artistId = "ArtistId";

            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist { Id = artistId});
            data.SaveChanges();

            var artistService = new DigitalArts.Services.Artist.ArtistService(data);

            //Act
            var result = artistService.IdByUser(artistId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(artistId, result);
        }
        [Fact]
        public static void AllByNameShouldReturnArtistsWithFullnames()
        {

            //Arrange
            const string artist1Fullname = "Artist 1";
            const string artist2Fullname = "Artist 2";

            var artists = new List<string> { artist1Fullname, artist2Fullname};

            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist { FirstName = "Artist", LastName = "1" });
            data.Artists.Add(new Artist { FirstName = "Artist", LastName = "2" });
            data.SaveChanges();

            var artistService = new DigitalArts.Services.Artist.ArtistService(data);

            //Act
            var result = artistService.AllByName();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<string>>(result);
            Assert.Equal(artists, result);
        }
        [Fact]
        public static void AllBShouldReturnAllArtists()
        {

            //Arrange
            var artist1 = new ArtistServiceModel { Id = "1", FirstName = "Artist", LastName="1" , ProfileImage = "Image", Email="Email", ArtistUsername="Username"};
            var artist2 = new ArtistServiceModel { Id = "2", FirstName = "Artist", LastName="2" , ProfileImage = "Image", Email="Email", ArtistUsername="Username"};
            var artist3 = new ArtistServiceModel { Id = "3", FirstName = "Artist", LastName="3", ProfileImage = "Image", Email = "Email", ArtistUsername = "Username" };

            var artists = new List<ArtistServiceModel>();

            artists.Add(artist1);
            artists.Add(artist2);
            artists.Add(artist3);

            artists = artists.OrderBy(a => a.FirstName).ThenBy(a => a.LastName).ToList();

            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist {Id = "1", FirstName = "Artist", LastName = "1", ProfileImage = "Image", Email="Email", ArtistUsername="Username" });
            data.Artists.Add(new Artist {Id = "2", FirstName = "Artist", LastName = "2", ProfileImage = "Image", Email = "Email", ArtistUsername = "Username" });
            data.Artists.Add(new Artist { Id = "3", FirstName = "Artist", LastName = "3", ProfileImage = "Image", Email = "Email", ArtistUsername = "Username" });
            data.SaveChanges();

            var artistService = new DigitalArts.Services.Artist.ArtistService(data);

            //Act
            var result = artistService.All();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(artists.Count(), result.Count());
        }
        [Fact]
        public static void UserFullDetailsShouldReturnArtistFullDetails()
        {

            //Arrange
            var artist1 = new ArtistServiceModel { Id = "1", FirstName = "Artist", LastName="1" , ProfileImage = "Image", Email="Email", ArtistUsername="Username"};

            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist {Id = "1", FirstName = "Artist", LastName = "1", ProfileImage = "Image", Email="Email", ArtistUsername="Username" });
            data.SaveChanges();

            var artistService = new DigitalArts.Services.Artist.ArtistService(data);

            //Act
            var result = artistService.UserFullDetails(artist1.Id);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(artist1.Id, result.Id);
            Assert.Equal(artist1.FirstName, result.FirstName);
            Assert.Equal(artist1.LastName, result.LastName);
            Assert.Equal(artist1.ProfileImage, result.ProfileImage);
            Assert.Equal(artist1.Email, result.Email);
            Assert.Equal(artist1.ArtistUsername, result.ArtistUsername);
        }
        [Fact]
        public static void DeleteShouldReturnTrue()
        {

            //Arrange
            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist { Id = "CorrectId"});
            data.SaveChanges();

            var artistService = new DigitalArts.Services.Artist.ArtistService(data);

            //Act
            var result = artistService.Delete("CorrectId");
            //Assert
            Assert.True(result);
        }
        [Fact]
        public static void DeleteShouldReturnFalse()
        {

            //Arrange
            using var data = DatabaseMock.Instance;

            data.Artists.Add(new Artist { });
            data.SaveChanges();

            var artistService = new DigitalArts.Services.Artist.ArtistService(data);

            //Act
            var result = artistService.Delete("WrongId");
            //Assert
            Assert.False(result);
        }
    }
}
