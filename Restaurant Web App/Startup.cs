using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Restaurant_Web_App.Startup))]
namespace Restaurant_Web_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
