using System.Collections.Generic;

namespace Broker
{
    public static class Constant
    {
        public static readonly DestinationInfo WORKER_001 = new DestinationInfo
        {
            destination = "Worker-001",
            hostName = "94.131.241.80",
            port = 5672,
            userName = "testmf3",
            password = "As123456"
        };

        public static readonly DestinationInfo SENDER_001 = new DestinationInfo
        {
            destination = "Sender-001",
            hostName = "94.131.241.80",
            port = 5672,
            userName = "testmf2",
            password = "As123456"
        };

        public static readonly string HELLO1_QUEUE = "hello1";
        public static readonly string HELLO2_QUEUE = "hello2";

        public static string QUEUE = "hello";
        public static bool DURABLE = false;
        public static bool EXCLUSIVE = false;
        public static bool AUTO_DELETE = false;
        public static IDictionary<string, object> ARGUMENTS = null;

        public static bool AUTO_ACK = true;

    }
}

