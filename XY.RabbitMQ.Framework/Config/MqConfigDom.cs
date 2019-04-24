using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.RabbitMQ.Framework
{
    public class MqConfigDom
    {
        public string MqHost { get; set; }
        public string MqUserName { get; set; }
        public string MqPassword { get; set; }
        public string MqVirtualHost { get; set; }
    }
}
