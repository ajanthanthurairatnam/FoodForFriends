using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodForFriends.Startup))]
namespace FoodForFriends
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
