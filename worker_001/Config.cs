using RabbitMQ.Client.Content;

namespace worker_001
{
    class Config { 
 
        public int number { get; set; }


        public void ToConfig(IMapMessageReader messageReader)
        {
            number = int.Parse(messageReader.Body["number"].ToString());
        }

        public int Sum(int value)
        {
            number += value;
            return number;
        }

        public override string ToString()
        {
            return  "Argument:  " + number;
        }
    }
}
