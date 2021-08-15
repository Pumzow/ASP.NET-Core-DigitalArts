using DigitalArts.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static DigitalArts.Test.Data.Query;

namespace DigitalArts.Test.Controllers
{
    public class GalleryController
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<DigitalArts.Controllers.GalleryController>
                .Instance()
                .Calling(g => g.Index(GalleryQuery))
            .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
            .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GalleryArtsQueryModel>());
    }
}
