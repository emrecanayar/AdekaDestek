using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.Results.Abstract;
using AdekaDestek.Entities.Concrete;

namespace AdekaDestek.Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<User>> GetAllAsync();
        Task<IDataResult<User>> GetAsync(string userName);
        Task<IResult> UpdatePasswordforInfiniAsync(string password, string userName, string modifiedByName);
        Task<IResult> UpdatePasswordforAdekaAsync(string password, string email, string modifiedByName);
      
    }
}
