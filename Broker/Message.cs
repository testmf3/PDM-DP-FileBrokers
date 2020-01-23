using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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

        public Message()
        {

        }

        public Message ToMessage(Dictionary<String, String> configurationDictionary)
        {
            applicationName = configurationDictionary["applicationName"];

            CultureInfo provider = new CultureInfo("uk-UA");
            DateTime.Parse(configurationDictionary["date"], provider, DateTimeStyles.NoCurrentDateDefault);

            number = int.Parse(configurationDictionary["number"]);
            return this;
        }

        public override string ToString()
        {
            return "Name:  " + applicationName + "\nDate:  " + date + "\nArgument:  " + number;
        }

    }



}
