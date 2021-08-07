using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AdekaDestek.Business.Abstract;
using AdekaDestek.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AdekaDestek.Business.Concrete
{
    public class TwoFactorManager : ITwoFactorService
    {
        private readonly UrlEncoder _urlEncoder;
        private readonly TwoFactorOptions _twoFactorOptions;
        public TwoFactorManager(UrlEncoder urlEncoder, IOptions<TwoFactorOptions> twoFactorOptions)
        {
            _urlEncoder = urlEncoder;
            _twoFactorOptions = twoFactorOptions.Value;
        }
        public string GenerateQrCodeUri(string email, string unformattedKey)
        {
            const string format = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

            return string.Format(format, _urlEncoder.Encode("www.destekadeka.com.tr"), _urlEncoder.Encode(email), unformattedKey);
        }

        public int GetCodeVerification()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999);
        }

        public int TimeLeft(HttpContext context)
        {

            if (context.Session.GetString("currentTime") == null)
            {
                context.Session.SetString("currentTime", DateTime.Now.AddSeconds(_twoFactorOptions.CodeTimeExpire).ToString());
            }

            DateTime currentTime = DateTime.Parse(context.Session.GetString("currentTime").ToString());

            int timeLeft = (int)(currentTime - DateTime.Now).TotalSeconds;

            if (timeLeft <= 0)
            {
                context.Session.Remove("currentTime");
                return 0;
            }
            else
            {
                return timeLeft;
            }
        }
    }
}
