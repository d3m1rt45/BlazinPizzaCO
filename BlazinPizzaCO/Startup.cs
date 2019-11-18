using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlazinPizzaCO.Startup))]
namespace BlazinPizzaCO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
