using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMQ.Models;
using XY.RabbitMQ.Framework;
using XY.RabbitMQ.Message;

namespace CoreMQ.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult MQTest()
        {
            return View();
        }

        public void RabbitMQTest(string content)
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
                MqConfigDom = new MqConfigDom()
                {
                    MqHost = "127.0.0.1",
                    MqUserName = "admin",
                    MqPassword = "admin",
                    MqVirtualHost = "/"
                }
            };

            IEventMessage<MessageEntity> message = new EventMessage<MessageEntity>()
            {
                IsOperationOk = false,//消息队列是否处理
                MessageEntity = new MessageEntity() {MessageContent = content },
                deliveryMode = 2
            };

            try
            {
                RabbitMQSender<MessageEntity> sender = new RabbitMQSender<MessageEntity>(context2, message);
                Console.WriteLine(string.Format("发送信息:{0}", message.MessageEntity.MessageContent));
                sender.TriggerEventMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("发送信息失败:{0}", ex.Message));
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
