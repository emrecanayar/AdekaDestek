using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Business.Abstract;
using AdekaDestek.Entities.Concrete;
using Microsoft.Extensions.Options;

namespace AdekaDestek.Business.Adapters.SmsService
{
    public class SmsManager : ISmsService
    {
        private readonly TwoFactorOptions _twoFactorOptions;
        private readonly ITwoFactorService _twoFactorService;
        private readonly SmsSettings _smsSettings;
        private static readonly Encoding encoding = Encoding.UTF8;
        public SmsManager(IOptions<TwoFactorOptions> options, ITwoFactorService twoFactorService, IOptionsSnapshot<SmsSettings> smsSettings)
        {
            _twoFactorOptions = options.Value;
            _twoFactorService = twoFactorService;
            _smsSettings = smsSettings.Value;

        }
        public string Send(string cellPhone)
        {

            string code = _twoFactorService.GetCodeVerification().ToString();
            string testXml = "<request>";
            testXml += "<authentication>";
            testXml += $"<username>{_smsSettings.Username}</username>";
            testXml += $"<password>{_smsSettings.Password}</password>";
            testXml += "</authentication>";
            testXml += "<order>";
            testXml += $"<sender>ADEKA ILAC</sender>";
            testXml += "<sendDateTime></sendDateTime>";
            testXml += "<message>";
            testXml += $"<text>AdekaDestek sistemine giriş yapabilmek için tek kullanımlık kod : {code} </text>";
            testXml += "<receipents>";
            testXml += $"<number>{cellPhone}</number>";
            testXml += "</receipents>";
            testXml += "</message>";
            testXml += "</order>";
            testXml += "</request>";

            this.XMLPOST("https://api.iletimerkezi.com/v1/send-sms", testXml);

            return code;
        }

        public string XMLPOST(string postAddress, string xmlData)
        {
            try
            {
                var res = "";
                byte[] bytes = Encoding.UTF8.GetBytes(xmlData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postAddress);

                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "text/xml";
                request.Timeout = 300000000;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                // This sample only checks whether we get an "OK" HTTP status code back.
                // If you must process the XML-based response, you need to read that from
                // the response stream.
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format(
                        "POST failed. Received HTTP {0}",
                        response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    Stream responseStream = response.GetResponseStream();
                    using (StreamReader rdr = new StreamReader(responseStream))
                    {
                        res = rdr.ReadToEnd();
                    }
                    return res;
                }
            }
            catch (Exception exception)
            {
                var exceptionMessage = exception.Message;
                return "-1";
            }
        }

        public string SendAsist(string text, string cellPhone)
        {
            string code = _twoFactorService.GetCodeVerification().ToString();
            string testXml = "<request>";
            testXml += "<authentication>";
            testXml += $"<username>{_smsSettings.Username}</username>";
            testXml += $"<password>{_smsSettings.Password}</password>";
            testXml += "</authentication>";
            testXml += "<order>";
            testXml += $"<sender>ADEKA ILAC</sender>";
            testXml += "<sendDateTime></sendDateTime>";
            testXml += "<message>";
            testXml += $"<text>{text}: {code} </text>";
            testXml += "<receipents>";
            testXml += $"<number>{cellPhone}</number>";
            testXml += "</receipents>";
            testXml += "</message>";
            testXml += "</order>";
            testXml += "</request>";

            this.XMLPOST("https://api.iletimerkezi.com/v1/send-sms", testXml);

            return code;
        }
    }
}
