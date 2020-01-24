using System;

namespace Sender_001
{
    class Message
    {
        public string applicationName { get; } = Constant.APPLICATIONNAME;
        public DateTime date { get; set; }
        public int number { get; set; }

        public Message(int number)
        {
            date = DateTime.Now;
            this.number = number;
        }

    }
}
