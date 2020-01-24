using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
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
                HostName = Constant.HOSTNAME, 
                Port = Constant.PORT, 
                UserName = Constant.USERNAME, 
                Password = Constant.PASSWORD
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: Constant.HOSTNAME, 
                        durable: Constant.DURABLE, 
                        exclusive: Constant.EXCLUSIVE, 
                        autoDelete: Constant.AUTODELETE, 
                        arguments: Constant.ARGUMENTS);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        //Message body
                        IMapMessageReader messageReader = new MapMessageReader(ea.BasicProperties, ea.Body);

                        Config config = new Config();

                        config.ToConfig(messageReader);
                        Console.WriteLine("To config object");
                        Console.WriteLine(config);

                        config.Sum(Constant.NUMBER);

                        Console.WriteLine("To config object after sum");
                        Console.WriteLine(config);

                        //Unsuscribe
                        channel.BasicReject(ea.DeliveryTag, false);
                        Console.WriteLine(" [x] Done");
                        
                    };
                    channel.BasicConsume(
                        queue: Constant.QUEUE, 
                        autoAck: Constant.AUTOACK, 
                        consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        } 
    } 
}
   
