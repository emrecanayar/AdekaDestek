using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdekaDestek.Business.Abstract;
using AdekaDestek.Business.Adapters.SmsService;
using AdekaDestek.Core.Utilities.ComplexTypes;
using AdekaDestek.Core.Utilities.Encryptions;
using AdekaDestek.Core.Utilities.Extensions;
using AdekaDestek.Core.Utilities.Results.ComplexTypes;
using AdekaDestek.Entities.Concrete;
using AdekaDestek.Entities.Dtos;
using AdekaDestek.Mvc.Api;
using AdekaDestek.Mvc.Areas.Admin.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace AdekaDestek.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IToastNotification _toastNotification;
        private readonly ITwoFactorService _twoFactorService;
        private readonly ISmsService _smsService;
        public UserController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, IToastNotification toastNotification, ITwoFactorService twoFactorService, ISmsService smsService) : base(userManager, mapper)
        {
            _signInManager = signInManager;
            _toastNotification = toastNotification;
            _twoFactorService = twoFactorService;
            _smsService = smsService;
        }
        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await UserManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }
        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,User.Create")]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [Authorize(Roles = "SuperAdmin,User.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<User>(userAddDto);
                user.ModifiedByName = $"{user.FirstName} {user.LastName}";
                user.ModifiedDate = DateTime.Now;
                user.CreatedByName = $"{user.FirstName} {user.LastName}";
                user.CreatedDate = DateTime.Now;
                user.TwoFactorType = 0;
                var result = await UserManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla eklenmiştir.",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxErrorModel);
                }

            }
            var userAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
            });
            return Json(userAddAjaxModelStateErrorModel);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,User.Read")]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await UserManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }

        [Authorize(Roles = "SuperAdmin,User.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await UserManager.FindByIdAsync(userId.ToString());
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await AdekaHelpDeskApi.DeleteUser(new UserDeleteDto
                {
                    Email = user.Email
                });

                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı başarıyla silinmiştir.",
                    User = user
                });
                return Json(deletedUser);
            }
            else
            {
                string errorMessages = String.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessages = $"*{error.Description}\n";
                }

                var deletedUserErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message =
                        $"{user.UserName} adlı kullanıcı adına sahip kullanıcı silinirken bazı hatalar oluştu.\n{errorMessages}",
                    User = user
                });
                return Json(deletedUserErrorModel);
            }
        }

        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await UserManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = Mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);

        }
        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldUser = await UserManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var updatedUser = Mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                updatedUser.ModifiedByName = $"{oldUser.FirstName} {oldUser.LastName}";
                updatedUser.ModifiedDate = DateTime.Now;
                var result = await UserManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    await AdekaHelpDeskApi.UpdateUser(userUpdateDto);

                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateViewModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userUpdateErorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateErorViewModel);
                }

            }
            else
            {
                var userUpdateModelStateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDto = userUpdateDto,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                });
                return Json(userUpdateModelStateErrorViewModel);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var updateDto = Mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {

                var oldUser = await UserManager.GetUserAsync(HttpContext.User);
                var updatedUser = Mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                updatedUser.ModifiedByName = $"{oldUser.FirstName} {oldUser.LastName}";
                updatedUser.ModifiedDate = DateTime.Now;
                var result = await UserManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    await AdekaHelpDeskApi.UpdateUser(userUpdateDto);

                    _toastNotification.AddSuccessToastMessage($"Bilgileriniz başarıyla güncellenmiştir.", new ToastrOptions
                    {

                        Title = "Başarılı İşlem!"
                    });
                    return View(userUpdateDto);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(userUpdateDto);
                }

            }
            else
            {
                return View(userUpdateDto);
            }
        }

        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.GetUserAsync(HttpContext.User);
                var isVerified = await UserManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await UserManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword,
                        userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        var newPassword = EncryptPassword.GetMD5Hash(userPasswordChangeDto.NewPassword);

                        await AdekaHelpDeskApi.UpdatePassword(new UserUpdatePasswordForAdekaDto
                        {
                            Email = user.Email,
                            Password = newPassword,
                            ModifiedByName = $"{user.FirstName} {user.LastName}"
                        });
                        await UserManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        _toastNotification.AddSuccessToastMessage($"Şifreniz başarıyla değiştirilmiştir.", new ToastrOptions
                        {

                            Title = "Başarılı İşlem!"
                        });
                        return View();
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(userPasswordChangeDto);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen, girmiş olduğunuz şu anki şifrenizi kontrol ediniz.");
                    return View(userPasswordChangeDto);
                }
            }
            else
            {
                return View(userPasswordChangeDto);
            }

        }

        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public async Task<PartialViewResult> GetDetail(int userId)
        {
            var user = await UserManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return PartialView("_GetDetailPartial", new UserDto { User = user });
        }

        [Authorize]
        [HttpGet]
        public IActionResult TwoFactorAuth()
        {
            return View(new AuthenticatorViewModel() { TwoFactorType = (TwoFactorType)LoggedInUser.TwoFactorType });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TwoFactorAuth(AuthenticatorViewModel authenticatorViewModel)
        {
            switch (authenticatorViewModel.TwoFactorType)
            {
                case TwoFactorType.None:
                    LoggedInUser.TwoFactorEnabled = false;
                    LoggedInUser.TwoFactorType = (sbyte)TwoFactorType.None;
                    _toastNotification.AddSuccessToastMessage($"İki adımlı kimlik doğrulama tipiniz 'Hiç Biri' olarak belirlenmiştir", new ToastrOptions
                    {

                        Title = "Başarılı İşlem!"
                    });
                    TempData["message"] = "İki adımlı kimlik doğrulama tipiniz 'Hiç Biri' olarak belirlenmiştir";
                    break;
                case TwoFactorType.Phone:
                    if (string.IsNullOrEmpty(LoggedInUser.PhoneNumber))
                    {
                        _toastNotification.AddInfoToastMessage($"Cep telefonu numaranız belirtilmemiştir. Lütfen 'Profil' sayfasına giderek cep telefonu numaranızı belirtiniz.", new ToastrOptions
                        {

                            Title = "Uyarı!"
                        });
                        ViewBag.warning = "Cep telefonu numaranız belirtilmemiştir. Lütfen 'Profil' sayfasına giderek cep telefonu numaranızı belirtiniz.";
                    }
                    else
                    {
                        return RedirectToAction("TwoFactorWithSms");

                    }


                    break;
                case TwoFactorType.MicrosoftGoogle:
                    return RedirectToAction("TwoFactorWithAuthenticator");

                default:
                    break;
            }
            await UserManager.UpdateAsync(LoggedInUser);

            return View(authenticatorViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> TwoFactorWithAuthenticator()
        {
            string unformattedKey = await UserManager.GetAuthenticatorKeyAsync(LoggedInUser); //Veritabanına key oluşturduk

            if (string.IsNullOrEmpty(unformattedKey))
            {
                await UserManager.ResetAuthenticatorKeyAsync(LoggedInUser);

                unformattedKey = await UserManager.GetAuthenticatorKeyAsync(LoggedInUser);
            }

            var authenticatorViewModel = new AuthenticatorViewModel();

            authenticatorViewModel.SharedKey = unformattedKey;
            authenticatorViewModel.AuthenticatorUri = _twoFactorService.GenerateQrCodeUri(LoggedInUser.Email, unformattedKey); //Burada key qr koda gönderiyoruz email ile qr oluşturma işlemindeki metodu tetiklemiş olduk.

            return View(authenticatorViewModel);

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TwoFactorWithAuthenticator(AuthenticatorViewModel authenticatorViewModel)
        {
            //Kullanıcı kontrolü sağladık.
            var verificationCode = authenticatorViewModel.VerificationCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            //Token doğrulama işlemini gerçekleştiriyoruz.
            var is2FATokenValid = await UserManager.VerifyTwoFactorTokenAsync(LoggedInUser, UserManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            //Gerekli kodlar yazılıp doğruluğu kontrol ediliyor.

            if (is2FATokenValid)
            {
                LoggedInUser.TwoFactorEnabled = true;
                LoggedInUser.TwoFactorType = (sbyte)TwoFactorType.MicrosoftGoogle;

                var recoveryCodes = await UserManager.GenerateNewTwoFactorRecoveryCodesAsync(LoggedInUser, 5);
                TempData["recoveryCodes"] = recoveryCodes;
                TempData["message"] = "İki adımlı kimlik doğrulama tipiniz Google / Microsoft Authenticator olarak belirlenmiştir";
                _toastNotification.AddSuccessToastMessage($"İki adımlı kimlik doğrulama tipiniz Google / Microsoft Authenticator olarak belirlenmiştir", new ToastrOptions
                {

                    Title = "Başarılı İşlem!"
                });

                return RedirectToAction("Logout", "Auth", new { Area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "Girdiğiniz doğrulama kodu yanlıştır.");
                return View(authenticatorViewModel);
            }


        }
        [Authorize]
        [HttpGet]
        public IActionResult TwoFactorWithSms()
        {
            var phoneNumber = LoggedInUser.PhoneNumber;
            return PartialView("_TwoFactorWithSmsPartial", new TwoFactorSmsDto { PhoneNumber = phoneNumber, VerificationCode = string.Empty });

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TwoFactorWithSms(TwoFactorSmsDto twoFactorSmsDto)
        {
            //bool isTwoFactorVerification = false;
            LoggedInUser.TwoFactorEnabled = false;
            LoggedInUser.TwoFactorType = (sbyte)TwoFactorType.None;
            ViewBag.timeLeft = _twoFactorService.TimeLeft(HttpContext);
            if (twoFactorSmsDto.VerificationCode == HttpContext.Session.GetString("codeVerification"))
            {
                var confirmTwoFactorSmsModel = JsonSerializer.Serialize(new TwoFactorSmsConfirmAjaxViewModel
                {
                    twoFactorSmsDto = new TwoFactorSmsDto
                    {
                        ResultStatus = ResultStatus.Success,
                        VerificationCode = twoFactorSmsDto.VerificationCode,
                        PhoneNumber = LoggedInUser.PhoneNumber

                    },
                    TwoFactorSmsConfirmPartial = await this.RenderViewToStringAsync("_TwoFactorWithSmsPartial", twoFactorSmsDto)
                });



                LoggedInUser.TwoFactorEnabled = true;
                LoggedInUser.TwoFactorType = (sbyte)TwoFactorType.Phone;
                await UserManager.UpdateAsync(LoggedInUser);
                HttpContext.Session.Remove("currentTime");
                HttpContext.Session.Remove("codeVerification");

                return Json(confirmTwoFactorSmsModel);

            }
            else
            {

                ModelState.AddModelError("", "Lütfen, girmiş olduğunuz şu anki şifrenizi kontrol ediniz.");
                var confirmTwoFactorSmsErrorModel = JsonSerializer.Serialize(new TwoFactorSmsConfirmAjaxViewModel
                {
                    twoFactorSmsDto = new TwoFactorSmsDto
                    {
                        ResultStatus = ResultStatus.Error,
                        VerificationCode = twoFactorSmsDto.VerificationCode,
                        PhoneNumber = LoggedInUser.PhoneNumber
                    },
                    TwoFactorSmsConfirmPartial = await this.RenderViewToStringAsync("_TwoFactorWithSmsPartial", twoFactorSmsDto)
                });

                return Json(confirmTwoFactorSmsErrorModel);
            }

        }
        [Authorize]
        [HttpPost]
        public JsonResult TwoFactorWithSmsVerification()
        {
            ViewBag.timeLeft = _twoFactorService.TimeLeft(HttpContext);
            string phoneNumber = LoggedInUser.PhoneNumber;
            string text = $"İki faktörlü kimlik doğrulama kodunuz";
            HttpContext.Session.SetString("codeVerification", _smsService.SendAsist(text, phoneNumber));

            var confirmedVerification = JsonSerializer.Serialize(new UserDto
            {
                ResultStatus = ResultStatus.Success,
                Message = $"{phoneNumber} adlı kullanıcı adına sahip kullanıcı başarıya doğrulanmıştır",
            });
            return Json(confirmedVerification);
        }
    }
}

