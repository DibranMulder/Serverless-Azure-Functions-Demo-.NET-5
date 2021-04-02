using System;
using System.Net.Http;
using System.Threading.Tasks;
using DataModel.Messages;
using Microsoft.Extensions.Logging;

namespace SalesFunctions
{
    public class PlaceOrderHandler
    {
        public PlaceOrderHandler()
        {
            crmHttpClient = new HttpClient();
        }

        private readonly HttpClient crmHttpClient;

        public async Task<OrderPlaced> Handle(PlaceOrder message, ILogger logger)
        {
            logger.LogInformation($"Received PlaceOrderV2, OrderId = {message.OrderId}");

            // Update CRM.
            var res = await crmHttpClient.GetAsync("https://dibranmulder.github.io/2020/09/22/Improving-your-Azure-Search-performance/");
            if (res.IsSuccessStatusCode)
            {
                logger.LogInformation("CRM called");
            }

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };
            return orderPlaced;
        }
    }
}