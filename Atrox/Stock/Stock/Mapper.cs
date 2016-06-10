using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Web.Api;

namespace Christoc.Modules.Stock
{
    public class Mapper:IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Stock", "default", "{controller}/{action}", new[] { "Christoc.Modules.Stock" });
        }
    }
}