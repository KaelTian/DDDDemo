using Zack.EventBus;

namespace EventWebApi2
{
    [EventName("OrderCreated")]
    public class EventHandler2 : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            if (eventName == "OrderCreated")
            {
                Console.WriteLine("我是微服务2,eventData=" + eventData);
            }
            return Task.CompletedTask;
        }
    }
}
