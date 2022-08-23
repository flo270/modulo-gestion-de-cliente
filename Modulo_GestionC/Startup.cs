using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Modulo_GestionC.Startup))]
namespace Modulo_GestionC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
