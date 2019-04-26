using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XY.RabbitMQ.Framework;
using XY.RabbitMQ.Message;

namespace XY.RabbitMQ.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = "";
            do
            {
                content = Console.ReadLine();
                RabbitMQTest(content);
            } while (content != "q");

        }

        //测试RabbitMQ
        static void RabbitMQTest(string content)
        {
            //持久化的Exchange、持久化的消息、持久化的队列
            //RabbitMQClientContext context = new RabbitMQClientContext() { SendQueueName = "SendQueueName11", SendExchange = "TEST" ,RoutType = MQRouteType.DirectExchange};
            //持久化的Exchange、持久化的消息、非持久化的队列
            RabbitMQClientContext context2 = new RabbitMQClientContext()
            {
                SendQueueName = "DirectQueue",
                SendExchange = "DirectQueue",
                RoutType = MqRouteType.DirectExchange,
                RoutKey = "DirectQueue",
                MqConfigDom =new MqConfigDom()
                {
                    MqHost = "127.0.0.1",
                    MqUserName = "admin",
                    MqPassword = "admin",
                    MqVirtualHost = "/"
                }
            };

            IEventMessage<MessageEntity> message = new EventMessage<MessageEntity>()
            {
                IsOperationOk = false,
                MessageEntity = new MessageEntity() { MessageContent =JsonConvert.SerializeObject(content)  },
                deliveryMode = 2
            };

            try
            {
                RabbitMQSender<MessageEntity> sender = new RabbitMQSender<MessageEntity>(context2, message);
                //for (int i = 0; i < 10000; i++)
                //{
                Console.WriteLine(string.Format("发送信息:{0}", message.MessageEntity.MessageContent));
                sender.TriggerEventMessage();
                //}



            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("发送信息失败:{0}", e.Message));
            }
        }
    }
}
