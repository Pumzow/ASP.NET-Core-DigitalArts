using DigitalArts.Areas.Administration;
using DigitalArts.Areas.Administration.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers
{
    public class AdministrationController
    {
        [Fact]
        public void ArtistsShouldReturnView()
               => MyController<ArtistsController>
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
        public void ArtsShouldReturnView()
               => MyController<ArtsController>
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
