using DataModel.Models;

namespace DataModel.Messages
{
    public class PlaceOrder : BaseMessage
    {
        public override string Type => nameof(PlaceOrder);
        public string OrderId { get; set; }
        public Product Product { get; set; }
        public string Buyer { get; set; }
    }
}
