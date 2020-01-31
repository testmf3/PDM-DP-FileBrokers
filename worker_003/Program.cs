using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Events;
using System;
using System.IO;

namespace worker_003
{
    class Program
    {
        private static Program program = new Program();
        private IConfigurationRoot config;
        private IConfigurationBuilder builder;
        private static string queue;
        public void getConfig()
        {

            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();
            queue = config["type"];
        }

        static void Main(string[] args)
        {
            program.getConfig();
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
                        queue: queue,
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

                        config.Sum(3);

                        Console.WriteLine("To config object after sum");
                        Console.WriteLine(config);

                        //Unsuscribe
                        channel.BasicReject(ea.DeliveryTag, false);
                        Console.WriteLine(" [x] Done");
                        Console.WriteLine();

                    };


                    channel.BasicConsume(
                        queue: queue,
                        autoAck: Constant.AUTO_ACK,
                        consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
