using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using AdekaDestek.Entities.Dtos;
using AdekaDestek.Mvc.SapServices.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace AdekaDestek.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PayrollController : BaseController
    {
        private readonly IPayrollService _payrollService;
        private readonly IToastNotification _toastNotification;
        public PayrollController(UserManager<User> userManager, IMapper mapper, IPayrollService payrollService, IToastNotification toastNotification) : base(userManager, mapper)
        {
            _payrollService = payrollService;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult SearchPayroll()
        {
            if (LoggedInUser.TwoFactorEnabled == false)
            {
                return RedirectToAction("AccessDenied");
            }
            else
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult SearchPayroll(PayrollDto payrollDto)
        {
            TempData["period"] = $"{payrollDto.Month}.{payrollDto.Year}";
            TempData["sapEmployeeNo"] = LoggedInUser.SapEmployeeNo;

            return RedirectToAction("GetPayroll");


        }
        [HttpGet]
        public IActionResult GetPayroll()
        {
            string period = TempData["period"].ToString();
            string sapEmployeeNo = TempData["sapEmployeeNo"].ToString();

            var payroll = _payrollService.GetPayrollDetailsAsync(period, sapEmployeeNo);

            if (payroll.Result.ResultStatus == Core.Utilities.Results.ComplexTypes.ResultStatus.Success)
            {
                return View(payroll.Result.Data);
            }

            else
            {
                _toastNotification.AddErrorToastMessage($"Belirttiğiniz döneme ait bordro bulunamadı.", new ToastrOptions
                {

                    Title = "Başarısız İşlem!"
                });
                return RedirectToAction("SearchPayroll");

            }
        }



        [HttpGet]
        public IActionResult GetPayrollDeactive()
        {
            return View();

        }

        [HttpGet]
        public IActionResult GetPayroll2()
        {
            return View();

        }
    }
}
