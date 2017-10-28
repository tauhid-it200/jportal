using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hiring.Startup))]
namespace Hiring
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
