using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Business.Abstract;
using AdekaDestek.Business.Adapters.SmsService;
using AdekaDestek.Core.Utilities.ComplexTypes;
using AdekaDestek.Core.Utilities.Encryptions;
using AdekaDestek.Core.Utilities.Extensions;
using AdekaDestek.Core.Utilities.Services;
using AdekaDestek.Entities.Concrete;
using AdekaDestek.Entities.Dtos;
using AdekaDestek.Mvc.Api;
using AdekaDestek.Mvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace AdekaDestek.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITwoFactorService _twoFactorService;
        private readonly ISmsService _smsService;
        private readonly IMailService _mailService;
        private readonly IToastNotification _toastNotification;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITwoFactorService twoFactorService, ISmsService smsService,
            IMailService mailService, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _twoFactorService = twoFactorService;
            _smsService = smsService;
            _mailService = mailService;
            _toastNotification = toastNotification;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                bool infiniCheckStatu = false;
                bool getEmail = userLoginDto.Email.Contains("@");

                if (getEmail == false) //İnfini üzerinden giriş yapmak için.
                {
                    infiniCheckStatu = InfiniUserService.InfiniCheck(userLoginDto.Email, userLoginDto.Password);

                    if (!infiniCheckStatu)
                    {
                        ModelState.AddModelError("", "Infini sisteminde doğruulama başarısız");
                    }
                    else
                    {
                        string encryptPassword = EncryptPassword.GetMD5Hash(userLoginDto.Password);
                        string[] userNames = userLoginDto.Email.Split("@");
                        string userName = userNames[0].ToString();

                        await UpdatePassowordForInfini.UpdatePassword(new UserUpdatePasswordForInfiniDto
                        {
                            Password = encryptPassword,
                            Username = userName,
                            ModifiedByName = "İnfini"
                        });

                        string infiniUsersEmail = userLoginDto.Email + "@adeka.com.tr";
                        User user = await _userManager.FindByEmailAsync(infiniUsersEmail);
                        bool userCheckInfini = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

                        if (userCheckInfini)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                            await _signInManager.SignOutAsync();
                            await _userManager.UpdateSecurityStampAsync(user);
                            var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, false);
                            if (result.RequiresTwoFactor)
                            {

                                if (user.TwoFactorType == (int)TwoFactorType.Phone)
                                {
                                    HttpContext.Session.Remove("currentTime");
                                }

                                return RedirectToAction("TwoFactorLogin", "Auth", new { Area = "Admin" });

                            }
                            else
                            {


                                return RedirectToAction("Index", "Home", new { Area = "Admin" });

                            }
                        }
                    }
                }
                else //Email ile Giriş
                {
                    //Böyle bir kullanıcı var mı
                    User user = await _userManager.FindByEmailAsync(userLoginDto.Email);

                    if (user != null)
                    {
                        //Email adresi ve şifresi doğru ise
                        bool userCheck = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

                        if (userCheck)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                            await _signInManager.SignOutAsync(); //Kullanıcı çıkış  yaptırdık.

                            var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false); //Gerekli argumanlar ile giriş yaptırdık.

                            if (result.RequiresTwoFactor)
                            {

                                if (user.TwoFactorType == (sbyte)TwoFactorType.Phone)
                                {
                                    HttpContext.Session.Remove("currentTime");

                                }
                                return RedirectToAction("TwoFactorLogin", "Auth", new { Area = "Admin" });


                            }
                            else
                            {


                                return RedirectToAction("Index", "Home", new { Area = "Admin" });

                            }

                        }
                        else
                        {
                            // Kullanıcının 3 kere email ve şifresini hatalı girmesi kontrolü
                            await _userManager.AccessFailedAsync(user);
                            int fail = await _userManager.GetAccessFailedCountAsync(user);
                            ModelState.AddModelError("", $"{fail}'kez başarısız giriş.");
                            if (fail == 3 || fail > 3)
                            {
                                await _userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(DateTime.Now.AddMinutes(20)));
                                ModelState.AddModelError("", $"Hesabınız {fail} başarısız giriş denemesinden dolayı 20 dakika süreyle güvenlik sebebiyle kilitlenmiştir. Lütfen daha sonra tekar deneyiniz.");
                            }
                            else
                            {
                                //Eğer hata varsa Modelstate hata gösterelim. Sayfada gözükecek

                                ModelState.AddModelError("", "Email / INFINI Kullanıcı Adı veya Şifreniz Yanlış");
                            }

                        }

                    }
                }


            }
            return View(userLoginDto);
        }

        [Authorize]
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth", new { Area = "Admin" });
        }
        [HttpGet]
        public async Task<IActionResult> TwoFactorLogin()
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();


            switch ((TwoFactorType)user.TwoFactorType)
            {
                case TwoFactorType.MicrosoftGoogle:
                    break;

                case TwoFactorType.Phone:
                    if (_twoFactorService.TimeLeft(HttpContext) == 0)
                    {
                        return RedirectToAction("Login");
                    }
                    ViewBag.timeLeft = _twoFactorService.TimeLeft(HttpContext);

                    HttpContext.Session.SetString("codeVerification", _smsService.Send(user.PhoneNumber));

                    break;




            }

            return View(new TwoFactorLoginViewModel() { TwoFactorType = (TwoFactorType)user.TwoFactorType, isRecoverCode = false, isRememberMe = false, VerificationCode = string.Empty });
        }
        [HttpPost]
        public async Task<IActionResult> TwoFactorLogin(TwoFactorLoginViewModel twoFactorLoginViewModel)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            ModelState.Clear();
            bool isSuccessAuth = false;

            if ((TwoFactorType)user.TwoFactorType == TwoFactorType.MicrosoftGoogle)
            {
                Microsoft.AspNetCore.Identity.SignInResult result;

                if (twoFactorLoginViewModel.isRecoverCode)
                {
                    result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(twoFactorLoginViewModel.VerificationCode);
                }
                else
                {
                    result = await _signInManager.TwoFactorAuthenticatorSignInAsync(twoFactorLoginViewModel.VerificationCode, twoFactorLoginViewModel.isRememberMe, false);
                }
                if (result.Succeeded)
                {
                    isSuccessAuth = true;
                }
                else
                {
                    ModelState.AddModelError("", "Doğrulama kodu yanlış");
                }
            }
            else if (user.TwoFactorType == (sbyte)TwoFactorType.Phone)
            {
                ViewBag.timeLeft = _twoFactorService.TimeLeft(HttpContext);
                if (twoFactorLoginViewModel.VerificationCode == HttpContext.Session.GetString("codeVerification"))
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, twoFactorLoginViewModel.isRememberMe);
                    HttpContext.Session.Remove("currentTime");
                    HttpContext.Session.Remove("codeVerification");
                    isSuccessAuth = true;
                }
                else
                {
                    ModelState.AddModelError("", "Girdiğiniz doğrulama kodu yanlıştır");
                }
            }

            if (isSuccessAuth)
            {

                return RedirectToAction("Index", "Home", new { Area = "Admin" });



            }

            twoFactorLoginViewModel.TwoFactorType = (TwoFactorType)user.TwoFactorType;

            return View(twoFactorLoginViewModel);
        }

        [HttpGet]
        public ViewResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            User user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
            if (user != null)
            {
                string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Auth", new
                {
                    userId = user.Id,
                    token = passwordResetToken

                }, HttpContext.Request.Scheme);

                resetPasswordViewModel.Link = passwordResetLink;
                resetPasswordViewModel.FirstName = user.FirstName;
                resetPasswordViewModel.LastName = user.LastName;
                string mailMessage = await this.RenderViewToStringAsync("ResetPasswordMail", resetPasswordViewModel);

                _mailService.Send(new EmailSendDto
                {
                    Subject = "AdekaDestek Şifre Yenileme",
                    Message = mailMessage,
                    Email = resetPasswordViewModel.Email,
                });

                _toastNotification.AddSuccessToastMessage($"Şifre yenileme linkiniz e-posta adresinize gönderilmiştir.", new ToastrOptions
                {

                    Title = "Başarılı İşlem!"
                });
            }
            else
            {
                ModelState.AddModelError("", "Sistemimize kayıtlı böyle bir e-posta adresi bulunamamıştır");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirm(int userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm([Bind("PasswordNew")] ResetPasswordViewModel resetPasswordViewModel)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();

            User user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.PasswordNew);

                if (result.Succeeded)
                {
                    var newPassword = EncryptPassword.GetMD5Hash(resetPasswordViewModel.PasswordNew);

                    await AdekaHelpDeskApi.UpdatePassword(new UserUpdatePasswordForAdekaDto
                    {
                        Email = user.Email,
                        Password = newPassword,
                        ModifiedByName = $"{user.FirstName} {user.LastName}"
                    });
                    await _userManager.UpdateSecurityStampAsync(user);
                    _toastNotification.AddSuccessToastMessage($"Şifreniz başarıyla güncellenmiştir.", new ToastrOptions
                    {

                        Title = "Başarılı İşlem!"
                    });

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            {


                ModelState.AddModelError("", "Bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.");


            }
            return View(resetPasswordViewModel);
        }
    }
}
