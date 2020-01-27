using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using System;

namespace broker
{
    class Send
    {
        public void Connect(DestinationInfo destination, Message message)
        {
            Console.WriteLine("Check destination: ");
            Console.WriteLine(destination);
            Console.WriteLine();

            var factory = new RabbitMQ.Client.ConnectionFactory() { 
                HostName = destination.hostName, 
                Port = destination.port, 
                UserName = destination.userName, 
                Password = destination.password
            };


            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello1", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    IMapMessageBuilder messageBuilder = new MapMessageBuilder(channel);
                    messageBuilder.Body["number"] = message.number;

                    /*
                    //Suscribe
                    channel.ConfirmSelect();
                    channel.BasicNacks += (sender, e) =>
                    {
                        Console.Write("Not received" + message);
                    };
                    */

  
                    channel.BasicPublish(exchange: "", routingKey: "hello1", basicProperties: null, body: messageBuilder.GetContentBody());
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
