using RabbitMQ.Client;
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
                HostName = Constant.HOST_NAME,
                Port = Constant.PORT,
                UserName = Constant.USER_NAME,
                Password = Constant.PASSWORD
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: Constant.QUEUE,
                        durable: Constant.DURABLE,
                        exclusive: Constant.EXCLUSIVE,
                        autoDelete: Constant.AUTO_DELETE,
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

                        config.Sum(1);

                        Console.WriteLine("To config object after sum");
                        Console.WriteLine(config);

                        //Unsuscribe
                        channel.BasicReject(ea.DeliveryTag, false);
                        Console.WriteLine(" [x] Done");
                        Console.WriteLine();
                    };


                    channel.BasicConsume(
                        queue: Constant.QUEUE,
                        autoAck: Constant.AUTO_ACK,
                        consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
