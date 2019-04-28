using System;
using System.Collections.Generic;
using System.Text;
using NetCore.Common;

namespace RabbitMQ
{
    public class MqConfigFactory
    {
        public static RabbitMq CreateConfigDomInstance(string key)
        {
            var appconfig = new AppConfigurtaionServices();
            var mqConfig = appconfig.GetAppSettings<RabbitMq>(key);
            return mqConfig;
        }
    }
}
