using DigitalArts.Data.Models;
using DigitalArts.Services.Statistics;
using DigitalArts.Test.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DigitalArts.Test.Controllers.Api
{
    public class StatisticsApiController
    {
        [Fact]
        public void GetStatisticsShoudReturnStatisticsCount()
        {
            //Arrange
            var admin = ClaimsPrincipalUser.Admin;

            using var data = DatabaseMock.Instance;

            data.Arts.Add(new Art {});
            data.Artists.Add(new Artist {});
            data.Artists.Add(new Artist {});
            data.Notiffications.Add(new Notiffication {});
            data.Bugs.Add(new Bug {});
            data.SaveChanges();

            var statisticsService = new StatisticsService(data);

            var statisticsApiController = new Areas.Administration.Controllers.Api.StatisticsApiController(statisticsService);
            statisticsApiController.ControllerContext = new ControllerContext();
            statisticsApiController.ControllerContext.HttpContext = new DefaultHttpContext { User = admin };
            //Act
            var result = statisticsApiController.GetStatistics();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<StatisticsServiceModel>(result);
            Assert.Equal(1, result.TotalArts);
            Assert.Equal(2, result.TotalArtists);
            Assert.Equal(1, result.TotalNotiffications);
            Assert.Equal(1, result.TotalBugs);
        }
    }
}
