using eShopOnContainers.Core.Extensions;
using eShopOnContainers.Core.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eShopOnContainers.Core.Services.Catalog
{
    public class CatalogMockService : ICatalogService
    {
        private ObservableCollection<CatalogBrand> MockCatalogBrand = new ObservableCollection<CatalogBrand>
        {
            new CatalogBrand { Id = 1, Brand = "Azure" },
            new CatalogBrand { Id = 2, Brand = "Visual Studio" }
        };

        private ObservableCollection<CatalogType> MockCatalogType = new ObservableCollection<CatalogType>
        {
            new CatalogType { Id = 1, Type = "Mug" },
            new CatalogType { Id = 2, Type = "T-Shirt" }
        };

        public async Task<ObservableCollection<CatalogItem>> GetCatalogAsync()
        {
            await Task.Delay(500);

            return new List<CatalogItem>().ToObservableCollection();
        }

        public async Task<ObservableCollection<CatalogItem>> FilterAsync(int catalogBrandId, int catalogTypeId)
        {
            await Task.Delay(500);

            return new List<CatalogItem>()   
                .ToObservableCollection();
        }

        public async Task<ObservableCollection<CatalogBrand>> GetCatalogBrandAsync()
        {
            await Task.Delay(500);

            return MockCatalogBrand;
        }

        public async Task<ObservableCollection<CatalogType>> GetCatalogTypeAsync()
        {
            await Task.Delay(500);

            return MockCatalogType;
        }
    }
}