using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using System;

namespace broker
{
    class Send
    {


        public void Exchange(Message message)
        {

            ConnectionFactory factory = null;

            if (message != null)
            {
                //TODO switch
                switch (message.applicationName)
                {
                    case "sender_001":
                        {

                            factory = new ConnectionFactory()
                            {
                                HostName = Constant.WORKER_001.hostName,
                                Port = Constant.WORKER_001.port,
                                UserName = Constant.WORKER_001.userName,
                                Password = Constant.WORKER_001.password
                            };

                           
                        }; break;


                    default:
                        {
                            Console.WriteLine("Not catch");
                            return;
                        };
                }


                Connect(factory, message); 
            }
        }


        private void Connect(ConnectionFactory factory, Message message)
        {
            Console.WriteLine("Check destination: ");
            Console.WriteLine(factory.UserName);
            Console.WriteLine();


            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: Constant.SEND_QUEUE, 
                        durable: Constant.DURABLE, 
                        exclusive: Constant.EXCLUSIVE, 
                        autoDelete: Constant.AUTO_DELETE, 
                        arguments: Constant.ARGUMENTS);

                    //Message builder
                    IMapMessageBuilder messageBuilder = new MapMessageBuilder(channel);
                    messageBuilder.Body["number"] = message.number;

                    /*
                    //Suscribe
                    channel.ConfirmSelect();
                    channel.BasicNacks += (sender, e) =>
                    {
                        Console.Write("Not received" + message);
                    };
                    */

                    channel.BasicPublish(
                        exchange: Constant.EXCHANGE, 
                        routingKey: Constant.ROUTING_KEY, 
                        basicProperties: Constant.PROPERTIES, 
                        body: messageBuilder.GetContentBody());

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
