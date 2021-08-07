using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.Results.Abstract;
using AdekaDestek.Entities.Dtos;

namespace AdekaDestek.HelpDeskAPI.Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> UpdatePasswordforHelpDeskAsync(string password, string email, string modifiedByName);
        Task<IResult> DeleteUserforHelpDeskAsync(string email);
        Task<IResult> UpdateUserforHelpDeskAsync(UserUpdateDto userUpdateDto);
    }
}
