namespace DataModel.Messages
{
    public class OrderBilled : BaseMessage
    {
        public override string Type => nameof(OrderBilled);

        public string OrderId { get; set; }
    }
}