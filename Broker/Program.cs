using System;
using System.Threading;

namespace Broker
{
    class Program
    {
      
        static void Exchange(Message message)
        {
            DestinationInfo destinationInfo = new DestinationInfo();

            if (message != null)
            {
                //TODO switch
                switch (message.applicationName)
                {
                    case "Sender-001":
                        {
                            DestinationInfo.Clone(Constant.WORKER_001, ref destinationInfo);
                        }; break;

                    default:; break;
                }

                Send send = new Send();
                send.Connect(destinationInfo, message);
            }
        }


        static void Main(string[] args)
        {
        
            Receive receive = new Receive();
            Message message = receive.Connect();  
            Exchange(message);
        }
    }
}
