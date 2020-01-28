using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Events;


namespace broker
{
    class Receive
    {
        public void Connect(ref Message receivedMessage)
        {

            var factory = new ConnectionFactory() { 
                HostName = Constant.SENDER_001.hostName, 
                Port = Constant.SENDER_001.port, 
                UserName = Constant.SENDER_001.userName, 
                Password = Constant.SENDER_001.password
            };


            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    
                    channel.QueueDeclare(
                        queue: Constant.RECEIVE_QUEUE, 
                        durable: Constant.DURABLE, 
                        exclusive: Constant.EXCLUSIVE, 
                        autoDelete: Constant.AUTO_DELETE, 
                        arguments: Constant.ARGUMENTS);

                    //Create message
                    Message message = new Message();

                    //Create consumer
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {

                        //Build message
                        IMapMessageReader messageReader = new MapMessageReader(ea.BasicProperties, ea.Body);
                        message.ToMessage(messageReader);

                        Console.WriteLine("Receive message: ");
                        Console.WriteLine(message);

                    };

                    receivedMessage = message;
                    channel.BasicConsume(queue: Constant.RECEIVE_QUEUE, autoAck: Constant.AUTO_ACK, consumer: consumer);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
