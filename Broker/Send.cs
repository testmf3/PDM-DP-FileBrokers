using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Broker
{
    class Send
    {

        public void Connect(DestinationInfo destinationInfo, Message message)
        {
            var factory = new ConnectionFactory() { 
                HostName = destinationInfo.hostName, 
                Port = destinationInfo.port, 
                UserName = destinationInfo.userName, 
                Password = destinationInfo.password
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));


                    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
