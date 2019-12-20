using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Clients
{
    public abstract class BaseClient
    {
        protected HttpClient Client { get; set; }

        protected abstract string ServiceAddress { get; }

        protected BaseClient(IConfiguration configuration)
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri(configuration["ClientAddress"])
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
        }

        protected T Get<T>(string url) where T : new() 
        {
            return GetAsync<T>(url).Result;
        }

        protected async Task<T> GetAsync<T>(string url) where T : new()
        {
            var list = new T();
            var response = await Client.GetAsync(url);

            if (response.IsSuccessStatusCode) 
            {
                list = await response.Content.ReadAsAsync<T>();
            }

            return list;
        }



        public HttpResponseMessage Delete(string url)
        {
            return DeleteAsync(url).Result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await Client.DeleteAsync(requestUri: url);
        }

        public HttpResponseMessage Post<T>(string url, T value) where T : new()
        {
            return PostAsync<T>(url, value).Result;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T value) where T : new()
        {
            var response = await Client.PostAsJsonAsync(requestUri: url, value: value);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public HttpResponseMessage Put<T>(string url, T value) where T : new()
        {
            return PutAsync<T>(url, value).Result;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T value) where T : new()
        {
            var response = await Client.PutAsJsonAsync(requestUri: url, value: value);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
