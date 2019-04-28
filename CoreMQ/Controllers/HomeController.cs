using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoreMQ.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RabbitMQ;

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
            #region MQ第一种写法

            RabbitMQHelper mq = new RabbitMQHelper("RabbitMQConfig");
            mq.SendMsg("卧槽卧槽卧槽");

            #endregion


            #region MQ第二种写法

            //RabbitMQHelper mqs = new RabbitMQHelper();
            //RabbitMq mq = new RabbitMq();
            //mq.ExchangeName = "amq.direct";
            //mq.QueueName = "QueueTest";
            //mq.RoutKey = "QueueTest";
            //mq.RoutType = "direct";
            //mq.MqConfig = new MqConfigDom()
            //{
            //    MqHost = "127.0.0.1",
            //    MqPassword = "admin",
            //    MqUserName = "admin",
            //    MqVirtualHost = "/"
            //};
            //mqs.SendMsg<string>(mq, "测试mQQQ");

            #endregion


            #region MQ第三种方法

            //持久化的Exchange、持久化的消息、持久化的队列
            //RabbitMQClientContext context = new RabbitMQClientContext() { SendQueueName = "SendQueueName11", SendExchange = "TEST" ,RoutType = MQRouteType.DirectExchange};
            //持久化的Exchange、持久化的消息、非持久化的队列
            //RabbitMQClientContext context2 = new RabbitMQClientContext()
            //{
            //    SendQueueName = "HELLOQUEUES",
            //    SendExchange = "KSHOP",
            //    RoutType = MqRouteType.DirectExchange,
            //    RoutKey = "BING_KELLO_QUEUE_KEY",
            //    MqConfigDom = new MqConfigDom()
            //    {
            //        MqHost = "127.0.0.1",
            //        MqUserName = "admin",
            //        MqPassword = "admin",
            //        MqVirtualHost = "/"
            //    }
            //};

            //IEventMessage<MessageEntity> message = new EventMessage<MessageEntity>()
            //{
            //    IsOperationOk = false,//消息队列是否处理
            //    MessageEntity = new MessageEntity() {MessageContent = content },
            //    deliveryMode = 2
            //};

            //try
            //{
            //    RabbitMQSender<MessageEntity> sender = new RabbitMQSender<MessageEntity>(context2, message);
            //    Console.WriteLine(string.Format("发送信息:{0}", message.MessageEntity.MessageContent));
            //    sender.TriggerEventMessage();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(string.Format("发送信息失败:{0}", ex.Message));
            //}

            #endregion


            #region MQ接收
            //委托第一种
            mq.Receive("DirectQueue", item =>
            {
                string msg = JsonConvert.DeserializeObject<string>(item);
                
                #region 业务逻辑操作

                #endregion

            });

            //委托第二种
            mq.Receive("DirectQueue", getMessage);

            #endregion

        }

        private void getMessage(string item)
        {
            string msg = JsonConvert.DeserializeObject<string>(item);
            #region 业务逻辑操作


            #endregion
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
