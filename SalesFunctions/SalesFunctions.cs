using System.Text;
using System.Threading.Tasks;
using DataModel.Messages;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SalesFunctions
{
    public class SalesFunctions
    {
        private readonly PlaceOrderHandler placeOrderHandler;

        public SalesFunctions(PlaceOrderHandler placeOrderHandler)
        {
            this.placeOrderHandler = placeOrderHandler;
        }

        [Function("PlaceOrderHandler")]
        public async Task<SalesOutputType> PlaceOrderHandler(
            [ServiceBusTrigger(queueName: "sales", Connection = "ServiceBusConnection")]
                PlaceOrder message, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("SalesFunctions");
            logger.LogInformation("Place order handler is called");

            var output = new SalesOutputType();

            logger.LogInformation($"Received PlaceOrderV2, OrderId = {message.OrderId}");

            output.OrderPlaced = await placeOrderHandler.Handle(message, logger);

            return output;
        }

        public class SalesOutputType
        {
            [ServiceBusOutput("billing", Connection = "ServiceBusConnection")]
            public OrderPlaced OrderPlaced { get; set; }
        }
    }
}
