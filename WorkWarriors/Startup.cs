using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkWarriors.Startup))]
namespace WorkWarriors
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
