using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Business.Adapters.SmsService
{
    public interface ISmsService
    {
        public string Send(string cellPhone);
        public string SendAsist(string text, string cellPhone);
        public string XMLPOST(string postAddress, string xmlData);
    }
}
