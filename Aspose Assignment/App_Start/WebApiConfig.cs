using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace Aspose_Assignment
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Api_Storage_Post",
                routeTemplate: "Word/{controller}",
                defaults: null,
                constraints: new { controller = "Storage", httpMethod = new HttpMethodConstraint("POST") }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Convert_Put",
                routeTemplate: "Word/{controller}",
                defaults: new
                {
                    outpath = RouteParameter.Optional
                },
                constraints: new { controller = "Convert", httpMethod = new HttpMethodConstraint("PUT") }
            );


            config.Routes.MapHttpRoute(
            name: "Api_Protection_Get",
            routeTemplate: "Word/{DocName}/{controller}",
            defaults: new
                {
                    Folder = RouteParameter.Optional
                },
            constraints: new { httpMethod = new HttpMethodConstraint("GET") }
         );

            config.Routes.MapHttpRoute(
            name: "Api_Protection_Post",
            routeTemplate: "Word/{DocName}/{controller}",
            defaults: new
            {
                Folder = RouteParameter.Optional,
                NewPassword = RouteParameter.Optional
            },
            constraints: new { httpMethod = new HttpMethodConstraint("POST") }
         );

        }
    }
}
