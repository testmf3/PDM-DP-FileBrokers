using RabbitMQ.Client.Content;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Broker
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

        public Message(){}

        public Message ToMessage(IMapMessageReader messageReader)
        {

            applicationName = messageReader.Body["applicationName"].ToString();
            date = DateTime.Parse(messageReader.Body["date"].ToString());
            number = int.Parse(messageReader.Body["number"].ToString());

            return this;
        }

        public override string ToString()
        {
            return "Name:  " + applicationName + "\nDate:  " + date + "\nArgument:  " + number;
        }

    }



}
