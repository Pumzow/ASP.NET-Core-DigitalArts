using System.Linq;
using DigitalArts.Data.Models;
using DigitalArts.Services.Bug;
using DigitalArts.Test.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers.Administration
{
    public class BugsController
    {
        [Fact]
        public void IndexShouldReturnView()
               => MyController<Areas.Administration.Controllers.BugsController>
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
        public void DeleteShouldDeleteBugFromDatabase()
        {
            //Arrange
            const int bugId = 12;
            const string bugTitle = "BugTitle";
            const string bugDescription = "BugDescription";

            var admin = ClaimsPrincipalUser.Admin;

            var bug = new Bug { Id = bugId, Title = bugTitle, Description = bugDescription };

            using var data = DatabaseMock.Instance;

            data.Bugs.Add(bug);
            data.SaveChanges();

            var bugService = new BugService(data);

            var artistsController = new Areas.Administration.Controllers.BugsController(bugService);
            artistsController.ControllerContext = new ControllerContext();
            artistsController.ControllerContext.HttpContext = new DefaultHttpContext { User = admin };
            //Act
            var result = artistsController.Delete(bugId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(0, data.Bugs.Count());
        }
    }
}
