using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using System;
using System.Collections.Generic;
using System.IO;

namespace broker
{
    class Send
    {
        private IConfiguration config;

        List<Config> configuration;

        public void getConfig()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();


            configuration = new List<Config>();
            config.GetSection("config").Bind(configuration);

            /*
            //Test
            Console.WriteLine("All worker");
            configuration.ForEach(
                config => Console.WriteLine(config)
                );*/
        }


        public void ExchangeLoop(Message message) {
            
            getConfig();

            //Test
            //Console.WriteLine("Find worker by type");
            //configuration.FindAll(x => x.type == message.type).ForEach(x => Console.WriteLine(x));
            
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
                                //HostName = config.applicationName,
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
