using Zack.EventBus;

namespace EventWebApi2
{
    [EventName("OrderCreated")]
    public class EventHandler4 : DynamicIntegrationEventHandler
    {
        public override Task HandleDynamic(string eventName, dynamic eventData)
        {
            Console.WriteLine($"Dynamic Handler,Id:{eventData.Id} Name:{eventData.Name} Date:{eventData.Date}");
            return Task.CompletedTask;
        }
    }
}
