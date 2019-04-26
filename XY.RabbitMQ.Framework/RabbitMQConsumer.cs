using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RabbitMQClient = RabbitMQ.Client;

namespace XY.RabbitMQ.Framework
{
    public class RabbitMQConsumer<T>
    {
        public RabbitMQClientContext Context { get; private set; }

        public IEventMessage<T> Message { get; private set; }

        public Action<IEventMessage<T>> ActionMessage = null;

        private RabbitMQConsumer() { }

        public RabbitMQConsumer(RabbitMQClientContext context, IEventMessage<T> message)
        {
            this.Context = context;
            this.Message = message;
        }

        public void OnListening()
        {
            Task.Run(() => ListenInit());
        }

        private void ListenInit()
        {
            try
            {
                //获取连接
                lock(this)
                {
                  
                    //Context.ListenConnection = RabbitMQClientFactory.CreateConnectionForSumer();
                    Context.ListenConnection = RabbitMQClientFactory.CreateConnection(Context.MqConfigDom);
                    //获取通道
                    Context.ListenChannel = RabbitMQClientFactory.CreateModel(Context.ListenConnection);
                    //获取消息队列中有多少数据。
                    int count = Convert.ToInt32(Context.ListenChannel.MessageCount(Context.ListenQueueName));
                    //创建事件驱动的消费者模型
                    //QueueingBasicConsumer这个是队列的消费者
                    var consumer = new EventingBasicConsumer(Context.ListenChannel);
                    consumer.Received += Consumer_Received;

                    //即在非自动确认消息的前提下，如果一定数目的消息（通过基于consume或者channel设置Qos的值）未被确认前，不进行消费新的消息。
                    // void BasicQos(uint prefetchSize, ushort prefetchCount, bool global);
                    /*prefetchSize：0 
                      prefetchCount：会告诉RabbitMQ不要同时给一个消费者推送多于N个消息，即一旦有N个消息还没有ack，则该consumer将block掉，直到有消息ack
                      global：true\false 是否将上面设置应用于channel，简单点说，就是上面限制是channel级别的还是consumer级别,如果是false就是当前channel
                        */
                    Context.ListenChannel.BasicQos(0, 1, false);
                    //消息确认, 首先将autoAck自动确认关闭，等我们的任务执行完成之后，手动的去确认
                    //Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
                    Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer.ConsumerTag!=null? consumer.ConsumerTag:"tag", false, false, null,
                        consumer);
                }
            }
            catch (Exception ex)
            {
                if (LogLocation.Log != null)
                {
                    LogLocation.Log.WriteInfo("RabbitMQClient", "ListenInit()方法报错：" + ex.Message);
                }
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                
                

                var result = Message.BuildEventMessageResult(e.Body);
                if (ActionMessage != null)
                    ActionMessage(result);//触发外部监听事件，处理此消息

                if (!result.IsOperationOk)
                {
                    //未能消费此消息，重新放入队列头
                    Context.ListenChannel.BasicReject(e.DeliveryTag, true);
                }
                else if (!Context.ListenChannel.IsClosed)
                {
                    //如果通道还未关闭，给RabbitMQ发送一条确认消息(ACK)，
                    //告诉它此消息已成功处理，可以从队列中删除了
                    Context.ListenChannel.BasicAck(e.DeliveryTag, false);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void Consumer_Received(RabbitMQClient.IBasicConsumer sender, BasicDeliverEventArgs args)
        //{
        //    try
        //    {
        //        var result = Message.BuildEventMessageResult(args.Body);

        //        if (ActionMessage != null)
        //            ActionMessage(result);//触发外部监听事件，处理此消息

        //        if (!result.IsOperationOk)
        //        {
        //            //未能消费此消息，重新放入队列头
        //            Context.ListenChannel.BasicReject(args.DeliveryTag, true);
        //        }
        //        else if (!Context.ListenChannel.IsClosed)
        //        {
        //            //如果通道还未关闭，给RabbitMQ发送一条确认消息(ACK)，
        //            //告诉它此消息已成功处理，可以从队列中删除了
        //            Context.ListenChannel.BasicAck(args.DeliveryTag, false);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
