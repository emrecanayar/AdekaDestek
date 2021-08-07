using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Concrete
{
    public class WebSiteInfo
    {
        [DisplayName("Site Adı/Başlığı")]
        public string Title { get; set; }
        [DisplayName("Menü Üzerindeki Site Adı/Başlığı")]
        public string MenuTitle { get; set; }
        [DisplayName("Seo Açıklama")]
        public string SeoDescription { get; set; }
        [DisplayName("Seo Etiketleri")]
        public string SeoTags { get; set; }
        [DisplayName("Seo Yazar")]
        public string SeoAuthor { get; set; }
    }
}
