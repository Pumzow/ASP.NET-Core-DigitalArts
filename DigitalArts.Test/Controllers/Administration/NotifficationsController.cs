using System.Linq;
using DigitalArts.Areas.Models.Notiffication;
using DigitalArts.Data.Models;
using DigitalArts.Services.Notiffication;
using DigitalArts.Test.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers.Administration
{
    public class NotifficationsController
    {
        [Fact]
        public void IndexShouldReturnView()
               => MyController<Areas.Administration.Controllers.NotifficationsController>
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
        public void PostShouldReturnView()
               => MyController<Areas.Administration.Controllers.NotifficationsController>
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
        public void PostShouldAddNotifficationToDatabase()
        {
            //Arrange
            const string notifficationTitle = "NotifficationTitle";
            const string notifficationContent = "NotifficationContent";

            var addNotifficationFormModel = new AddNotifficationFormModel
            {
                Title = notifficationTitle,
                Content = notifficationContent
            };

            var admin = ClaimsPrincipalUser.Admin;

            using var data = DatabaseMock.Instance;

            var notifficationService = new NotifficationService(data);

            var artController = new Areas.Administration.Controllers.NotifficationsController(notifficationService);
            artController.ControllerContext = new ControllerContext();
            artController.ControllerContext.HttpContext = new DefaultHttpContext { User = admin };
            //Act
            var result = artController.Post(addNotifficationFormModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(1, data.Notiffications.Count());
        }
        [Fact]
        public void DeleteShouldDeleteNotifficationFromDatabase()
        {
            //Arrange
            const int notifficationId = 15;
            const string notifficationTitle = "NotifficationTitle";
            const string notifficationContent = "NotifficationContent";

            var admin = ClaimsPrincipalUser.Admin;

            var notiffication = new Notiffication { Id = notifficationId, Title = notifficationTitle, Content = notifficationContent };

            using var data = DatabaseMock.Instance;

            data.Notiffications.Add(notiffication);
            data.SaveChanges();

            var notifficationService = new NotifficationService(data);

            var artistsController = new Areas.Administration.Controllers.NotifficationsController(notifficationService);
            artistsController.ControllerContext = new ControllerContext();
            artistsController.ControllerContext.HttpContext = new DefaultHttpContext { User = admin };
            //Act
            var result = artistsController.Delete(notifficationId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(0, data.Notiffications.Count());
        }
    }
}
