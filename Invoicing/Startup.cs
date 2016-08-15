using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Invoicing.Startup))]
namespace Invoicing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
