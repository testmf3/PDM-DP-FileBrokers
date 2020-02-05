using RabbitMQ.Client;
using System.Collections.Generic;

namespace broker
{
    class Constant
    {
        public static readonly DestinationInfo SENDER_001 = new DestinationInfo
        {
            hostName = "94.131.241.80",
            port = 5672,
            userName = "testmf3",
            password = "As123456"
        };

        public static readonly DestinationInfo WORKER_001 = new DestinationInfo
        {
            hostName = "94.131.241.80",
            port = 5672,
            userName = "testmf2",
            password = "As123456"
        };


        //Queue Receive Declare 
        public static readonly string RECEIVE_QUEUE = "sender_new";
        public static readonly bool DURABLE = true;
        public static readonly bool EXCLUSIVE = false;
        public static readonly bool AUTO_DELETE = true;
        public static readonly IDictionary<string, object> ARGUMENTS = null;


        //Basic Consume
        public static readonly bool AUTO_ACK = false;


        //Queue Send Declare 
        public static readonly string EXCHANGE = "";
    }
}
