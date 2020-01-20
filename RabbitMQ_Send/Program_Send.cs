using System;
using RabbitMQ.Client;
using System.Text;


namespace RabbitMQ
{
    class Program_Send
    {
        static void Main(string[] args)
        {
            var factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "94.131.241.80", Port = 5672, UserName = "testmf3", Password = "As123456" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            //Console.WriteLine("Hello World!");
            //Console.WriteLine("Hello World!");
        }
    }
}
