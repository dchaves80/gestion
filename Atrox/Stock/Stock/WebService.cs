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

namespace Christoc.Modules.Stock
{
   
        public class ModuleTaskController : DnnApiController
        {
            [AllowAnonymous]
            [HttpGet]
            
            public HttpResponseMessage UpdateStock(int UID,int PID,string cant, int unit, string key) 
            {

                

                Data2.Class.Struct_Producto PD = Data2.Class.Struct_Producto.Get_SingleArticle(UID, PID);

                if (PD != null && UserInfo.UserID==UID)
                {

                    Data2.Class.Struct_Unidades U = new Data2.Class.Struct_Unidades(PD.IdUnidad);

                    if (PD.IdUnidad != unit) 
                    {
                        Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
                        int IdUser = SWS.GetUserByPrivateKey(key);
                        if (IdUser != 0)
                        {
                            PD.IdUnidad = unit;
                            PD.Actualizar(IdUser);
                        }
                    }



                   

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


                
            }
        }
    
}