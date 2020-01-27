using System;
using System.Threading;

namespace broker
{
    class Program
    {


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

            Console.WriteLine("Send:");
            Send sender = new Send();
            sender.Connect(message);

            Console.WriteLine("end:");
            Console.ReadLine();
           
        }
    }
}
