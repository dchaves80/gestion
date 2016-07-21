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

namespace Christoc.Modules.Factura2
{

    public class ModuleTaskController : DnnApiController
    {

        [AllowAnonymous]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [HttpGet]
        public HttpResponseMessage SendTicket(string KEY, string F,string S)
        {
            try
            {
                Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
                int IdUser = SWS.GetUserByPrivateKey(KEY);
                if (IdUser != 0)
                {
                    int IdFactura = int.Parse(F);
                    string returnString = SWS.UpdateFacturaTicket(IdUser, IdFactura, S);
                    return Request.CreateResponse(HttpStatusCode.OK, returnString);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "null");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, "null");
            }
        }

        [AllowAnonymous]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [HttpGet]
        public HttpResponseMessage GetDetallesFactura(string KEY, string F)
        {
            try
            {
                Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
                int IdUser = SWS.GetUserByPrivateKey(KEY);
                if (IdUser != 0)
                {
                    int IdFactura = int.Parse(F);
                    //string returnString = SWS.GetDetalleFactura(IdUser, IdFactura);
                    return Request.CreateResponse(HttpStatusCode.OK, "null");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "null");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, "null");
            }
        }

        [AllowAnonymous]
        [DnnModuleAuthorize(AccessLevel=SecurityAccessLevel.View)]
        [HttpGet]
        public HttpResponseMessage GetDatosFactura(string KEY, string F) 
        {
            try
            {
                Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
                int IdUser = SWS.GetUserByPrivateKey(KEY);
                if (IdUser != 0)
                {
                    int IdFactura = int.Parse(F);
                    string returnString = SWS.GetDatosFacturas(IdUser, IdFactura).GetSerializad();
                    return Request.CreateResponse(HttpStatusCode.OK, returnString);
                }
                else 
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "null");
                }
            }
            catch (Exception E)
            {
                return Request.CreateResponse(HttpStatusCode.OK, E.Message);
            }
        }
        [AllowAnonymous]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [HttpGet]
        public HttpResponseMessage GetFacturasDisponibles(string KEY)
        {

            Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();


            int IdUser = SWS.GetUserByPrivateKey(KEY);
            if (IdUser != 0)
            {

                
                
                return Request.CreateResponse(HttpStatusCode.OK, SWS.GetFacturasDisponibles(IdUser));

            }
            else 
            {
                return Request.CreateResponse(HttpStatusCode.OK, "null");
            }
            

          
        }
    }

}