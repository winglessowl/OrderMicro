namespace AccountMicro.Events
{
    public class OrderAceptedEvent : IntegrationEvent
    {
        public int IdOrder { get; set; }
        public int ClientId { get; set; }
        public string Status { get; set; }
    }
}
