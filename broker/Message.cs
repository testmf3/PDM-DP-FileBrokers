using RabbitMQ.Client;
using RabbitMQ.Client.Content;

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ARM.PDM.broker
{
    class Message
    {
        public string applicationName;
        public DateTime date;
        public int number;
        public List<string> sProject = new List<string>();
        public List<string> sStage = new List<string>();

        public Service.Model.Message objMessage;

        public Message(string applicationName, DateTime date, int number, string sProject, string sStage)
        {
            this.applicationName = applicationName;
            this.date = date;
            this.number = number;
            //this.sProject.Add(sProject);
            //this.sStage.Add(sStage);
        }

        public Message() { }

        public void ToMessage(IMapMessageReader messageReader, ref IBasicProperties basicProps)
        {
            switch (basicProps.ContentType)
            {
                case "application/json":
                    {
                        messageReader.BodyStream.Position = 0;
                        StreamReader strMessage = new StreamReader(messageReader.BodyStream);
                        objMessage = JsonSerializer.Deserialize<ARM.PDM.Service.Model.Message>(strMessage.ReadToEnd());
                        applicationName = objMessage.applicationName;
                        date = DateTime.Parse(objMessage.date);
                        number = objMessage.data;
                        sProject.Add(objMessage.Project);
                        sStage.Add(objMessage.Stage);
                    } break;
                case "text/plain":
                    {
                        applicationName = messageReader.Body["applicationName"].ToString();
                        date = DateTime.Parse(messageReader.Body["date"].ToString());
                        number = int.Parse(messageReader.Body["number"].ToString());
                    } break;
                default:
                    {
                        Console.WriteLine("Undefined content type");
                        return;
                    }
            }
        }

        public override string ToString()
        {
            return "Name:  " + applicationName + "\nDate:  " + date + "\nArgument:  " + number;
        }

    }



}
