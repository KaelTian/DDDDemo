using Zack.EventBus;

namespace EventWebApi2
{
    [EventName("OrderCreated")]
    [EventName("OrderDeleted")]
    public class EventHandler1 : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            if(eventName== "OrderCreated")
            {
                Console.WriteLine("收到了订单,eventData="+eventData); 
            }
            return Task.CompletedTask;
        }
    }
}
