using Microsoft.Owin;
using Owin;
using Startup = BMSA.App.Startup;

[assembly: OwinStartup(typeof(Startup))]
namespace BMSA.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}