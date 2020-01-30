using System;
using System.Collections.Generic;

namespace worker_003
{
    class Constant
    {

        //Declare factory
        public static readonly string HOST_NAME = "94.131.241.80";
        public static readonly Int32 PORT = 5672;
        public static readonly string USER_NAME = "testmf3";
        public static readonly string PASSWORD = "As123456";


        //Declare queue
        public static readonly string QUEUE = "hello1";
        public static readonly bool DURABLE = false;
        public static readonly bool EXCLUSIVE = false;
        public static readonly bool AUTO_DELETE = false;
        public static readonly IDictionary<string, object> ARGUMENTS = null;


        public static readonly bool AUTO_ACK = false;


    }
}
