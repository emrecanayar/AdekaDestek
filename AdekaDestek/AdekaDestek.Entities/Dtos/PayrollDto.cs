using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class PayrollDto
    {
        [DisplayName("Bordro Yıl")]
        public string Year { get; set; }
        [DisplayName("Bordro Ay")]
        public string Month { get; set; }
    }
}
