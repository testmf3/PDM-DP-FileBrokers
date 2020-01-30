using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace broker
{
    class Program
    {

   
        private Send sender;
        private Receive receive;
        private Message message;
        

        Program() {
            sender = new Send();
            receive = new Receive();
            message = new Message();
        }
      
        static void Main(string[] args)
        {


            Program program = new Program();


            Console.WriteLine("Receive:");
            program.receive.Connect(program.message);   
           
            
            //program.sender.ExchangeLoop(program.message);
            Console.WriteLine("end:");
            Console.ReadLine();
           
        }
    }
}
