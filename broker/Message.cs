using RabbitMQ.Client.Content;
using System;

namespace broker
{
    class Message
    {
        public string applicationName;
        public DateTime date;
        public int number;
        public string type;

        public Message(string applicationName, DateTime date, int number, string type)
        {
            this.applicationName = applicationName;
            this.date = date;
            this.number = number;
            this.type = type;
        }

        public Message() { }

        public void ToMessage(IMapMessageReader messageReader)
        {

            applicationName = messageReader.Body["applicationName"].ToString();
            date = DateTime.Parse(messageReader.Body["date"].ToString());
            number = int.Parse(messageReader.Body["number"].ToString());
            type = messageReader.Body["type"].ToString();

        }

        public override string ToString()
        {
            return "Name:  " + applicationName + "\nDate:  " + date + "\nArgument:  " + number + "\ntype: " + type;
        }

    }



}
