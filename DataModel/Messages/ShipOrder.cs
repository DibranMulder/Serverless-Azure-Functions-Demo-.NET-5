using DataModel.Messages;

namespace DataModel.Commands
{
    public class ShipOrder : BaseMessage
    {
        public override string Type => nameof(ShipOrder);
        public string OrderId { get; set; }
    }
}