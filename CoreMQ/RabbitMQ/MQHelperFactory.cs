using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMQ.RabbitMQ
{
    public class MQHelperFactory
    {
        public static RabbitMQHelper Default() =>
            new RabbitMQHelper("exchange_fanout");
    }
}
