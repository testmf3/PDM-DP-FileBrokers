using System;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Events;


namespace broker
{
    class Receive
    {
        public void Connect()
        {
            var factory = new ConnectionFactory() { 
                HostName = "94.131.241.80", 
                Port = 5672, 
                UserName = "testmf3", 
                Password = "As123456" };


            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {

                        Console.WriteLine("Receive message: ");
                        IMapMessageReader messageReader = new MapMessageReader(ea.BasicProperties, ea.Body);
                        Message message = new Message();
                        message.ToMessage(messageReader);
                        Console.WriteLine(messageReader.Body["applicationName"].ToString());

                    };

                    channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
