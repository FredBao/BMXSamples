using Microsoft.Owin;

[assembly: OwinStartup(typeof(Sample_AntiForgeryToken.Startup))]

namespace Sample_AntiForgeryToken
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}