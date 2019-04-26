using XY.RabbitMQ.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.RabbitMQ.Framework
{
    [Serializable]
    public class EventMessage<T> : IEventMessage<T>
    {
        //是否持久化，1否，2 是
        public byte deliveryMode{ get; set;}
        //发送的实体
        public T MessageEntity { get; set; }
        //消息队列是否处理
        public bool IsOperationOk { get; set; }

        public IEventMessage<T> BuildEventMessageResult(byte[] body)
        {
            return MessageSerializerFactory.CreateMessageSerializerInstance().BytesDeseriallizer<IEventMessage<T>>(body);
        }
    }
}
