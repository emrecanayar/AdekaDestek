using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;

namespace AdekaDestek.Mvc.Areas.Admin.Models
{
    public class TwoFactorSmsConfirmAjaxViewModel
    {
        public TwoFactorSmsDto twoFactorSmsDto { get; set; }
        public string TwoFactorSmsConfirmPartial { get; set; }
    }
}
