using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Web.Api;

namespace Christoc.Modules.ConfiguracionesDeCuenta
{
    public class Mapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("ConfiguracionesDeCuenta", "default", "{controller}/{action}", new[] { "Christoc.Modules.ConfiguracionesDeCuenta" });
        }
    }
}