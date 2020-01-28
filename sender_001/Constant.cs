using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace sender_001
{
    class Constant
    {

        //Create Factory
        public static readonly string HOST_NAME = "94.131.241.80";
        public static readonly Int32 PORT = 5672;
        public static readonly string USER_NAME = "testmf3";
        public static readonly string PASSWORD = "As123456";



        public static readonly string APPLICATON_NAME = "sender_001";

        //Declare Channel
        public static readonly string QUEUE = "hello";
        public static readonly bool DURABLE = false;
        public static readonly bool EXCLUSIVE = false;
        public static readonly bool AUTO_DELETE = false;
        public static readonly IDictionary<string, object> ARGUMENTS = null;

        //Basic Publish
        public static readonly string EXCHANGE = "";
        public static readonly string ROUTING_KEY = "hello";
        public static readonly IBasicProperties PROPERTIES = null;

    

    }
}
