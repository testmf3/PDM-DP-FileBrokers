using RabbitMQ.Client.Content;
using System;

namespace broker
{
    class Message
    {
        public string applicationName { get; set; }

        public string type { get; set; }
        public DateTime date { get; set; }
        public int number { get; set; }

        public Message(string applicationName, string type, DateTime date, int number)
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
