using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using System.Threading;

namespace Sender_001
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
            pr.Sum(Constant.a, Constant.b);
            pr.Notify -= SendMessage;
        }

        private static void SendMessage(Message message)
        {
            var factory = new ConnectionFactory() { HostName = Constant.HOST_NAME, Port = Constant.PORT, UserName = Constant.USERNAME, Password = Constant.PASSWORD };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {


                    channel.QueueDeclare(queue: Constant.QUEUE, durable: Constant.DURABLE, exclusive: Constant.EXCLUSIVE, autoDelete: Constant.AUTO_DELETE, arguments: Constant.ARGUMENTS);
                    
                    IMapMessageBuilder messageBuilder = new MapMessageBuilder(channel);

                   
                    messageBuilder.Body["applicationName"] = message.applicationName;
                    messageBuilder.Body["number"] = message.number;
                    messageBuilder.Body["date"] = message.date.ToString();


                    Thread.Sleep(4000);
                    channel.BasicPublish(
                        exchange: Constant.EXCHANGE,
                        routingKey:Constant.ROUTING_KEY,
                        basicProperties:Constant.BASIC_PROPERTIES,
                        body: messageBuilder.GetContentBody());
                    
                    Console.WriteLine(" [x] Sent {0}", message);
                    
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
