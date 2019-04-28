

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace NetCore.Common
{
    /// <summary>
    /// 读取appsettings.json的值
    /// </summary>
    public class AppConfigurtaionServices
    {
        //public static IConfiguration Configuration { get; set; }
        //static AppConfigurtaionServices()
        //{
        //    //ReloadOnChange = true 当appsettings.json被修改时重新加载            
        //    Configuration = new ConfigurationBuilder()
        //        .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
        //        .Build();

          
        //}

        public T GetAppSettings<T>(string key) where T : class, new()
        {
            var baseDir = AppContext.BaseDirectory;
            //var indexSrc = baseDir.IndexOf("src");
            //var subToSrc = baseDir.Substring(0, indexSrc);
            //var currentClassDir = subToSrc + "src" + Path.DirectorySeparatorChar + "StutdyEFCore.Data";

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(baseDir)
                .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
                .Build();
            var appconfig = new ServiceCollection().AddOptions().Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }
    }
    
}
