using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecureContainer.Startup))]
namespace SecureContainer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
