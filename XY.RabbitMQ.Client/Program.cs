using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.RabbitMQ.Framework;
using XY.RabbitMQ.Message;

namespace XY.RabbitMQ.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100000; i++)
            {
                RabbitMQTest();
            }

            Console.ReadKey();
        }

        //测试RabbitMQ
        static void RabbitMQTest()
        {
            //持久化的Exchange、持久化的消息、持久化的队列
            RabbitMQClientContext context = new RabbitMQClientContext() { SendQueueName = "SendQueueName", SendExchange = "amq.fanout" };
            //持久化的Exchange、持久化的消息、非持久化的队列
            RabbitMQClientContext context2 = new RabbitMQClientContext() { SendQueueName = "SendQueueName", SendExchange = "amq.fanout" };

            IEventMessage<MessageEntity> message = new EventMessage<MessageEntity>()
            {
                IsOperationOk = false,
                MessageEntity = new MessageEntity() { MessageID = 1, MessageContent = "测试消息队列3" },
                deliveryMode = 2
            };

            try
            {
                RabbitMQSender<MessageEntity> sender = new RabbitMQSender<MessageEntity>(context2, message);
                sender.TriggerEventMessage();

                Console.WriteLine(string.Format("发送信息:{0}", message.MessageEntity.MessageContent));
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("发送信息失败:{0}", e.Message));
            }
        }
    }
}
