using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class PayrollListDto
    {
        public IList<PayrollPersonelInfoDto> PayrollPersonelInfoList { get; set; }
        public IList<PayrollAllPaymentDto> PayrollAllPaymentList { get; set; }

    }
}
