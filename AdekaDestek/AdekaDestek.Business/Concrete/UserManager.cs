using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Business.Abstract;
using AdekaDestek.Core.Entities.Concrete;
using AdekaDestek.Core.Utilities.Results.Abstract;
using AdekaDestek.Core.Utilities.Results.Concrete;
using AdekaDestek.Entities.Concrete;
using ProgrammersBlog.DataAccess.Abstract;

namespace AdekaDestek.Business.Concrete
{
    public class UserManager : ManagerBase, IUserService
    {

        public UserManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<IDataResult<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<User>> GetAsync(string userName)
        {
            var user = await UnitOfWork.Users.GetAsync(u => u.InfiniUserName == userName);

            if (user != null)
            {
                return new DataResult<User>(Core.Utilities.Results.ComplexTypes.ResultStatus.Success, data: user);

            }
            else
            {
                return new DataResult<User>(Core.Utilities.Results.ComplexTypes.ResultStatus.Error, data: null);
            }
        }

        public async Task<IResult> UpdatePasswordforAdekaAsync(string password, string email, string modifiedByName)
        {
            var oldUser = await UnitOfWork.Users.GetAsync(u => u.Email == email);
            if (oldUser == null)
            {
                return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Error, messages: $"Belirttiğiniz e-postaya ait kullanıcı bulunanamıştır.");

            }
            var user = new User();
            user.Id = oldUser.Id;
            user.FirstName = oldUser.FirstName;
            user.LastName = oldUser.LastName;
            user.SapUserName = oldUser.SapUserName;
            user.SapEmployeeNo = oldUser.SapEmployeeNo;
            user.InfiniUserName = oldUser.InfiniUserName;
            user.TwoFactorType = oldUser.TwoFactorType;
            user.CreatedDate = oldUser.CreatedDate;
            user.ModifiedDate = DateTime.Now;
            user.CreatedByName = oldUser.CreatedByName;
            user.ModifiedByName = modifiedByName;
            user.IsActive = oldUser.IsActive;
            user.UserName = oldUser.UserName;
            user.NormalizedUserName = oldUser.NormalizedUserName;
            user.Email = oldUser.Email;
            user.NormalizedEmail = oldUser.NormalizedEmail;
            user.EmailConfirmed = oldUser.EmailConfirmed;
            user.PasswordHash = password;
            user.SecurityStamp = oldUser.SecurityStamp;
            user.ConcurrencyStamp = oldUser.ConcurrencyStamp;
            user.PhoneNumber = oldUser.PhoneNumber;
            user.PhoneNumberConfirmed = oldUser.PhoneNumberConfirmed;
            user.TwoFactorEnabled = oldUser.TwoFactorEnabled;
            user.LockoutEnd = oldUser.LockoutEnd;
            user.LockoutEnabled = oldUser.LockoutEnabled;
            user.AccessFailedCount = oldUser.AccessFailedCount;

            await UnitOfWork.Users.UpdateAsync(user);
            await UnitOfWork.SaveAsync();
            return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Success, messages: $"Kullanıcının şifresi başarıyla güncellenmiştir");
        }

        public async Task<IResult> UpdatePasswordforInfiniAsync(string password, string userName, string modifiedByName)
        {
            var oldUser = await UnitOfWork.Users.GetAsync(u => u.InfiniUserName == userName);
            if (oldUser == null)
            {
                return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Error, messages: $"Belirttiğiniz infini kullanıcı adına ait kullanıcı bulunanamıştır.");

            }
            var user = new User();
            user.Id = oldUser.Id;
            user.FirstName = oldUser.FirstName;
            user.LastName = oldUser.LastName;
            user.SapUserName = oldUser.SapUserName;
            user.SapEmployeeNo = oldUser.SapEmployeeNo;
            user.InfiniUserName = oldUser.InfiniUserName;
            user.TwoFactorType = oldUser.TwoFactorType;
            user.CreatedDate = oldUser.CreatedDate;
            user.ModifiedDate = DateTime.Now;
            user.CreatedByName = oldUser.CreatedByName;
            user.ModifiedByName = modifiedByName;
            user.IsActive = oldUser.IsActive;
            user.UserName = oldUser.UserName;
            user.NormalizedUserName = oldUser.NormalizedUserName;
            user.Email = oldUser.Email;
            user.NormalizedEmail = oldUser.NormalizedEmail;
            user.EmailConfirmed = oldUser.EmailConfirmed;
            user.PasswordHash = password;
            user.SecurityStamp = oldUser.SecurityStamp;
            user.ConcurrencyStamp = oldUser.ConcurrencyStamp;
            user.PhoneNumber = oldUser.PhoneNumber;
            user.PhoneNumberConfirmed = oldUser.PhoneNumberConfirmed;
            user.TwoFactorEnabled = oldUser.TwoFactorEnabled;
            user.LockoutEnd = oldUser.LockoutEnd;
            user.LockoutEnabled = oldUser.LockoutEnabled;
            user.AccessFailedCount = oldUser.AccessFailedCount;

            await UnitOfWork.Users.UpdateAsync(user);
            await UnitOfWork.SaveAsync();
            return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Success, messages: $"Kullanıcının şifresi başarıyla güncellenmiştir");
        }
    }
}
