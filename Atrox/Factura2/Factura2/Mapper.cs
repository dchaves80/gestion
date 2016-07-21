using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Web.Api;

namespace Christoc.Modules.Factura2
{
    public class Mapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Factura2", "default", "{controller}/{action}", new[] { "Christoc.Modules.Factura2" });
        }
    }
}