using Zack.EventBus;

namespace EventWebApi2
{
    [EventName("OrderCreated")]
    public class EventHandler3 : JsonIntegrationEventHandler<OrderData>
    {

        public override Task HandleJson(string eventName, OrderData? eventData)
        {
            Console.WriteLine("EventHandler3,eventData=" + eventData);
            return Task.CompletedTask;
        }
    }

   public record OrderData(long Id, string Name, DateTime Date);
}
