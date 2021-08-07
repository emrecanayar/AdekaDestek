using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.Helpers.Abstract;
using AdekaDestek.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;

namespace AdekaDestek.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class OptionsController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly WebSiteInfo _webSiteInfo;
        private readonly IWritableOptions<WebSiteInfo> _websiteInfoWriter;
        private readonly SmtpSettings _smtpSettings;
        private readonly IWritableOptions<SmtpSettings> _smtpSettingsWriter;
        private readonly SmsSettings _smsSettings;
        private readonly IWritableOptions<SmsSettings> _smsSettingsWriter;
        private readonly TwoFactorOptions _twoFactorOptions;
        private readonly IWritableOptions<TwoFactorOptions> _twoFactorOptionsWriter;

        public OptionsController(IToastNotification toastNotification, IOptionsSnapshot<WebSiteInfo> webSiteInfo, IWritableOptions<WebSiteInfo> websiteInfoWriter,
            IOptionsSnapshot<SmtpSettings> smtpSettings, IWritableOptions<SmtpSettings> smtpSettingsWriter,
            IOptionsSnapshot<SmsSettings> smsSettings, IWritableOptions<SmsSettings> smsSettingsWriter,
              IOptionsSnapshot<TwoFactorOptions> twoFactorOptions, IWritableOptions<TwoFactorOptions> twoFactorOptionsWriter)
        {
            _toastNotification = toastNotification;
            _websiteInfoWriter = websiteInfoWriter;
            _webSiteInfo = webSiteInfo.Value;
            _smtpSettingsWriter = smtpSettingsWriter;
            _smtpSettings = smtpSettings.Value;
            _smsSettingsWriter = smsSettingsWriter;
            _smsSettings = smsSettings.Value;
            _twoFactorOptions = twoFactorOptions.Value;
            _twoFactorOptionsWriter = twoFactorOptionsWriter;
        }

        [HttpGet]
        public IActionResult GeneralSettings()
        {
            return View(_webSiteInfo);
        }
        [HttpPost]
        public IActionResult GeneralSettings(WebSiteInfo webSiteInfo)
        {
            if (ModelState.IsValid)
            {
                _websiteInfoWriter.Update(x =>
                {
                    x.Title = webSiteInfo.Title;
                    x.MenuTitle = webSiteInfo.MenuTitle;
                    x.SeoAuthor = webSiteInfo.SeoAuthor;
                    x.SeoDescription = webSiteInfo.SeoDescription;
                    x.SeoTags = webSiteInfo.SeoTags;

                });
                _toastNotification.AddSuccessToastMessage("Sitenizin genel ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem"!
                });

                return View(webSiteInfo);
            }
            return View(webSiteInfo);
        }

        [HttpGet]
        public IActionResult EmailSettings()
        {
            return View(_smtpSettings);
        }
        [HttpPost]
        public IActionResult EmailSettings(SmtpSettings smtpSettings)
        {
            if (ModelState.IsValid)
            {
                _smtpSettingsWriter.Update(x =>
                {
                    x.Server = smtpSettings.Server;
                    x.Port = smtpSettings.Port;
                    x.SenderName = smtpSettings.SenderName;
                    x.SenderEmail = smtpSettings.SenderEmail;
                    x.Username = smtpSettings.Username;
                    x.Password = smtpSettings.Password;

                });
                _toastNotification.AddSuccessToastMessage("Sitenizin e-posta ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem"!
                });

                return View(smtpSettings);
            }
            return View(smtpSettings);
        }

        [HttpGet]
        public IActionResult SmsSettings()
        {
            return View(_smsSettings);
        }
        [HttpPost]
        public IActionResult SmsSettings(SmsSettings smsSettings)
        {
            if (ModelState.IsValid)
            {
                _smsSettingsWriter.Update(x =>
                {
                    x.Username = smsSettings.Username;
                    x.Password = smsSettings.Password;
                    x.Key = smsSettings.Key;
                    x.Sender = smsSettings.Sender;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin sms ayarları başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem"!
                });

                return View(smsSettings);
            }
            return View(smsSettings);
        }

        [HttpGet]
        public IActionResult TimeLeftSettings()
        {
            return View(_twoFactorOptions);
        }
        [HttpPost]
        public IActionResult TimeLeftSettings(TwoFactorOptions twoFactorOptions)
        {
            if (ModelState.IsValid)
            {
                _twoFactorOptionsWriter.Update(x =>
                {
                    x.CodeTimeExpire = twoFactorOptions.CodeTimeExpire;

                });
                _toastNotification.AddSuccessToastMessage("Sitenizin iki faktörlü kimlik doğrulama yaparken geçen süre ayarı başarıyla güncellenmiştir.", new ToastrOptions
                {
                    Title = "Başarılı İşlem"!
                });

                return View(twoFactorOptions);
            }
            return View(twoFactorOptions);
        }


    }
}
