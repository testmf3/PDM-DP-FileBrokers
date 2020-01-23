using System;

namespace Broker
{
    class Program
    {

        static void Exchange(Message message)
        {

            string Destination="", HostName = "", UserName = "", Password = "";
            Int32 Port=0;
            //TODO switch
            switch (message.applicationName)
            {
                case "Sender-001":
                    {
                        Destination = "Worker-001";
                        HostName = "94.131.241.80";
                        Port = 5672;
                        UserName = "testmf2";
                        Password = "As123456";
                    }; break;
                default:; break;
            }
            Send send = new Send();
            send.Connect(Destination, HostName, Port, UserName, Password, message);


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
