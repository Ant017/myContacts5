using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myContacts5.Startup))]
namespace myContacts5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
