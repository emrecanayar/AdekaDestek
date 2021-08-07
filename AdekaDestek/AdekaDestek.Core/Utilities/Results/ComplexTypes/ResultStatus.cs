using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Core.Utilities.Results.ComplexTypes
{
    //Result yapısı sonuçları - Kullanıcıyı ve MVC katmanını bilgilendirmek amaçlı oluşturulmuştur.
    public enum ResultStatus
    {
        Success = 0,
        Error = 1,
        Warning = 2,
        Info = 3

    }
}
