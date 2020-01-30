using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;

namespace sender_001
{
    class Program
    {
       
        private Message message;
        public delegate void Handler(Message message);
        public event Handler Notify;

        private IConfigurationRoot config;
        private IConfigurationBuilder builder;

        Program() {
            message = new Message();
        }

        public void Sum(int a, int b)
        {

            message.number = a + b;
            Notify?.Invoke(message);
        }

        public void getConfig() {

            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();

            message.applicationName = config["applicationName"];
            message.type = config["type"];
        }
        static void Main(string[] args)
        {

            Program program = new Program();

            program.getConfig();


            //Test in real time
            while (true)
            {
                Console.WriteLine("a: ");
                int a = int.Parse(Console.ReadLine());
                Console.WriteLine("b: ");
                int b = int.Parse(Console.ReadLine());
                program.Notify += program.SendMessage;
                program.Sum(a, b);
                program.Notify -= program.SendMessage;

            }
        }

        private void SendMessage(Message message)
        {

        
            var factory = new ConnectionFactory() { 
                HostName = Constant.HOST_NAME, 
                Port = Constant.PORT, 
                UserName = Constant.USER_NAME, 
                Password = Constant.PASSWORD };


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


                    IMapMessageBuilder messageBuilder = new MapMessageBuilder(channel);


                    messageBuilder.Body["applicationName"] = message.applicationName;
                    messageBuilder.Body["type"] = message.type;
                    messageBuilder.Body["number"] = message.number;
                    messageBuilder.Body["date"] = message.date.ToString();
                   
               

                    channel.BasicPublish(
                        exchange: Constant.EXCHANGE,
                        routingKey: Constant.ROUTING_KEY,
                        basicProperties: Constant.PROPERTIES,
                        body: messageBuilder.GetContentBody());

                    Console.WriteLine(" [x] Sent {0}", message);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
            
        }
    }
}
