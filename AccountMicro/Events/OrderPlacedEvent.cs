namespace AccountMicro.Events
{
    public class OrderPlacedEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int IdClient { get; set; }
    }
}
