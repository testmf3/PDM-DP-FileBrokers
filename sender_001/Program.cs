using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using System.Threading;

namespace sender_001
{
    class Program
    {

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
            pr.Sum(5, 4);
            pr.Notify -= SendMessage;
        }

        private static void SendMessage(Message message)
        {
           
            var factory = new RabbitMQ.Client.ConnectionFactory() { 
                HostName = "94.131.241.80", 
                Port = 5672, 
                UserName = "testmf3", 
                Password = "As123456" };


            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    IMapMessageBuilder messageBuilder = new MapMessageBuilder(channel);


                    messageBuilder.Body["applicationName"] = message.applicationName;
                    messageBuilder.Body["number"] = message.number;
                    messageBuilder.Body["date"] = message.date.ToString();


                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: messageBuilder.GetContentBody());

                    Console.WriteLine(" [x] Sent {0}", message);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }

        }
    }
}
