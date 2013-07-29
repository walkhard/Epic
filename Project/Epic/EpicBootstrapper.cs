using System.Data.Entity;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Epic.Database;
using Epic.Models;
using Epic.Services;

namespace Epic
{
    public class EpicBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup (TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup (container, pipelines);
            System.Data.Entity.Database.SetInitializer<EpicDatabase> (new EpicDatabaseInitializer ());

            Nancy.Session.CookieBasedSessions.Enable (pipelines);
        }

        protected override void ConfigureApplicationContainer (TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer (container);
        }

        protected override void ConfigureRequestContainer (TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer (container, context);

            // Register request scoped services.
            container.Register<IEpicDatabase, EpicDatabase> ();
            container.Register<IUserMapper, UserMapper> ();
        }

        protected override void RequestStartup (TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
        {
            // activate forms authentication
            var formsAuthConfiguration = new FormsAuthenticationConfiguration {
                RedirectUrl = "~/login",
                UserMapper = requestContainer.Resolve<IUserMapper>(),
            };

            FormsAuthentication.Enable (pipelines, formsAuthConfiguration);

            // set selected settings
            pipelines.AfterRequest.AddItemToEndOfPipeline (ctx => 
            {
                var settingsService = requestContainer.Resolve<ISettingsService> ();
                var settings = settingsService.Get();

                context.ViewBag.Title = settings.Title;
                context.ViewBag.MetaTitle = settings.MetaTitle;
            });
            Nancy.Session.CookieBasedSessions.Enable (pipelines);
            // load notifications
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                string notifications = ctx.Request.Session["EpicAlerts"] as string;
                ctx.ViewBag.Notifications = notifications;
                ctx.Request.Session.Delete("EpicAlerts");
            });
        }
    }
}