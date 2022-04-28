using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ThreeDPrintMVC.Startup))]
namespace ThreeDPrintMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
