using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ConsulSpike.AspNetCore.Config;
using Consul;
using System.Text;

namespace ConsulSpike.AspNetCore
{
    public class Startup
    {
        IConfigurationRoot _configuration;
        public IConfigurationRoot Configuration { get { return _configuration; } }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            _configuration = builder.SetBasePath($"{env.ContentRootPath}\\Config")
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();
            services.Configure<ConfigData>(Configuration);
            services.Configure<ConfigData>(async config => {                
                using (var client = new ConsulClient(clientConfig => clientConfig.Address = new Uri("http://192.168.99.100:8500/")))
                {
                    var getPair = await client.KV.Get("serviceUrl2");
                    if (getPair.Response != null)
                    {
                        var serviceUrl = Encoding.UTF8.GetString(getPair.Response.Value, 0, getPair.Response.Value.Length);
                        config.ServiceUrl = serviceUrl;
                    }
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
