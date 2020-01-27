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
            Console.WriteLine("Connect");
            receive.Connect(ref message);
            Console.WriteLine("message:");
            Console.WriteLine(message);
            Console.WriteLine("end:");
            Console.ReadLine();
            //Exchange(message);

        }
    }
}
