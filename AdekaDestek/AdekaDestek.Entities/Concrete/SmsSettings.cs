using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Concrete
{
    public class SmsSettings
    {
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DataType(DataType.Password, ErrorMessage = "{0} alanı şifre formatında olmalıdır.")]
        public string Password { get; set; }
        [DisplayName("Key")]
        public string Key { get; set; }
        [DisplayName("Gizli Anahtar")]
        public string SecretKey { get; set; }
        [DisplayName("Kullanıcı Başlığı")]
        public string Sender { get; set; }

    }
}
