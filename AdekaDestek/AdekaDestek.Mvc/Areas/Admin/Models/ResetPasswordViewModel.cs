using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdekaDestek.Mvc.Areas.Admin.Models
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "E-posta adresiniz")]
        [Required(ErrorMessage = "E-posta alanı gereklidir")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = " Yeni Şifreniz")]
        [Required(ErrorMessage = "Şifre alanı gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakterden oluşmalıdır.")]
        public string PasswordNew { get; set; }

        public string Link { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
