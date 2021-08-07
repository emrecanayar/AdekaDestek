using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Core.Entities.Concrete;
using AdekaDestek.Core.Utilities.Results.Abstract;
using AdekaDestek.Core.Utilities.Results.Concrete;
using AdekaDestek.Entities.Dtos;
using AdekaDestek.HelpDeskAPI.Business.Abstract;
using AdekaDestek.HelpDeskAPI.Concrete;
using AdekaDestek.HelpDeskAPI.DataAccess.Abstract;

namespace AdekaDestek.HelpDeskAPI.Business.Concrete
{
    public class UserManager : ManagerBase, IUserService
    {
        public UserManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IResult> DeleteUserforHelpDeskAsync(string email)
        {
            var oldUser = await UnitOfWork.HelpDeskUsers.GetAsync(u => u.EMAIL == email);
            if (oldUser == null)
            {
                return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Error, messages: $"Belirttiğiniz e-postaya ait kullanıcı bulunanamıştır.");
            }
            var user = new USER();
            user.ID = oldUser.ID;
            user.NAME = oldUser.NAME;
            user.SURNAME = oldUser.SURNAME;
            user.EMAIL = oldUser.EMAIL;
            user.PASSWORD = oldUser.PASSWORD;
            user.USER_DEPARTMENT_ID = oldUser.USER_DEPARTMENT_ID;
            user.USER_SECTION_ID = oldUser.USER_SECTION_ID;
            user.USER_GROUP_ID = oldUser.USER_GROUP_ID;
            user.LOCATION_ID = oldUser.LOCATION_ID;
            user.MANAGER_ID = oldUser.MANAGER_ID;
            user.DEVICE_ID = oldUser.DEVICE_ID;
            user.INSERT_DATETIME = oldUser.INSERT_DATETIME;
            user.INSERT_USER = oldUser.INSERT_USER;
            user.UPDATE_DATETIME = DateTime.Now;
            user.UPDATE_USER = oldUser.UPDATE_USER;
            user.LAST_LOGIN_DATETIME = oldUser.LAST_LOGIN_DATETIME;
            user.LAST_WRONG_LOGIN_TRY = oldUser.LAST_WRONG_LOGIN_TRY;
            user.SAP_KULLANICI_ADI = oldUser.SAP_KULLANICI_ADI;
            user.SAP_PERSONEL_NO = oldUser.SAP_PERSONEL_NO;
            user.INFINI_KULLANICI_ADI = oldUser.INFINI_KULLANICI_ADI;
            user.EmailAtilabilirMi = oldUser.EmailAtilabilirMi;
            user.OnayaTabiMi = oldUser.OnayaTabiMi;
            user.YONETICI_CC = oldUser.YONETICI_CC;
            user.ISE_DEVAM_MI = false;
            await UnitOfWork.HelpDeskUsers.UpdateAsync(user);
            await UnitOfWork.SaveAsync();
            return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Success, messages: $"Kullanıcı başarıyla pasife alınmıştır.");
        }

        public async Task<IResult> UpdatePasswordforHelpDeskAsync(string password, string email, string modifiedByName)
        {
            var oldUser = await UnitOfWork.HelpDeskUsers.GetAsync(u => u.EMAIL == email);
            if (oldUser == null)
            {
                return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Error, messages: $"Belirttiğiniz e-postaya ait kullanıcı bulunanamıştır.");
            }

            var user = new USER();
            user.ID = oldUser.ID;
            user.NAME = oldUser.NAME;
            user.SURNAME = oldUser.SURNAME;
            user.EMAIL = oldUser.EMAIL;
            user.PASSWORD = password;
            user.USER_DEPARTMENT_ID = oldUser.USER_DEPARTMENT_ID;
            user.USER_SECTION_ID = oldUser.USER_SECTION_ID;
            user.USER_GROUP_ID = oldUser.USER_GROUP_ID;
            user.LOCATION_ID = oldUser.LOCATION_ID;
            user.MANAGER_ID = oldUser.MANAGER_ID;
            user.DEVICE_ID = oldUser.DEVICE_ID;
            user.INSERT_DATETIME = oldUser.INSERT_DATETIME;
            user.INSERT_USER = oldUser.INSERT_USER;
            user.UPDATE_DATETIME = DateTime.Now;
            user.UPDATE_USER = modifiedByName;
            user.LAST_LOGIN_DATETIME = oldUser.LAST_LOGIN_DATETIME;
            user.LAST_WRONG_LOGIN_TRY = oldUser.LAST_WRONG_LOGIN_TRY;
            user.SAP_KULLANICI_ADI = oldUser.SAP_KULLANICI_ADI;
            user.SAP_PERSONEL_NO = oldUser.SAP_PERSONEL_NO;
            user.INFINI_KULLANICI_ADI = oldUser.INFINI_KULLANICI_ADI;
            user.EmailAtilabilirMi = oldUser.EmailAtilabilirMi;
            user.OnayaTabiMi = oldUser.OnayaTabiMi;
            user.YONETICI_CC = oldUser.YONETICI_CC;
            user.ISE_DEVAM_MI = oldUser.ISE_DEVAM_MI;
            await UnitOfWork.HelpDeskUsers.UpdateAsync(user);
            await UnitOfWork.SaveAsync();
            return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Success, messages: $"Kullanıcının şifresi başarıyla güncellenmiştir");
        }

        public async Task<IResult> UpdateUserforHelpDeskAsync(UserUpdateDto userUpdateDto)
        {
            var oldUser = await UnitOfWork.HelpDeskUsers.GetAsync(u => u.EMAIL == userUpdateDto.Email);
            if (oldUser == null)
            {
                return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Error, messages: $"Belirttiğiniz e-postaya ait kullanıcı bulunanamıştır.");
            }

            var user = new USER();
            user.ID = oldUser.ID;
            user.NAME = userUpdateDto.FirstName;
            user.SURNAME = userUpdateDto.LastName;
            user.EMAIL = oldUser.EMAIL;
            user.PASSWORD = oldUser.PASSWORD;
            user.USER_DEPARTMENT_ID = oldUser.USER_DEPARTMENT_ID;
            user.USER_SECTION_ID = oldUser.USER_SECTION_ID;
            user.USER_GROUP_ID = oldUser.USER_GROUP_ID;
            user.LOCATION_ID = oldUser.LOCATION_ID;
            user.MANAGER_ID = oldUser.MANAGER_ID;
            user.DEVICE_ID = oldUser.DEVICE_ID;
            user.INSERT_DATETIME = oldUser.INSERT_DATETIME;
            user.INSERT_USER = oldUser.INSERT_USER;
            user.UPDATE_DATETIME = DateTime.Now;
            user.UPDATE_USER = oldUser.UPDATE_USER;
            user.LAST_LOGIN_DATETIME = oldUser.LAST_LOGIN_DATETIME;
            user.LAST_WRONG_LOGIN_TRY = oldUser.LAST_WRONG_LOGIN_TRY;
            user.SAP_KULLANICI_ADI = userUpdateDto.SapUserName;
            user.SAP_PERSONEL_NO = Convert.ToInt32(userUpdateDto.SapEmployeeNo);
            user.INFINI_KULLANICI_ADI = userUpdateDto.InfiniUserName;
            user.EmailAtilabilirMi = oldUser.EmailAtilabilirMi;
            user.OnayaTabiMi = oldUser.OnayaTabiMi;
            user.YONETICI_CC = oldUser.YONETICI_CC;
            user.ISE_DEVAM_MI = oldUser.ISE_DEVAM_MI;
            await UnitOfWork.HelpDeskUsers.UpdateAsync(user);
            await UnitOfWork.SaveAsync();
            return new Result(Core.Utilities.Results.ComplexTypes.ResultStatus.Success, messages: $"Kullanıcının başarıyla güncellenmiştir.");
        }
    }
}
