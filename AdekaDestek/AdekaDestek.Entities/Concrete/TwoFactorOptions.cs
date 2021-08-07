using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Concrete
{
    public class TwoFactorOptions
    {
        public string SendGridApiKey { get; set; }
        [DisplayName("Geçen Süre")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int CodeTimeExpire { get; set; }
    }
}
