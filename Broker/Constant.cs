using System;
using System.Collections.Generic;
using System.Text;

namespace Broker
{
    public static class Constant
    {
        public static readonly DestinationInfo sender_001 = new DestinationInfo
        {
            Destination = "Worker-001",
            HostName = "94.131.241.80",
            Port = 5672,
            UserName = "testmf2",
            Password = "As123456"
        };

       
    }
}
