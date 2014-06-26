using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompatiblTestApp.Startup))]
namespace CompatiblTestApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
