using System;

namespace RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {

            MQHelperFactory.Default().Receive("DirectQueue", item =>
            {
                MQHelperFactory.Default().SendMsg<string>("DirectQueue", item);
            });
            Console.ReadKey();
        }
    }
}
