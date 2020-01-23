using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Broker
{
    class Receive
    {
        Message message;

        Message ParseByte(byte[] body)
        {
            var configurationString = Encoding.Default.GetString(body);
            Dictionary<String, String> configurationDictionary = JsonConvert.DeserializeObject<Dictionary<String, String>>(configurationString);

            return new Message().ToMessage(configurationDictionary);
        }

        public Message connect()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "94.131.241.80",
                Port = 5672,
                UserName = "testmf3",
                Password = "As123456"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        message = ParseByte(ea.Body);
                    };
                    channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();

                    return message;
                }
            }
        }
    }
}
