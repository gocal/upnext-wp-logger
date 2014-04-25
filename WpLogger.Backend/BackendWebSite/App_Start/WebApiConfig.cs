using System;
using System.Web.Http;

namespace BackendWebSite
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Device",
                routeTemplate: "api/device/",
                defaults: new { controller = "devices" }
            );

            config.Routes.MapHttpRoute(
                name: "DeviceById",
                routeTemplate: "api/device/{deviceId}/",
                defaults: new { controller = "devices" }
            );

            config.Routes.MapHttpRoute(
                name: "App",
                routeTemplate: "api/device/{deviceId}/app/",
                defaults: new { controller = "apps" }
            );

            config.Routes.MapHttpRoute(
                name: "AppById",
                routeTemplate: "api/device/{deviceId}/app/{appId}",
                defaults: new { controller = "apps" }
            );

            config.Routes.MapHttpRoute(
                name: "LogWithDates",
                routeTemplate: "api/device/{deviceId}/app/{appId}/log/{from}/{to}",
                defaults: new { controller = "logs", from = DateTimeOffset.MinValue, to = DateTimeOffset.MaxValue }
            );
        }
    }
}
