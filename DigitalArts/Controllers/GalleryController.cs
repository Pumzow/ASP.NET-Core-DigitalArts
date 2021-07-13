using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalArts.Controllers
{
    public class GalleryController : Controller
    {
        public ActionResult Arts()
        {
            return View();
        }
    }
}
