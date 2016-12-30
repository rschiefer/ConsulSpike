using Consul;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsulSpike.AspNet.Config
{
    public class ConfigDataProvider : IConfigDataProvider
    {
        public ConfigData GetConfigData()
        {
            var config = new ConfigData();
            if (ConfigurationManager.AppSettings.AllKeys.Contains("serviceUrl"))
            {
                config.ServiceUrl = ConfigurationManager.AppSettings.GetValues("serviceUrl").FirstOrDefault();
            }

            using (var client = new ConsulClient(clientConfig => {
                clientConfig.Address = new Uri("http://192.168.99.100:8500/");
            }))
            {
                var getPair = client.KV.Get("serviceUrl").Result                    ;
                if (getPair.Response != null)
                {
                    var serviceUrl = Encoding.UTF8.GetString(getPair.Response.Value, 0, getPair.Response.Value.Length);
                    config.ServiceUrl = serviceUrl;
                }
            }

            return config;
        }
    }
}
