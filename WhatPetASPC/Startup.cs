using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhatPetASPC
.Startup))]
namespace WhatPetASPC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
