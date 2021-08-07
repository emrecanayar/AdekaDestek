using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Core.Utilities.ComplexTypes
{
    public enum TwoFactorType
    {
        [Display(Name = "Hiç Biri")]
        None = 0,
        [Display(Name = "Sms ile kimlik doğrulama")]
        Phone = 1,
        [Display(Name = "Google / Microsoft Authencicator ile kimlik doğrulama")]
        MicrosoftGoogle = 2,
    }
}
