using Microsoft.AspNetCore.Mvc;

namespace DigitalArts.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
