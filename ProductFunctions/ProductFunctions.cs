using DataModel.Messages;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ProductFunctions.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProductFunctions
{
    public class ProductFunctions
    {
        [Function("v1/products")]
        public async Task<HttpResponseData> GetProductsAsync([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("ProductFunctions");
            logger.LogInformation("Get Products called");

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(ProductStore.Products);

            return response;
        }

        [Function("v1/products/order")]
        public async Task<PlaceOrderOutputType> OrderProductV2Async([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("ProductFunctions");
            logger.LogInformation("Order Product is called");

            var output = new PlaceOrderOutputType();

            var order = await req.ReadFromJsonAsync<ProductOrder>();
            var p = ProductStore.Products.FirstOrDefault(x => x.Name == order.Name);
            if (p != null)
            {
                logger.LogInformation("Product found, sending message");
                output.HttpResponse = req.CreateResponse(HttpStatusCode.OK);
                output.Order = new PlaceOrder
                {
                    OrderId = Guid.NewGuid().ToString(),
                    Product = p,
                    Buyer = order.Buyer
                };
            }
            else
            {
                logger.LogWarning("Product not found");
                output.HttpResponse = req.CreateResponse(HttpStatusCode.NotFound);
            }
            return output;

        }

        public class PlaceOrderOutputType
        {
            [ServiceBusOutput("sales", Connection = "ServiceBusConnection")]
            public PlaceOrder Order { get; set; }

            public HttpResponseData HttpResponse { get; set; }
        }
    }
}
