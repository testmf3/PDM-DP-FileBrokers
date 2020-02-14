using System;
using System.Collections.Generic;

namespace sender_001
{
    class Message
    {
        public string applicationName { get; } = Constant.APPLICATON_NAME;
        public DateTime date { get; set; }
        public int number { get; set; }
        public List<string> sProject { get; set; }
        public List<string> sStage { get; set; }

        public Message(int number)
        {
            date = DateTime.Now;
            this.number = number;
        }

        public override string ToString()
        {
            return "name: " + applicationName + "\ndate: " + date;
        }
    }
}
