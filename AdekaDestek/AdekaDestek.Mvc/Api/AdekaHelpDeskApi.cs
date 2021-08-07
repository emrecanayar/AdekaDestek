using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;
using Newtonsoft.Json;

namespace AdekaDestek.Mvc.Api
{
    public static class AdekaHelpDeskApi
    {

        public static async Task UpdatePassword(UserUpdatePasswordForAdekaDto userUpdatePasswordForAdekaDto)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.50.26:9627/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(JsonConvert.SerializeObject(userUpdatePasswordForAdekaDto), Encoding.UTF8, "application/json");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var response = await client.PostAsync("/api/User/updatepasswordforadeka", stringContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                dynamic result = await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task DeleteUser(UserDeleteDto userDeleteDto)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.50.26:9627/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(JsonConvert.SerializeObject(userDeleteDto), Encoding.UTF8, "application/json");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var response = await client.PostAsync("/api/User/deleteuserforadeka", stringContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                dynamic result = await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task UpdateUser(UserUpdateDto userUpdateDto)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.50.26:9627/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(JsonConvert.SerializeObject(userUpdateDto), Encoding.UTF8, "application/json");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var response = await client.PostAsync("/api/User/updateuserforadeka", stringContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                dynamic result = await response.Content.ReadAsStringAsync();
            }
        }

    }
}
