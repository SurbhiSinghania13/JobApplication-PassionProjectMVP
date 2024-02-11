using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jobApplicationTracking.Startup))]
namespace jobApplicationTracking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
