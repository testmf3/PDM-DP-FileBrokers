using System;

namespace Broker
{
    class Program
    {
      
        static void Exchange(Message message)
        {
            DestinationInfo destinationInfo = new DestinationInfo();
           
            //TODO switch
            switch (message.applicationName)
            {
                case "Sender-001":
                    {
                        DestinationInfo.Clone(Constant.sender_001, ref destinationInfo);
                    }; break;

                default:; break;
            }

           
            Send send = new Send();
            send.Connect(destinationInfo, message);

            Console.WriteLine(destinationInfo);
            Console.WriteLine(message);
        }


        static void Main(string[] args)
        {
           // Program program = new Program();

            Receive receive = new Receive();
            Exchange(receive.connect());
        }
    }
}
