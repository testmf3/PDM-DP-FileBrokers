using RabbitMQ.Client.Content;
using System;
using System.Collections.Generic;
using System.Globalization;

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

            this.applicationName = messageReader.Body["applicationName"].ToString();
            this.date = DateTime.Parse(messageReader.Body["date"].ToString());
            this.number = int.Parse(messageReader.Body["number"].ToString());

        }

        public override string ToString()
        {
            return "Name:  " + applicationName + "\nDate:  " + date + "\nArgument:  " + number;
        }

    }



}
