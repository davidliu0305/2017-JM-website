using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JMWebsite.Startup))]
namespace JMWebsite
{
    public partial class Startup
    {
        //hello
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
