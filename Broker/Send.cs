using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Broker
{
    class Send
    {

        public /*static */void Connect(DestinationInfo destinationInfo, Message message)
        {
            var factory = new RabbitMQ.Client.ConnectionFactory() { 
                HostName = destinationInfo.HostName, 
                Port = destinationInfo.Port, 
                UserName = destinationInfo.UserName, 
                Password = destinationInfo.Password
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
