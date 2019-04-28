using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class RabbitMQClientFactory
    {
        public static MqConfigDom mqConfigCom { get; set; }
        public static RabbitMq rabbitmq { get; set; }

        public static IConnection CreateConnection(string configKey)
        {
            if (rabbitmq == null)
            {
                rabbitmq = MqConfigFactory.CreateConfigDomInstance(configKey); //获取MQ的配置
                mqConfigCom = rabbitmq.MqConfig;
            }

            const ushort heartbeat = 60;
            var factory = new ConnectionFactory()
            {
                HostName = mqConfigCom.MqHost,
                UserName = mqConfigCom.MqUserName,
                Password = mqConfigCom.MqPassword,
                //心跳超时时间，如果是单节点，不设置这个值是没有问题的
                //但如果连接的是类似HAProxy虚拟节点的时候就会出现TCP被断开的可能性
                RequestedHeartbeat = heartbeat,
                AutomaticRecoveryEnabled = true, //自动重连
                Port = AmqpTcpEndpoint.UseDefaultPort,
                VirtualHost = mqConfigCom.MqVirtualHost
            };
            return factory.CreateConnection();//创建连接对你
        }


        public static IConnection CreateConnection(RabbitMq _rabbitMq)
        {
            rabbitmq = _rabbitMq;
            mqConfigCom = rabbitmq.MqConfig;
            const ushort heartbeat = 60;
            var factory = new ConnectionFactory()
            {
                HostName = mqConfigCom.MqHost,
                UserName = mqConfigCom.MqUserName,
                Password = mqConfigCom.MqPassword,
                //心跳超时时间，如果是单节点，不设置这个值是没有问题的
                //但如果连接的是类似HAProxy虚拟节点的时候就会出现TCP被断开的可能性
                RequestedHeartbeat = heartbeat,
                AutomaticRecoveryEnabled = true, //自动重连
                Port = AmqpTcpEndpoint.UseDefaultPort,
                VirtualHost = mqConfigCom.MqVirtualHost
            };
            return factory.CreateConnection();//创建连接对你
        }
    }
}
