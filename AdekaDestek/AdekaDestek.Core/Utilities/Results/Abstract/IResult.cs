using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.Results.ComplexTypes;

namespace AdekaDestek.Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        //Mvc katmanına bilgi vermek amaçlı
        public ResultStatus ResultStatus { get; }
        //Kullanıcıya veya diğer katmanlara mesaj taşımak amaçlı.
        public string Message { get; }
        //Kullanıcya veya diğer katmanlara hatayı taşımak amaçlı.
        public Exception Exception { get; }
    }
}
