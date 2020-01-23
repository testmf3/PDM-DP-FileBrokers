using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sender_001
{
    class Constant
    {
        public static string applicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;//"Sender-001"

        //numbers for Sum
        public static int a = 1;
        public static int b = 1;


        //ConnectionFactory
        public static string hostName = "94.131.241.80";
        public static Int32 port = 5672;
        public static string userName = "testmf2";
        public static string password = "As123456";


        //QueueDeclare
        public static string queue = "hello";
        public static bool durable = false;
        public static bool exclusive = false;
        public static bool autoDelete = false;
        public static System.Collections.Generic.IDictionary<String, Object> arguments = null;


        //BasicPublish
        public static string exchange = "";
        public static string routingKey = "hello";
        public static IBasicProperties basicProperties = null;

    }
}
