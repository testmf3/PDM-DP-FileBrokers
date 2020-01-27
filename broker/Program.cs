using System;
using System.Threading;

namespace broker
{
    class Program
    {


        static void Main(string[] args)
        {

            Receive receive = new Receive();
            

            Thread.Sleep(5000);
            Console.WriteLine("Connect");
            receive.Connect();
            //Exchange(message);
           
        }
    }
}
