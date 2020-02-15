using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Car_Galery.Startup))]
namespace Car_Galery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
