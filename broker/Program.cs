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

   
        private Receive receive;
        

        Program() {
            receive = new Receive();
        }
      
        static void Main(string[] args)
        {


            Program program = new Program();


            Console.WriteLine("Receive:");
            program.receive.Connect();   

            Console.WriteLine("end:");
            Console.ReadLine();
           
        }
    }
}
