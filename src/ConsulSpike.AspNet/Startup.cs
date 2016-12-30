using Autofac;
using Autofac.Integration.Mvc;
using ConsulSpike.AspNet.Config;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(ConsulSpike.AspNet.Startup))]
namespace ConsulSpike.AspNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConfigDataProvider>().As<IConfigDataProvider>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
        }
    }
}
