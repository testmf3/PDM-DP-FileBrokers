using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;


namespace Sender_001
{
    class Program
    {
        public int number;

        public delegate void Handler(string message);
        public event Handler Notify;


        public void Sum(int a, int b)
        {
            number = a + b;
            Notify?.Invoke(JsonConvert.SerializeObject(new Message(number)));
        }

        static void Main(string[] args)
        {
            Program pr = new Program();

            pr.Notify += SendMessage;
            pr.Sum(Constant.a, Constant.b);
            pr.Notify -= SendMessage;
        }

        private static void SendMessage(string message)
        {
            var factory = new RabbitMQ.Client.ConnectionFactory() { HostName = Constant.hostName, Port = Constant.port, UserName = Constant.userName, Password = Constant.password };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: Constant.queue, durable: Constant.durable, exclusive: Constant.exclusive, autoDelete: Constant.autoDelete, arguments: Constant.arguments);

                    var body = Encoding.UTF8.GetBytes(message);
              
                    channel.BasicPublish(exchange: Constant.exchange, routingKey: Constant.routingKey, basicProperties: Constant.basicProperties, body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
