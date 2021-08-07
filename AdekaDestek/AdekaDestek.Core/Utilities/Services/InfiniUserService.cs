using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace AdekaDestek.Core.Utilities.Services
{
    //Infini firmasının paylaştığı login servis yapısı. Sisteme Infini üzerinden login yapabilmek için gereklidir.
   public static class InfiniUserService
    {
        public const string BASE_URL = @"https://adeka.idesse.com/";
        public const string RESOURCE_ACCOUNT_CHECK = "v17s701/IsAccountValid/";
        public const string RESOURCE_EXPENSE_PENDING_ITEMS = "v17s701/GetPendingExpenses/";
        public const string RESOURCE_EXPENSE_DOC = "v17s701/GetExpenseDoc/";
        public const string RESOURCE_EXPENSE_MARK = "v17s701/MarkAsIntegrated/";
        public const string JSON_CONTENT_TYPE = "application/json";

        public static bool InfiniCheck(string email, string password)
        {
            bool getEmail = email.Contains("@");

            bool infiniCheckStatu = false;

            if (getEmail == false)
            {


                var client = new RestClient(BASE_URL);
                var request = new RestRequest(RESOURCE_ACCOUNT_CHECK, Method.GET);
                request.AddParameter("username", email);
                request.AddParameter("password", password);
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");

                var response = client.Execute(request);
                if (response.ContentType.StartsWith(JSON_CONTENT_TYPE) == false)
                {
                    infiniCheckStatu = false;
                }
                var deserializer = new RestSharp.Deserializers.DeserializeAsAttribute();

                var resultinfini = JsonConvert.DeserializeObject<IsAccountValidResult>(response.Content);
                if (resultinfini.Success == true)
                {
                    infiniCheckStatu = true;
                }
            }


            return infiniCheckStatu;

        }
        public class IsAccountValidResult
        {
            public bool Success { get; set; }
        }
    }
}
