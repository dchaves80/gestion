using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Web.Api;
using DotNetNuke.Security;

namespace Christoc.Modules.Facturacion
{

    public class ModuleTaskController : DnnApiController
    {
        [AllowAnonymous]
        [HttpGet]

        public HttpResponseMessage SearchProduct(string Search, bool ByCode)
        {

            string SearchChain = Search.Replace("_", " ");
            string result="";
            if (ByCode == false)
            {
                result = Data2.Class.Struct_Producto.GetProductsNameByName(Search, UserInfo.UserID, 5);
            }
            else 
            {
                result = Data2.Class.Struct_Producto.GetProductsCodeByCode(Search, UserInfo.UserID, 5);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
            
/*
            Data2.Class.Struct_Producto PD = Data2.Class.Struct_Producto.Get_SingleArticle(UID, PID);

            if (PD != null && UserInfo.UserID == UID)
            {

                Data2.Class.Struct_Unidades U = new Data2.Class.Struct_Unidades(PD.IdUnidad);





                if (PD.UpdateStock(cant) == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "ok");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "error");
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "error");
            }


            */
        }
    }

}