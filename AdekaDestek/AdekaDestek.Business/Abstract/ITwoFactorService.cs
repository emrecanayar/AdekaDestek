using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AdekaDestek.Business.Abstract
{
    public interface ITwoFactorService
    {
        public int TimeLeft(HttpContext context);
        public string GenerateQrCodeUri(string email, string unformattedKey);
        public int GetCodeVerification();
    }
}
