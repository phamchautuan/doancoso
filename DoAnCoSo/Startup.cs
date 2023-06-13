using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoAnCoSo.Startup))]
namespace DoAnCoSo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
