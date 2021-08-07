using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.Entities.Abstract;

namespace AdekaDestek.Entities.Dtos
{
    public class TwoFactorSmsDto : DtoGetBase
    {
        [DisplayName("Cep Telefonu")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Doğrulama Kodunuz")]
        [Required(ErrorMessage = "Doğrulama kodu boş geçilemez.")]
        [StringLength(8, ErrorMessage = "Doğrulama kodunuz en fazla 8 haneli olabilir")]
        public string VerificationCode { get; set; }
    }
}
