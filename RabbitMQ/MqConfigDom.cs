using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    /// <summary>
    /// Rabbitmq服务器配置
    /// </summary>
    public class MqConfigDom
    {
        /// <summary>
        ///  MQ的服务器地址
        /// </summary>
        public string MqHost { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string MqUserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string MqPassword { get; set; }
        /// <summary>
        /// 虚拟主机
        /// </summary>
        public string MqVirtualHost { get; set; }
    }
}
