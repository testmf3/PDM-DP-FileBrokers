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
                            destinationInfo.userName = Constant.WORKER_001.userName;
                            destinationInfo.password = Constant.WORKER_001.password;
                            destinationInfo.destination = Constant.WORKER_001.destination;
                            destinationInfo.port = Constant.WORKER_001.port;
                            destinationInfo.hostName = Constant.WORKER_001.hostName;
                           
                        }; break;

                    default:
                        {
                            destinationInfo.userName = Constant.WORKER_001.userName;
                            destinationInfo.password = Constant.WORKER_001.password;
                            destinationInfo.destination = Constant.WORKER_001.destination;
                            destinationInfo.port = Constant.WORKER_001.port;
                            destinationInfo.hostName = Constant.WORKER_001.hostName;

                        }; break;
                }

                Send send = new Send();
                send.Connect(destinationInfo, message);
            }
        }


        static void Main(string[] args)
        {


           
            Receive receive = new Receive();
            Message message;
            Console.WriteLine("Connect");
            message = receive.Connect();
            Console.WriteLine("Message");
            Console.WriteLine(message);
            //Exchange(message);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
