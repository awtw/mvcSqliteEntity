using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(csvWeb_v3_sqlite.Startup))]
namespace csvWeb_v3_sqlite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
