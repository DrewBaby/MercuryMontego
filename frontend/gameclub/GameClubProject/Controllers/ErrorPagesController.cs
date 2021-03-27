using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameClubProject.Controllers
{
    public class ErrorPagesController : Controller
    {
        [HttpGet("ForbiddenError")]
        public IActionResult ForbiddenError()
        {
            return View("ForbiddenError");
        }
    }
}
