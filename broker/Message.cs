using RabbitMQ.Client.Content;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;

namespace broker
{
    class Message
    {
        public string applicationName;
        public DateTime date;
        public int number;

        public Message(string applicationName, DateTime date, int number)
        {
            this.applicationName = applicationName;
            this.date = date;
            this.number = number;
        }

        public Message() { }

        public void ToMessage(IMapMessageReader messageReader)
        {
            
            applicationName = messageReader.Body["applicationName"].ToString();
            date = DateTime.Parse(messageReader.Body["date"].ToString());
            number = int.Parse(messageReader.Body["number"].ToString());

            IMapMessageReader messageReadercopy = messageReader;
            messageReadercopy.BodyStream.Position = 0;
            StreamReader dfdg = new StreamReader(messageReadercopy.BodyStream);
            string stream = dfdg.ReadToEnd();
        }

        public override string ToString()
        {
            return "Name:  " + applicationName + "\nDate:  " + date + "\nArgument:  " + number;
        }

    }



}
