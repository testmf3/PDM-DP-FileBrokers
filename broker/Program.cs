using System;
using System.Collections.Generic;
using System.Threading;

namespace broker
{
    class Program
    {
        static List<String> names = new List<string> { "sender_001"};

        static void Exchange(Message message)
        {
            DestinationInfo destination = new DestinationInfo();
           
            if (message != null)
            {
                //TODO switch
                switch (message.applicationName)
                {
                    case "sender_001":
                        {
                            destination.userName = "testmf2";
                            destination.password = "As123456";
                            destination.destination = "Worker-001";
                            destination.port = 5672;
                            destination.hostName = "94.131.241.80";
                        }; break;

                    default:
                        {
                            Console.WriteLine("Not catch");
                        }; break;
                }

             
                Send sender = new Send();
             
                sender.Connect(destination, message);
            }
        }

        static void Main(string[] args)
        {

          
            Receive receive = new Receive();

            Message message = new Message();
            Thread.Sleep(5000);
            Console.WriteLine("Receive:");
            receive.Connect(ref message);

            Console.WriteLine("Message:");
            Console.WriteLine(message);
            Console.ReadLine();

            //1:n
            names.ForEach(name =>
            {
                Console.WriteLine("Send to " + name);
                message.applicationName = name;
                Exchange(message);

            });

      
            
            Console.WriteLine("end:");
            Console.ReadLine();
           
        }
    }
}
