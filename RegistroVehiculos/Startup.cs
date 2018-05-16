using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RegistroVehiculos.Startup))]
namespace RegistroVehiculos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
