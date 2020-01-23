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

        public Message ToMessage(Dictionary<String, String> configurationDictionary)
        {

            applicationName = configurationDictionary["applicationName"];
            date = DateTime.Parse(configurationDictionary["date"]);
            number = int.Parse(configurationDictionary["number"]);

            return this;
        }

        public override string ToString()
        {
            return "Name:  " + applicationName + "\nDate:  " + date + "\nArgument:  " + number;
        }

    }



}
