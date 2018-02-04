using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebformsCms.Startup))]
namespace WebformsCms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
