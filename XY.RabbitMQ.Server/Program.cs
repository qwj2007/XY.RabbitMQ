using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.RabbitMQ.Framework;
using XY.RabbitMQ.Message;

namespace XY.RabbitMQ.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================监听程序开始初始化==================");

            RabbitMQTest();

            Console.WriteLine("==================监听程序已启动，监听中==================");

            Console.ReadKey();
        }

        //测试RabbitMQ的消费者
        static void RabbitMQTest()
        {
            LogLocation.Log = new LogInfo();
            RabbitMQClientContext context = new RabbitMQClientContext()
            {
                ListenQueueName = "SendQueueName"
            };

            RabbitMQConsumer<MessageEntity> consumer = new RabbitMQConsumer<MessageEntity>(context, new EventMessage<MessageEntity>())
            {
                ActionMessage = b =>
                {
                    Console.WriteLine(b.MessageEntity.MessageContent);
                    b.IsOperationOk = true;
                }
            };

            consumer.OnListening();
            //多个消费者，共用一个连接，使用不同的Channel
            //consumer.OnListening();
        }
    }
}
