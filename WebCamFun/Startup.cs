using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Owin;
using Microsoft.Owin;
using WebCamFun.Hubs;

[assembly: OwinStartup(typeof(WebCamFun.Startup))]
namespace WebCamFun
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DashboardHub>().AsImplementedInterfaces().SingleInstance()/*.ExternallyOwned()*/;

            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterFilterProvider();

            // BUILD THE CONTAINER
            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // REGISTER WITH OWIN
            //app.UseAutofacMiddleware(container);
            //app.UseAutofacMvc();
            //ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}