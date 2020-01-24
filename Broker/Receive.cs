using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Events;


namespace Broker
{
    class Receive
    {
        Message message;

        public Message Connect()
        {
            var factory = new ConnectionFactory()
            {
                HostName = Constant.SENDER_001.hostName,
                Port = Constant.SENDER_001.port,
                UserName = Constant.SENDER_001.userName,
                Password = Constant.SENDER_001.password
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: Constant.HELLO1_QUEUE, Constant.DURABLE,
                        Constant.EXCLUSIVE, Constant.AUTO_DELETE, Constant.ARGUMENTS);
                  
                    var consumer = new EventingBasicConsumer(channel);
                   
                   
                    consumer.Received += (model, ea) =>    
                    {  
                        IMapMessageReader messageReader = new MapMessageReader(ea.BasicProperties, ea.Body);
                        message = message.ToMessage(messageReader);   
                        Console.WriteLine(message);
                    };
                    
                    channel.BasicConsume(queue: Constant.HELLO1_QUEUE, Constant.AUTO_ACK, consumer: consumer);
                    return message;
                }
            }
        }
    }
}
