using System;
using System.IO;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using RabbitMQ.Client.Events;

using System.Diagnostics;

using System.Text.Json;
using System.Text.Json.Serialization;


namespace ARM.PDM.broker
{
    class Receive
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
                        }
                        break;
                    case "MSFlow_ProjectCreation":
                        {
                            using (var process = new Process())
                            {
                                var obj = JsonSerializer.Deserialize<Service.Model.DataStructCreatorConfig>(File.ReadAllText(@"C:\Users\d.radomtsev\source\repos\PDM.IO.FileBrokers.DataStructureSetup\bin\Release\netcoreapp3.0\appsettings.json"));
                                process.StartInfo.FileName = @"..\PDM.IO.FileBrokers.DataStructureSetup\bin\Release\netcoreapp3.0\PDM.IO.FileBrokers.DataStructureSetup.exe";
                                process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
                                process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
                                process.Start();
                                //process.BeginOutputReadLine();
                                //process.BeginErrorReadLine();
                            }
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Not catch");
                            return;
                        };
                }
            }
        }

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
                        queue: Constant.SEND_QUEUE, 
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
                        IBasicProperties basicProps = ea.BasicProperties;
                        //Build message
                        IMapMessageReader messageReader = new MapMessageReader(ea.BasicProperties, ea.Body);
                        message.ToMessage(messageReader, ref basicProps);
                        Exchange(message);
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
