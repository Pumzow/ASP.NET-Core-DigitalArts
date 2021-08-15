using DigitalArts.Controllers;
using DigitalArts.Models.Home;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexViewModel>());
        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
