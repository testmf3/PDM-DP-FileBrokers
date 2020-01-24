﻿using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using RabbitMQ.Client.Content;
using System.Threading;

namespace Sender_001
{
    class Program
    {
        public static int count = 10;
        public int number;

        public delegate void Handler(Message message);
        public event Handler Notify;


        public void Sum(int a, int b)
        {
            number = a + b;
            
            Notify?.Invoke(new Message(number));
        }

        static void Main(string[] args)
        {
            Program pr = new Program();

            pr.Notify += SendMessage;
            pr.Sum(Constant.a, Constant.b);
            pr.Notify -= SendMessage;
        }

        private static void SendMessage(Message message)
        {
            var factory = new ConnectionFactory() { HostName = Constant.hostName, Port = Constant.port, UserName = Constant.userName, Password = Constant.password };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(queue: Constant.queue, durable: Constant.durable, exclusive: Constant.exclusive, autoDelete: Constant.autoDelete, arguments: Constant.arguments);
                    
                    IMapMessageBuilder messageBuilder = new MapMessageBuilder(channel);

                   
                    messageBuilder.Body["applicationName"] = message.applicationName;
                    messageBuilder.Body["number"] = message.number;
                    messageBuilder.Body["date"] = message.date.ToString();

                    IBasicProperties props = channel.CreateBasicProperties();



                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "hello",
                        props,
                        body: messageBuilder.GetContentBody());

                    Console.WriteLine(" [x] Sent {0}", message);
                    
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
