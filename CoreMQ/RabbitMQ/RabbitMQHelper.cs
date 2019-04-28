using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreMQ.RabbitMQ
{
    public class RabbitMQHelper
    {
        ConnectionFactory connectionFactory;
        IConnection connection;
        IModel channel;
        string exchangeName;

        public RabbitMQHelper(string changeName = "fanout_mq")
        {
            this.exchangeName = changeName;
            //创建连接工厂
            connectionFactory = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                UserName = "admin",
                Password = "admin"
            };
            //创建连接
            connection = connectionFactory.CreateConnection();
            //创建通道
            channel = connection.CreateModel();
            //声明交换机
            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queName"></param>
        /// <param name="msg"></param>
        public void SendMsg<T>(string queName, T msg) where T : class
        {
            //声明一个队列
            channel.QueueDeclare(queName, true, false, false, null);
            //绑定队列，交换机，路由键
            channel.QueueBind(queName, exchangeName, queName);

            var basicProperties = channel.CreateBasicProperties();
            //1：非持久化 2：可持久化
            basicProperties.DeliveryMode = 2;
            var payload = Encoding.UTF8.GetBytes("我发出的消息");
            var address = new PublicationAddress(ExchangeType.Direct, exchangeName, queName);
            channel.BasicPublish(address, basicProperties, payload);
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queName"></param>
        /// <param name="received"></param>
        public void Receive(string queName, Action<string> received)
        {
            //事件基本消费者
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            //接收到消息事件
            consumer.Received += (ch, ea) =>
            {
                string message = Encoding.UTF8.GetString(ea.Body);
                received(message);

                // channel. BasicReject(ea.DeliveryTag, true);
                //确认该消息已被消费
                channel.BasicAck(ea.DeliveryTag, false);
            };
            //即在非自动确认消息的前提下，如果一定数目的消息（通过基于consume或者channel设置Qos的值）未被确认前，不进行消费新的消息。
            // void BasicQos(uint prefetchSize, ushort prefetchCount, bool global);
            /*prefetchSize：0 
              prefetchCount：会告诉RabbitMQ不要同时给一个消费者推送多于N个消息，即一旦有N个消息还没有ack，则该consumer将block掉，直到有消息ack
              global：true\false 是否将上面设置应用于channel，简单点说，就是上面限制是channel级别的还是consumer级别,如果是false就是当前channel
                */
            channel.BasicQos(0, 1, false);
            //启动消费者 设置为手动应答消息
            channel.BasicConsume(queName, false, consumer);

        }
    }
}
