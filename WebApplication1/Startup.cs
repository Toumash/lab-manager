#define PUBLISH
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
    public partial class Startup
    {

        // corresponds to the connectionString from main web.config
#if PUBLISH
        public static readonly string Environment = "remote";
#else
         public static readonly string Environment = "local";
#endif

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}