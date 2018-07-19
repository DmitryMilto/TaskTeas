using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TastTest.Startup))]
namespace TastTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
