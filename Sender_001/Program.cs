using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;



namespace Sender_001
{
    class Program
    {
        public string applicationName = "Sender-001";
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
            pr.Sum(3, 1);
            pr.Notify -= SendMessage;
        }
        private static void SendMessage(string message)
        {
            var factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "94.131.241.80", Port = 5672, UserName = "testmf2", Password = "As123456" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

              
                    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            

//            Console.WriteLine(message);
        }
    }
}
