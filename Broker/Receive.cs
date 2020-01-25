using System;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Events;


namespace Broker
{
    class Receive
    {
        Message message = new Message();

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
                    
                    Thread.Sleep(2000);
                    consumer.Received += (model, ea) =>    
                    {  
                        IMapMessageReader messageReader = new MapMessageReader(ea.BasicProperties, ea.Body);
                        message = message.ToMessage(messageReader);
                      
                        Console.WriteLine("Receive message: ");
                        Console.WriteLine(message);

                    };

                    channel.BasicConsume(queue: Constant.HELLO1_QUEUE, Constant.AUTO_ACK, consumer: consumer);
                   
                    return message;
                }
            }
        }
    }
}
