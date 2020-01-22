using System;
using System.Collections.Generic;
using System.Text;

namespace Sender_001
{
    class Message
    {
        public string applicationName { get; } = "Sender-001";
        public int number { get; set; }

        public Message(int num)
        {
            number = num;
        }
    }
}
