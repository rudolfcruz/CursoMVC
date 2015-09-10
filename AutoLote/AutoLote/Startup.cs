using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoLote.Startup))]
namespace AutoLote
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
