using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Web.Api;

namespace Christoc.Modules.Facturacion3
{
    public class Mapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Facturacion3", "default", "{controller}/{action}", new[] { "Christoc.Modules.Facturacion3" });
        }
    }
}