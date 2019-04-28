using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    /// <summary>
    /// rabbitMQ类
    /// </summary>
    public class RabbitMq
    {
        
        /// <summary>
        /// 消息队列名称
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        ///交换机名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 路由类型
        /// </summary>
        public string RoutType { get; set; }
        /// <summary>
        /// 路由键
        /// </summary>
        public string RoutKey { get; set; }
        /// <summary>
        /// MQ主机配置
        /// </summary>
        public MqConfigDom MqConfig { get; set; }
    }
}
