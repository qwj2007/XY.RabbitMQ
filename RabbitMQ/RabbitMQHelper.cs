using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RabbitMQ
{
    public class RabbitMQHelper : IRabbitMQHelper
    {
        /// <summary>
        /// 建立连接
        /// </summary>
        public IConnection connection { get; set; }
        /// <summary>
        /// 通道
        /// </summary>
        public IModel channel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryConfigKey">配置键</param>
        public RabbitMQHelper(string queryConfigKey = "RabbitMQConfig")
        {
            //创建连接
            connection = RabbitMQClientFactory.CreateConnection(queryConfigKey);
            //创建通道
            channel = connection.CreateModel();
            channel.QueueDeclare(RabbitMQClientFactory.rabbitmq.QueueName, true, false, false, null);
            //声明交换机
            channel.ExchangeDeclare(RabbitMQClientFactory.rabbitmq.ExchangeName, RabbitMQClientFactory.rabbitmq.RoutType, true, false, null);
            //交换机和消息队列绑定
            channel.QueueBind(RabbitMQClientFactory.rabbitmq.QueueName, RabbitMQClientFactory.rabbitmq.ExchangeName, RabbitMQClientFactory.rabbitmq.RoutKey);
        }

        public RabbitMQHelper()
        {
        }
        

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queName"></param>
        /// <param name="msg"></param>
        public void SendMsg<T>(T msg) where T : class
        {
            var basicProperties = channel.CreateBasicProperties();
            //1：非持久化 2：可持久化
            basicProperties.DeliveryMode = 2;
            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
            var address = new PublicationAddress(RabbitMQClientFactory.rabbitmq.RoutType, RabbitMQClientFactory.rabbitmq.ExchangeName, RabbitMQClientFactory.rabbitmq.RoutKey);
            channel.BasicPublish(address, basicProperties, payload);
        }

        public void SendMsg<T>(string queName, T msg) where T : class
        {
            var basicProperties = channel.CreateBasicProperties();
            //1：非持久化 2：可持久化
            basicProperties.DeliveryMode = 2;
            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
            var address = new PublicationAddress(RabbitMQClientFactory.rabbitmq.RoutType, RabbitMQClientFactory.rabbitmq.ExchangeName, queName);
            channel.BasicPublish(address, basicProperties, payload);
        }

        public void SendMsg<T>(string queName, string exchangName, string routType, T msg) where T : class
        {
            var basicProperties = channel.CreateBasicProperties();
            //1：非持久化 2：可持久化
            basicProperties.DeliveryMode = 2;
            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
            var address = new PublicationAddress(routType, exchangName, queName);
            channel.BasicPublish(address, basicProperties, payload);
        }

        


        public void SendMsg<T>(RabbitMq rabbitMq, T msg)
        {
            //创建连接
            IConnection connection = RabbitMQClientFactory.CreateConnection(rabbitMq);
            //创建通道
            IModel channel = connection.CreateModel();
            //申明队列
            channel.QueueDeclare(rabbitMq.QueueName, true, false, false, null);
            //声明交换机
            channel.ExchangeDeclare(rabbitMq.ExchangeName, rabbitMq.RoutType, true, false, null);
            //交换机和消息队列绑定
            channel.QueueBind(rabbitMq.QueueName, rabbitMq.ExchangeName, rabbitMq.RoutKey, null);
            var basicProperties = channel.CreateBasicProperties();
            //1：非持久化 2：可持久化
            basicProperties.DeliveryMode = 2;
            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
            var address = new PublicationAddress(rabbitMq.RoutType, rabbitMq.ExchangeName, rabbitMq.RoutKey);
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
