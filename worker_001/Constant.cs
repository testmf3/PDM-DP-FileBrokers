using System;
using System.Collections.Generic;

namespace worker_001
{
    class Constant
    {

        //Declare factory
        public static readonly string HOST_NAME = "94.131.241.80";
        public static readonly Int32 PORT = 5672;
        public static readonly string USER_NAME = "testmf2";
        public static readonly string PASSWORD = @"PHtsPT2e@SkdV?@";


        //Declare queue
        public static readonly string QUEUE = "hello1";
        public static readonly bool DURABLE = false;
        public static readonly bool EXCLUSIVE = false;
        public static readonly bool AUTO_DELETE = false;
        public static readonly IDictionary<string,object> ARGUMENTS = null;


        public static readonly bool AUTO_ACK = false;


    }
}
