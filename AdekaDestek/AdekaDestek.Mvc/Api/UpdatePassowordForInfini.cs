using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace AdekaDestek.Mvc.Api
{
    public static class UpdatePassowordForInfini
    {
        public static async Task UpdatePassword(UserUpdatePasswordForInfiniDto userUpdatePasswordForInfiniDto)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.50.26:9625/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(JsonConvert.SerializeObject(userUpdatePasswordForInfiniDto), Encoding.UTF8, "application/json");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var response = await client.PostAsync("/api/User/updatepasswordforinfini", stringContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                dynamic result = await response.Content.ReadAsStringAsync();
            }
        }
    }
}