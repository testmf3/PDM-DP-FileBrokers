using System;
using System.Collections.Generic;
using System.Threading;

namespace broker
{
    class Program
    {

        static List<String> names = new List<string> { "sender_001" };
        
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
            
        

            Thread.Sleep(5000);
            Console.WriteLine("Receive:");
            program.receive.Connect(ref program.message);

            Console.WriteLine("Message:");
            Console.WriteLine(program.message);
            Console.ReadLine();


            //1:n
           
            names.ForEach(name =>
            {
                Console.WriteLine("Send to " + name);
                program.message.applicationName = name;
                program.sender.Exchange(program.message);

            });



            Console.WriteLine("end:");
            Console.ReadLine();
           
        }
    }
}
