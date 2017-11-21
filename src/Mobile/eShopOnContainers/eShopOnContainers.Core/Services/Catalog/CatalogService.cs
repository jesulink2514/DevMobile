using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eShopOnContainers.Core.Models.Catalog;
using Newtonsoft.Json;

namespace eShopOnContainers.Core.Services.Catalog
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Constants.BaseUrl)
            };
        }
        public async Task<ObservableCollection<CatalogBrand>> GetCatalogBrandAsync()
        {
            var json = await _httpClient.GetStringAsync("/api/catalog/brands");
            var items = JsonConvert.DeserializeObject<ObservableCollection<CatalogBrand>>(json);
            return items;
        }

        public async Task<ObservableCollection<CatalogItem>> FilterAsync(int catalogBrandId, int catalogTypeId)
        {
            var url = $"/api/catalog/filter?catalogBrandId={catalogBrandId}&catalogTypeId={catalogTypeId}";

            var json = await _httpClient.GetStringAsync(url);

            var items = JsonConvert.DeserializeObject<ObservableCollection<CatalogItem>>(json);

            return items;
        }

        public async Task<ObservableCollection<CatalogType>> GetCatalogTypeAsync()
        {
            var json = await _httpClient.GetStringAsync("/api/catalog/types");
            var items = JsonConvert.DeserializeObject<ObservableCollection<CatalogType>>(json);
            return items;
        }

        public async Task<ObservableCollection<CatalogItem>> GetCatalogAsync()
        {
            var json = await _httpClient.GetStringAsync("/api/catalog");

            var items = JsonConvert.DeserializeObject<ObservableCollection<CatalogItem>>(json);

            return items;
        }
    }
}
