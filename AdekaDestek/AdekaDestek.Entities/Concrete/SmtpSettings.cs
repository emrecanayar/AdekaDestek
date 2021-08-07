using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Concrete
{
    public class SmtpSettings
    {
        [DisplayName("Sunucu")]
        public string Server { get; set; }
        [DisplayName("Port")]
        public int Port { get; set; }
        [DisplayName("Gönderen Adı")]
        public string SenderName { get; set; }
        [DisplayName("Gönderen E-posta")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} alanı e-posta formatında olmalıdır.")]
        public string SenderEmail { get; set; }
        [DisplayName("Kullanıcı Adı/E-Posta")]
        public string Username { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DataType(DataType.Password, ErrorMessage = "{0} alanı şifre formatında olmalıdır.")]
        public string Password { get; set; }
    }
}
