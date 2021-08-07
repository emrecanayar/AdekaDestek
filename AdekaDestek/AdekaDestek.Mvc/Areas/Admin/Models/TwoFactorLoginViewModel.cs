using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.ComplexTypes;

namespace AdekaDestek.Mvc.Areas.Admin.Models
{
    public class TwoFactorLoginViewModel
    {
        [Display(Name = "Doğrulama Kodunuz")]
        [Required(ErrorMessage = "Doğrulama kodu boş geçilemez.")]
        [StringLength(8, ErrorMessage = "Doğrulama kodunuz en fazla 8 haneli olabilir")]
        public string VerificationCode { get; set; }
        public bool isRememberMe { get; set; }
        public bool isRecoverCode { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
    }
}
