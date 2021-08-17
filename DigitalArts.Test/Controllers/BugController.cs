using System.Linq;
using DigitalArts.Models.Bug;
using DigitalArts.Services.Bug;
using DigitalArts.Test.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers
{
    public class BugController
    {
        [Fact]
        public void ReportShouldReturnView()
               => MyController<DigitalArts.Controllers.BugController>
                .Instance()
                .Calling(a => a.Report())
            .ShouldHave()
                .ValidModelState()
            .AndAlso()
                .ShouldReturn()
                .View();
        [Fact]
        public void ReportShouldAddbugToDatabase()
        {
            //Arrange
            const string bugTitle = "BugTitle";
            const string bugDescription = "BugCDescription";

            var addBugFormModel = new AddBugFormModel
            {
                Title = bugTitle,
                Description = bugDescription
            };

            var user = ClaimsPrincipalUser.User;

            using var data = DatabaseMock.Instance;

            var bugService = new BugService(data);

            var bugController = new DigitalArts.Controllers.BugController(bugService);
            bugController.ControllerContext = new ControllerContext();
            bugController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = bugController.Report(addBugFormModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(1, data.Bugs.Count());
        }
    }
}
