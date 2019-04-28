using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public interface IRabbitMQHelper
    {
        //ConnectionFactory connectionFactory;
        /// <summary>
        /// 创建链接
        /// </summary>
        IConnection connection { get; set; }
        /// <summary>
        /// 获取通道
        /// </summary>
        IModel channel { get; set; }
       

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        void SendMsg<T>(T msg) where T : class;
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queName"></param>
        /// <param name="msg"></param>
        void SendMsg<T>(string queName, T msg) where T : class;
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queName"></param>
        /// <param name="exchangName"></param>
        /// <param name="routType"></param>
        /// <param name="msg"></param>
        void SendMsg<T>(string queName, string exchangName, string routType, T msg) where T : class;

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configKey"></param>
        /// <param name="msg"></param>
        void SendMsg<T>(RabbitMq rabbitMq, T msg);
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="queName"></param>
        /// <param name="received"></param>
        void Receive(string queName, Action<string> received);
    }
}
