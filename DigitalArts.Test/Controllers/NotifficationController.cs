using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers
{
    public class NotifficationController
    {
        [Fact]
        public void NotifficationsShouldReturnView()
               => MyController<DigitalArts.Controllers.NotifficationsController>
                .Instance()
                .Calling(a => a.Index())
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
            .AndAlso()
                .ShouldReturn()
                .View();
    }
}
