using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConsulSpike.AspNet.Startup))]
namespace ConsulSpike.AspNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
