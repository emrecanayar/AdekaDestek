using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdekaDestek.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(UserManager<User> userManager, IMapper mapper) : base(userManager, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            var isEnabledTwoFactor = LoggedInUser.TwoFactorEnabled;
            if (isEnabledTwoFactor == true)
            {
                return RedirectToAction("Card");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Card()
        {
            return View();
        }
    }
}
