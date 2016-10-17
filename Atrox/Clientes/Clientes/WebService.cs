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
using System.Web.Script.Serialization;

namespace Christoc.Modules.Clientes
{

    public class ModuleTaskController : DnnApiController
    {

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage SSC(string K, int idc)
        {

            Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
            int iduser = SWS.GetUserByPrivateKey(K);
            if (iduser != 0)
            {
                Data2.Class.Struct_Cliente  MyClient = Data2.Class.Struct_Cliente.GetClient(idc, iduser);
                if (MyClient != null)
                {
                    string R = Data2.Statics.Conversion.GetJasonFromObject<Data2.Class.Struct_Cliente>(MyClient);
                    return Request.CreateResponse(HttpStatusCode.OK, R);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "null");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "null");
            }


            

        }

        [AllowAnonymous]
        [HttpGet]

        
        

        public HttpResponseMessage SC(string K, string ss)
        {

            Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
            int iduser = SWS.GetUserByPrivateKey(K);
            if (iduser != 0)
            {
                List<Data2.Class.Struct_Cliente> MyList = Data2.Class.Struct_Cliente.SearchClient(ss, iduser);
                if (MyList != null)
                {
                    string R = Data2.Statics.Conversion.GetJasonFromList<List<Data2.Class.Struct_Cliente>>(MyList);
                    return Request.CreateResponse(HttpStatusCode.OK, R);

                }
                else 
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "null");
                }
            }
            else 
            {
                return Request.CreateResponse(HttpStatusCode.OK, "null");
            }








        }

        public HttpResponseMessage SA(string K, string ss)
        {




                return Request.CreateResponse(HttpStatusCode.OK, "null");




        }
    }

}