using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SAFERUN.IMS.Web.Startup))]
namespace SAFERUN.IMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
