using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gamedalf.Startup))]
namespace Gamedalf
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
