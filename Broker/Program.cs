using System;

namespace Broker
{
    class Program
    {
      
        static void Exchange(Message message)
        {
            DestinationInfo destinationInfo = new DestinationInfo();

            message = new Message("Sender-001", DateTime.Now, 3);

            if (message!=null)
            {

                //TODO switch
                switch (message?.applicationName)
                {
                    case "Sender-001":
                        {
                            DestinationInfo.Clone(Constant.sender_001, ref destinationInfo);
                        }; break;

                    default:; break;
                }


                Send send = new Send();
                send.Connect(destinationInfo, message);

            }


            Console.WriteLine(destinationInfo);
            Console.WriteLine(message);
        }


        static void Main(string[] args)
        {
        
            Receive receive = new Receive();
            Exchange(receive.Connect());
        }
    }
}
