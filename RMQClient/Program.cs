using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.HostName = "127.0.0.1";
factory.DispatchConsumersAsync = true;
string exchangeName = "exchange1";
string eventName = "myEvent";
string queueName = "queue1";
using var conn = factory.CreateConnection();
while (true)
{
    string msg = DateTime.Now.TimeOfDay.ToString();//待发送消息
    using (var channel = conn.CreateModel())//创建信道
    {
        var properties = channel.CreateBasicProperties();
        properties.DeliveryMode = 2;
        channel.ExchangeDeclare(exchange: exchangeName, type: "direct");//声明交换机
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: eventName);
        byte[] body = Encoding.UTF8.GetBytes(msg);
        channel.BasicPublish(exchange: exchangeName, routingKey: eventName, mandatory: true,
            basicProperties: properties, body: body);//发布消息
    }
    Console.WriteLine("发布了消息:" + msg);
    Thread.Sleep(1000);
}