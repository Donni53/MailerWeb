using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MailerWeb.Shared.Models.Requests;
using MailerWeb.Shared.Models.Responses;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace MailerWeb.Client.Services
{
    public class HttpService
    {
        public async Task<HttpResponseMessage> Request(HttpMethod method, string uri, object data = null)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage {RequestUri = new Uri(uri), Method = method};
                    if (data != null)
                    {
                        var json = JsonConvert.SerializeObject(data);
                        HttpContent content = new StringContent(json);
                        request.Content = content;
                        request.Content.Headers.ContentType.MediaType = "application/json";
                    }
                    response = await client.SendAsync(request);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return response;
        }
    }
}
