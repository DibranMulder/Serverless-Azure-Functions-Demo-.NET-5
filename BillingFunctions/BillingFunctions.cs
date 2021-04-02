using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataModel.Messages;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SalesFunctions
{
    public class BillingFunctions
    {
        [Function("OrderPlacedHandler")]
        public async Task<BillingOutputType> Run(
            [ServiceBusTrigger(queueName: "billing", Connection = "ServiceBusConnection")]
                OrderPlaced message, string name, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("BillingFunctions");
            logger.LogInformation("Order Placed is called");

            return new BillingOutputType()
            {
                Blob = Encoding.UTF8.GetBytes($"Hello {message.OrderId} is paid."),
                OrderBilled = new OrderBilled()
                {
                    OrderId = message.OrderId
                }
            };
        }

        public class BillingOutputType
        {
            [ServiceBusOutput("shipping", Connection = "ServiceBusConnection")]
            public OrderBilled OrderBilled { get; set; }

            [BlobOutput("invoices/invoice.txt", Connection = "BlobConnection")]
            public byte[] Blob { get; set; }
        }
    }
}
