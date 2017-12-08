using System;
using System.Net.Http;

namespace eShopOnContainers.Core.Services
{
    public abstract class ServiceBase
    {
        protected readonly HttpClient _httpClient;
        protected ServiceBase()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Constants.BaseUrl)
            };
        }
    }
}