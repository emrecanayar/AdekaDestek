using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Core.Utilities.Results.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        //Result yapısı taşınırken yanında Data'da taşımak için.
        public T Data { get; }
    }
}
