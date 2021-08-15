using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers
{
    public class BugController
    {
        [Fact]
        public void BugShouldReturnView()
               => MyController<DigitalArts.Controllers.BugController>
                .Instance()
                .Calling(a => a.Post())
            .ShouldHave()
                .ValidModelState()
            .AndAlso()
                .ShouldReturn()
                .View();
    }
}
