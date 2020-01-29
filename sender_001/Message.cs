using System;

namespace sender_001
{
    class Message
    {
        public string applicationName { get; set; }
        public DateTime date { get; set; }
        public int number { get; set; }

        public string type { get; set; }
        public Message(int number)
        {
            date = DateTime.Now;
            this.number = number;
        }

        public override string ToString()
        {
            return "name: " + applicationName + 
                "\ndate: " + date + 
                "\nnumber: " + number + 
                "\ntype: " + type;
        }

        public Message() {
            date = DateTime.Now;
        }
    }
}
