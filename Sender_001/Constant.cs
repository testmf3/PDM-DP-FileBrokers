using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace Sender_001
{
    class Constant
    {
        public static string applicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;//"Sender-001"

        //numbers for Sum
        public static int a = 1;
        public static int b = 1;


        //ConnectionFactory
        public static string HOST_NAME = "94.131.241.80";
        public static Int32 PORT = 5672;
        public static string USERNAME = "testmf2";
        public static string PASSWORD = "As123456";


        //QueueDeclare
        public static string QUEUE = "hello1";
        public static bool DURABLE = false;
        public static bool EXCLUSIVE = false;
        public static bool AUTO_DELETE = false;
        public static IDictionary<string, object> ARGUMENTS = null;


        //BasicPublish
        public static string EXCHANGE = "";
        public static string ROUTING_KEY = "hello1";
        public static IBasicProperties BASIC_PROPERTIES = null;

    }
}
