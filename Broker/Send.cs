using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.MessagePatterns;
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

                    //Message body
                    IMapMessageBuilder messageBuilder = new MapMessageBuilder(channel);

                    //messageBuilder.Body["applicationName"] = message.applicationName;
                    //messageBuilder.Body["date"] = message.date.ToString();
                    messageBuilder.Body["number"] = message.number;


                    //Suscribe
                    channel.ConfirmSelect();
                    channel.BasicNacks += (sender, e) =>
                    {
                        Console.Write("Not received" + sender);
                    };

                    //Properties
                    var properties = channel.CreateBasicProperties();

                    //Publish
                    channel.BasicPublish(exchange: "", routingKey: "hello", 
                        basicProperties: properties, body: messageBuilder.GetContentBody());
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
