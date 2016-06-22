using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        // corresponds to the connectionString from main web.config
        public static readonly string Environment = "local";

        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);
        }
    }
}
