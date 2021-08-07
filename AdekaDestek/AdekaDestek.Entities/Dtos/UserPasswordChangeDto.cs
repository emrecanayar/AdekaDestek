using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Şu Anki Şifreniz")]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifreniz")]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifreniz Tekrarı")]
        [Compare("NewPassword", ErrorMessage = "Girdiğiniz yeni şifreler birbirleriyle uyuşmuyor.")]
        public string RepeatPassword { get; set; }
    }
}
