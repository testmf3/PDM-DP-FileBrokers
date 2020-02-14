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
    public class ObjectTypesConverter : JsonConverter<Service.Model.DataStructCreatorConfig>
    {
        public override Service.Model.DataStructCreatorConfig Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Service.Model.DataStructCreatorConfig objRoot = new Service.Model.DataStructCreatorConfig();
            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                //if (reader.TokenType != JsonTokenType.StartObject)
                //{
                //    throw new JsonException();
                //}

                //if (reader.TokenType != JsonTokenType.PropertyName)
                //{
                //    throw new JsonException();
                //}
                //reader.Read();
                string propertyName = reader.GetString();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return objRoot;
                    }
                    //string propertyName = reader.GetString();
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        propertyName = reader.GetString();
                        reader.Read();
                        switch (propertyName)
                        {
                            case "CreditLimit":
                                decimal creditLimit = reader.GetDecimal();
                                //((Customer)person).CreditLimit = creditLimit;
                                break;
                            case "OfficeNumber":
                                string officeNumber = reader.GetString();
                                //((Employee)person).OfficeNumber = officeNumber;
                                break;
                            case "Name":
                                string name = reader.GetString();
                                //person.Name = name;
                                break;
                        }
                    }
                }
            }
            return objRoot;
        }
        public override void Write(Utf8JsonWriter writer, Service.Model.DataStructCreatorConfig objectToWrite, JsonSerializerOptions options)
        {
                var sdfss = 1;
        }
    }

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
                                HostName    = Constant.WORKER_001.hostName,
                                Port        = Constant.WORKER_001.port,
                                UserName    = Constant.WORKER_001.userName,
                                Password    = Constant.WORKER_001.password
                            };
                        }
                        break;
                    case "MSFlow_ProjectCreation":
                        {
                            using (var process = new Process())
                            {
                                
                                var serializeOptionsRead = new JsonSerializerOptions();
                                serializeOptionsRead.Converters.Add(new ObjectTypesConverter());
                                var obj = JsonSerializer.Deserialize<Service.Model.DataStructCreatorConfig>(File.ReadAllText(@"C:\Users\d.radomtsev\source\repos\PDM.IO.FileBrokers.DataStructureSetup\bin\Release\netcoreapp3.0\appsettings.json"), serializeOptionsRead);
                                obj.configset.Data.Project = message.sProject.ToArray();
                                obj.configset.Data.StagePhase = message.sStage.ToArray();

                                var serializeOptionsWrite = new JsonSerializerOptions();
                                serializeOptionsWrite.WriteIndented = true;
                                File.WriteAllText(@"C:\Users\d.radomtsev\source\repos\PDM.IO.FileBrokers.DataStructureSetup\bin\Release\netcoreapp3.0\appsettings.json", JsonSerializer.Serialize<Service.Model.DataStructCreatorConfig>(obj, serializeOptionsWrite));
                                process.StartInfo.FileName = @"C:\Users\d.radomtsev\source\repos\PDM.IO.FileBrokers.DataStructureSetup\bin\Release\netcoreapp3.0\PDM.IO.FileBrokers.DataStructureSetup.exe";
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
