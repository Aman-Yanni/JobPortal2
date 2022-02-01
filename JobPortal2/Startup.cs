using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobPortal2.Startup))]
namespace JobPortal2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
