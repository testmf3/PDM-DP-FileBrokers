using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace broker
{
    class Send
    {
        private IConfiguration config;

        List<Config> configuration;

        public void getConfig()
        {
            string configPath = Path.GetDirectoryName(Assembly
                .GetEntryAssembly()
                .Location.Substring(0, Assembly.GetEntryAssembly()
                .Location.IndexOf("bin\\")));

            config = new ConfigurationBuilder()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();


            configuration = new List<Config>();
            config.GetSection("config").Bind(configuration);        
        }


        public void ExchangeLoop(Message message) {
            getConfig();
            configuration.FindAll(config=>config.type == message.type).ForEach(config => Exchange(message, config));
        }


        public void Exchange(Message message, Config config)
        {
           
            //TODO factory in config
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
                    case "sender_002":
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
            string queue = message.type+"_new";
            string routingKey = message.type + "_new";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: queue, 
                        durable: Constant.DURABLE, 
                        exclusive: Constant.EXCLUSIVE, 
                        autoDelete: Constant.AUTO_DELETE, 
                        arguments: Constant.ARGUMENTS);


                    //Message builder
                    IMapMessageBuilder messageBuilder = new MapMessageBuilder(channel);
                    messageBuilder.Body["number"] = message.number;

                    channel.ExchangeDeclare("logs", ExchangeType.Fanout);

                    channel.QueueBind(
                        queue: queue,
                        exchange: "logs",
                        routingKey: routingKey);

                    /*
                    //Suscribe
                    channel.ConfirmSelect();
                    channel.BasicNacks += (sender, e) =>
                    {
                        Console.Write("Not received" + message);
                    };
                    */

                    IBasicProperties props = channel.CreateBasicProperties();
                    props.DeliveryMode = 2;
  
                    channel.BasicPublish(
                        exchange: Constant.EXCHANGE,
                        routingKey: routingKey, 
                        basicProperties: props, 
                        body: messageBuilder.GetContentBody());

                    Console.WriteLine(" [x] Sent {0}", message);
                    Console.WriteLine();
                }
            }
        }
    }
}
