using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eShopOnContainers.Core.Extensions;
using eShopOnContainers.Core.Models.Basket;
using eShopOnContainers.Core.Models.Orders;
using Newtonsoft.Json;

namespace eShopOnContainers.Core.Services.Order
{
    public class OrderService: ServiceBase, IOrderService
    {
        public async Task CreateOrderAsync(Models.Orders.Order newOrder, string token)
        {
            const string url = "/api/orders";

            var json = JsonConvert.SerializeObject(newOrder);
            var content = new StringContent(json,Encoding.UTF8,"application/json");
            
            _httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("bearer",token);

            var resp = await _httpClient.PostAsync(url,content);
            
            resp.EnsureSuccessStatusCode();
        }

        public async Task<ObservableCollection<Models.Orders.Order>> GetOrdersAsync(string token)
        {
            const string url = "/api/orders";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",token);

            var json = await _httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<ObservableCollection<Models.Orders.Order>>(json);
        }

        public async Task<Models.Orders.Order> GetOrderAsync(int orderId, string token)
        {
            var url = $"/api/orders/{orderId}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",token);

            var json = await _httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<Models.Orders.Order>(json);
        }

        public async Task<ObservableCollection<CardType>> GetCardTypesAsync(string token)
        {
            return OrderMockService.MockCardTypes.ToObservableCollection();
        }

        public async Task<bool> CancelOrderAsync(int orderId, string token)
        {
            var url = $"/api/orders?orderId={orderId}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",token);

            var resp = await _httpClient.DeleteAsync(url);

            return resp.IsSuccessStatusCode;
        }

        public BasketCheckout MapOrderToBasket(Models.Orders.Order order)
        {
            return new BasketCheckout()
            {
                CardExpiration = order.CardExpiration,
                CardHolderName = order.CardHolderName,
                CardNumber = order.CardNumber,
                CardSecurityNumber = order.CardSecurityNumber,
                CardTypeId = order.CardTypeId,
                City = order.ShippingCity,
                State = order.ShippingState,
                Country = order.ShippingCountry,
                ZipCode = order.ShippingZipCode,
                Street = order.ShippingStreet
            };
        }
    }
}
