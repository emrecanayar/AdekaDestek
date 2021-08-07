using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.ComplexTypes;

namespace AdekaDestek.Mvc.Areas.Admin.Models
{
    public class AuthenticatorViewModel
    {
        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }

        [Display(Name = "Doğrulama Kodunuz")]
        [Required(ErrorMessage = "Doğrulama Kodu gereklidir.")]
        public string VerificationCode { get; set; }

        [Display(Name = "İki Adımlı Kimlik Doğrulama Tipini Seçiniz")]
        public TwoFactorType TwoFactorType { get; set; }
    }
}
