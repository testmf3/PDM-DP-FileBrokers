using RabbitMQ.Client.Content;
using System;
using System.Collections.Generic;

namespace Worker_001
{
    class Config
    {
        public int number { get; set; }
        public string applicationName { get; set; }
        public void ToConfig(IMapMessageReader messageReader) {
            number = int.Parse(messageReader.Body["number"].ToString());
            //applicationName = messageReader.Body["applicationName"].ToString();
        }

        public int Sum(int value) {
            this.number = this.number + value;
            return this.number;
        }

        public override string ToString()
        {
            return "Name:  " + applicationName   + "\nArgument:  " + number;
        }
    }
}
