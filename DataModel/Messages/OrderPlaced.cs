namespace DataModel.Messages
{
    public class OrderPlaced : BaseMessage
    {
        public override string Type => nameof(OrderPlaced);
        public string OrderId { get; set; }
    }
}