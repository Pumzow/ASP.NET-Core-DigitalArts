using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalArts.Data.Models;
using DigitalArts.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static DigitalArts.Test.Data.Arts;
using static DigitalArts.Test.Data.Artists;
using DigitalArts.Services.Arts;
using DigitalArts.Services.Artist;

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
        public void ViewShouldReturnView()
            => MyController<DigitalArts.Controllers.ArtController>
                .Instance(controller => controller
                    .WithData(TenArts, TenArtists))
                .Calling(a => a.View())
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
            .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ArtViewModel>());

        [Fact]
        public void EditShouldReturnView()
            => MyController<DigitalArts.Controllers.ArtController>
                .Instance(controller => controller
                    .WithData(TenArts))
                .Calling(a => a.Edit(TenArts.First().Id))
                .ShouldReturn()
                .View();
    }
}
