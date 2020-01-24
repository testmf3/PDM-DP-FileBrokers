using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace Sender_001
{
    class Constant
    {
        public static string APPLICATIONNAME = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;//"Sender-001"

        //numbers for Sum
        public static int A = 1;
        public static int B = 1;


        //ConnectionFactory
        public static string HOSTNAME = "94.131.241.80";
        public static Int32 PORT = 5672;
        public static string USERNAME = "testmf2";
        public static string PASSWORD = "As123456";


        //QueueDeclare
        public static string QUEUE = "hello";
        public static bool DURABLE = false;
        public static bool EXLUSIVE = false;
        public static bool AUTODELETE = false;
        public static IDictionary<string, object> ARGUMENTS = null;


        //BasicPublish
        public static string EXCHANGE = "";
        public static string ROUTINGKEY = "hello";
        public static IBasicProperties BASICPROPERTIES = null;

    }
}
