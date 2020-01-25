using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_001
{
    class Constant
    {
        public static string applicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;//"Sender-001"

        //numbers for Sum
        public static int NUMBER = 5;


        //ConnectionFactory
        public static string HOSTNAME = "94.131.241.80";
        public static Int32 PORT = 5672;
        public static string USERNAME = "testmf3";
        public static string PASSWORD = "As123456";


        //QueueDeclare
        public static string QUEUE = "hello2";
        public static bool DURABLE = false;
        public static bool EXCLUSIVE = false;
        public static bool AUTODELETE = false;
        public static IDictionary<string, object> ARGUMENTS = null;


        //BasicConsume 
        public static bool AUTO_ACK = false;
    }
}
