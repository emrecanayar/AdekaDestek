using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Core.Entities.Concrete
{
    public class MvcErrorModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public int StatusCode { get; set; }
    }
}
