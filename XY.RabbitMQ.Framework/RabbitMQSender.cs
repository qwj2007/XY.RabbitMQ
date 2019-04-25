using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.RabbitMQ.Framework
{
    public class RabbitMQSender<T>
    {
        public RabbitMQClientContext Context { get; private set; }

        public IEventMessage<T> Message { get; private set; }

        private RabbitMQSender() { }

        public RabbitMQSender(RabbitMQClientContext context, IEventMessage<T> message)
        {
            this.Context = context;
            this.Message = message;
        }

        //客户端发送消息的时候要标记上消息的持久化状态
        //可以在创建队列的时候设置此队列是持久化的，但是队列中的消息要在我们发送某个消息的时候打上需要持久化的状态标记。
        public void TriggerEventMessage()
        {
            //if (Context.SendConnection == null)
            //{
            Context.SendConnection = RabbitMQClientFactory.CreateConnectionForSend();//获取连接
            //}
            using (Context.SendConnection)
            {
                //获取发送通道
                //if (Context.SendChannel == null)
                //{
                Context.SendChannel = RabbitMQClientFactory.CreateModel(Context.SendConnection);
                //声明消息队列
                Context.SendChannel.QueueDeclare(Context.SendQueueName, true, false, false, null);
                //交换机
                Context.SendChannel.ExchangeDeclare(Context.SendExchange, Context.RoutType, true, false, null);
                //绑定
                Context.SendChannel.QueueBind(Context.SendQueueName, Context.SendExchange, Context.RoutKey);
                //}

                using (Context.SendChannel)
                {
                    //序列化消息器
                    var messageSerializer = MessageSerializerFactory.CreateMessageSerializerInstance();
                    //消息持久化
                    var properties = Context.SendChannel.CreateBasicProperties();
                    properties.DeliveryMode = Message.deliveryMode;
                    //推送消息
                    byte[] sMessage = messageSerializer.SerializerBytes(Message);
                    //for (int i = 0; i < 10000; i++)
                    //{
                        Console.WriteLine(string.Format("发送信息:{0}", Newtonsoft.Json.JsonConvert.SerializeObject(Message.MessageEntity)));
                        Context.SendChannel.BasicPublish(Context.SendExchange, Context.SendQueueName, properties,
                            sMessage);
                    //}
                }
            }
        }
    }
}
