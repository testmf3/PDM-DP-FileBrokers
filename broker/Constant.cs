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
            password = "As123456",
            destination = "sender_001"
        };

        public static readonly DestinationInfo WORKER_001 = new DestinationInfo
        {
            hostName = "94.131.241.80",
            port = 5672,
            userName = "testmf2",
            password = "As123456",
            destination = "worker_001"
        };


        //Queue Receive Declare 
        public static readonly string RECEIVE_QUEUE = "hello3";
        public static readonly bool DURABLE = false;
        public static readonly bool EXCLUSIVE = false;
        public static readonly bool AUTO_DELETE = false;
        public static readonly IDictionary<string, object> ARGUMENTS = null;


        //Basic Consume
        public static readonly bool AUTO_ACK = false;

        //Queue Send Declare 
        public static readonly string SEND_QUEUE = "hello1";
        public static readonly string EXCHANGE = "";
        public static readonly string ROUTING_KEY = "hello1";
        public static readonly IBasicProperties PROPERTIES = null;

    }
}
