using System;
using System.Collections.Generic;
using DigitalArts.Controllers;
using DigitalArts.Test.Data;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using DigitalArts.Services.Home;

using static DigitalArts.Test.Data.Arts;
using static DigitalArts.Test.Data.Artists;
using DigitalArts.Models.Home;
using DigitalArts.Models;

namespace DigitalArts.Test.Controllers
{
    public class GalleryController
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<DigitalArts.Controllers.GalleryController>
                .Instance()
                .Calling(g => g.Index(new GalleryArtsQueryModel
                {
                    CurrentPage = 0,
                    Sorting = 0,
                    ArtistFullName = null,
                    SearchTag = null
                }))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<GalleryArtsQueryModel>());
    }
}
