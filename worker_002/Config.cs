using RabbitMQ.Client.Content;

namespace worker_002
{
    class Config
    {

        public int number { get; set; }


        public void ToConfig(IMapMessageReader messageReader)
        {
            number = int.Parse(messageReader.Body["number"].ToString());
        }

        public int Div(int value)
        {
            if (value == 0)
            {
                value = 1;
            }

            number /= value;
            return number;
        }

        public override string ToString()
        {
            return "Argument:  " + number;
        }
    }
}
