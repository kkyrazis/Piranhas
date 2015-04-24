using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Piranhas.Startup))]
namespace Piranhas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
