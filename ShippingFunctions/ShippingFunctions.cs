using System;
using System.Threading.Tasks;
using DataModel.Commands;
using DataModel.Messages;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ShippingFunctions
{
    public class ShippingFunctions
    {
        [Function("OrderBilledHandler")]
        public async Task<ShippingOutput> OrderBilledHandler(
            [ServiceBusTrigger(queueName: "shipping", Connection = "ServiceBusConnection")]
                OrderBilled message, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("ShippingFunctions");

            logger.LogInformation($"Received OrderBilled, OrderId = {message.OrderId}");
            var output = new ShippingOutput()
            {
                ShipOrder = new ShipOrder()
                {
                    OrderId = message.OrderId
                }
            };

            return output;
        }
    }

    public class ShippingOutput
    {
        [ServiceBusOutput("communication", Connection = "ServiceBusConnection")]
        public ShipOrder ShipOrder { get; set; }
    }
}
