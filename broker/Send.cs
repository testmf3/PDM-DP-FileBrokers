using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ARM.PDM.broker
{
    class Send
    {
        public IConfiguration Configuration { get; set; }

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
                                var builder = new ConfigurationBuilder().AddJsonFile(@"..\PDM.IO.FileBrokers.DataStructureSetup\bin\Release\netcoreapp3.0\appsettings.json");
                                Configuration = builder.Build();
                                Configuration["AllowedHosts"] = "ssdf";
                                process.StartInfo.FileName = @"..\PDM.IO.FileBrokers.DataStructureSetup\bin\Release\netcoreapp3.0\PDM.IO.FileBrokers.DataStructureSetup.exe";
                                process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
                                process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
                                //process.Start();
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
