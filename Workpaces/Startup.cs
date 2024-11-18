using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Workpaces.Startup))]
namespace Workpaces
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
