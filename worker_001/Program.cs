﻿using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Events;
using System;

namespace worker_001
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "94.131.241.80",
                Port = 5672,
                UserName = "testmf2",
                Password = "As123456"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "hello1",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        //Message body
                        IMapMessageReader messageReader = new MapMessageReader(ea.BasicProperties, ea.Body);

                        Config config = new Config();

                        config.ToConfig(messageReader);
                        Console.WriteLine("To config object");
                        Console.WriteLine(config);

                        config.Sum(1);

                        Console.WriteLine("To config object after sum");
                        Console.WriteLine(config);

                        //Unsuscribe
                        channel.BasicReject(ea.DeliveryTag, false);
                        Console.WriteLine(" [x] Done");

                    };
                    channel.BasicConsume(
                        queue: "hello1",
                        autoAck: false,
                        consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }

        }
    }
}
