using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DigitalArts.Test.Controllers
{
    public class PrivacyPolicyController
    {
        [Fact]
        public void IndexShouldReturnCorrectView()
            => MyController<DigitalArts.Controllers.PrivacyPolicyController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();
    }
}
