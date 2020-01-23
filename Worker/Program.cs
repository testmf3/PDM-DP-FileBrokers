using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_001
{
    class Program
    { 

        static void Main(string[] args)
        {

            var factory = new ConnectionFactory()
            {
                HostName = "94.131.241.80", Port = 5672,
                UserName = "testmf3", Password = "As123456"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;

                        var configurationString = Encoding.Default.GetString(body);
                        Dictionary<String, String> configurationDictionary = JsonConvert.DeserializeObject<Dictionary<String, String>>(configurationString);

                        Config config = new Config();

                        config.ToConfig(configurationDictionary);
                        Console.WriteLine("To config object");
                        Console.WriteLine(config);

                        config.Sum(5);

                        Console.WriteLine("To config object");
                        Console.WriteLine(config);

                        //TODO answer

                    };
                    channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        } 
    } 
}
   
