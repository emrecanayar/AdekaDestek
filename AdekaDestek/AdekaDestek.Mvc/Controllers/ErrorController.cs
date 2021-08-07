using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdekaDestek.Mvc.Controllers
{
    [AllowAnonymous]
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public ViewResult NotFoundPage()
        {
            return View();
        }

        [Route("400")]
        public ViewResult BadRequestPage()
        {
            return View();
        }

        [Route("401")]
        public ViewResult UnauthorizedPage()
        {
            return View();
        }

        [Route("503")]
        public ViewResult ServiceUnavailable()
        {
            return View();
        }

        [Route("504")]
        public ViewResult GatewayTimeout()
        {
            return View();
        }

    }
}
