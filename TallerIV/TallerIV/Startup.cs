using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TallerIV.Startup))]
namespace TallerIV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
