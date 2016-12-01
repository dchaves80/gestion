/*
' Copyright (c) 2016  Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Christoc.Modules.Remitos
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from RemitosModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : RemitosModuleBase, IActionable
    {

        string RemitoString = "MyRemito";

        void LoadRemitos() 
        {
            List<Data2.Class.Struct_Remito> LR = Data2.Class.Struct_Remito.GetAllRemitos(UserId);
            bool alternatecolorow = false;
            if (LR != null && LR.Count > 0) 
            {
                for (int a = 0; a < LR.Count; a++) 
                {

                    string classnamerow = "";

                   

                    if (alternatecolorow == true)
                    {
                        alternatecolorow = false;
                        classnamerow = "metroparline";
                    }
                    else
                    {
                        alternatecolorow = true;
                        classnamerow = "metroimparline";
                    }


                    classnamerow = classnamerow + " animationline";


                    HtmlGenericControl TR = new HtmlGenericControl("tr");
                    TR.Attributes.Add("class", classnamerow);
                    HtmlGenericControl TD_NRO = new HtmlGenericControl("td");
                    HtmlGenericControl TD_PROVEE = new HtmlGenericControl("td");
                    HtmlGenericControl TD_FECHA = new HtmlGenericControl("td");
                    HtmlGenericControl TD_TOTAL = new HtmlGenericControl("td");
                    HtmlGenericControl TD_MOSTRAR = new HtmlGenericControl("td");

                    TD_NRO.InnerText = LR[a].NUMEROREMITO;
                    TD_PROVEE.InnerText = LR[a].Supplier.Nombre + "(" + LR[a].Supplier.NombreFantasia + ")";
                    TD_FECHA.InnerText = LR[a].Fecha.ToString();
                    TD_TOTAL.InnerText = LR[a].total.ToString();
                    TD_MOSTRAR.InnerText = "Función en desarrollo";

                    TR.Controls.Add(TD_NRO);
                    TR.Controls.Add(TD_PROVEE);
                    TR.Controls.Add(TD_FECHA);
                    TR.Controls.Add(TD_TOTAL);
                    TR.Controls.Add(TD_MOSTRAR);

                    tbl_ListadoFacturas.Controls.Add(TR);
                    
                }
            }
        }

        void LoadProviders() 
        {
            if (!IsPostBack)
            {
                DataTable DT = Data2.Connection.D_Supplier.Get_AllShort(UserId);
                cmb_Providers.Items.Clear();
                if (DT != null && DT.Rows.Count > 0)
                {
                    foreach (DataRow D in DT.Rows)
                    {
                        string nombreproveedor = D["Nombre"].ToString();
                        if (nombreproveedor == "")
                        {
                            nombreproveedor = D["NombreFantasía"].ToString();
                        }
                        System.Web.UI.WebControls.ListItem LI = new System.Web.UI.WebControls.ListItem(nombreproveedor, D["Id"].ToString());
                        cmb_Providers.Items.Add(LI);
                    }
                }
                if (ObtenerRemito() != null && ObtenerRemito().get_SUPPLIER()!=null)
                {
                    int selectedindex = 0;
                        Data2.Class.Struct_Remito SR = ObtenerRemito();
                        for (int a=0;a<cmb_Providers.Items.Count;a++)
                        {
                            if (ObtenerRemito().get_SUPPLIER().Id.ToString() == cmb_Providers.Items[a].Value)
                            {
                                selectedindex = a;
                                break;
                            }
                        }
                        cmb_Providers.SelectedIndex = selectedindex;
                    

                }
            }
            else 
            {
                if (ObtenerRemito() != null && ObtenerRemito().get_SUPPLIER()!=null) 
                {
                    if (cmb_Providers.SelectedValue!=ObtenerRemito().get_SUPPLIER().Id.ToString())
                    {
                        Data2.Class.Struct_Remito SR = ObtenerRemito();
                        SR.ListaArticulos.Clear();
                        SR.set_SUPPLIER(int.Parse(cmb_Providers.SelectedValue));
                    } 

                }
            }
        }


        Data2.Class.Struct_Remito ObtenerRemito() 
        {
            if (Session[RemitoString] != null)
            {
                Data2.Class.Struct_Remito R = Session[RemitoString] as Data2.Class.Struct_Remito;
                return R;
            }
            else 
            {
                return null;
            }
        }

        void LoadControls() 
        {

            Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
            string IDU = SWS.GetPrivateKeyByIdUser(UserId);
            hf_key.Value = IDU;

            if (Session[RemitoString] != null)
            {
                FormRemito.Visible = true;
                NewRemito.Visible = false;
                btnCancelarFactura.Visible = true;
            }
            else 
            {
                FormRemito.Visible = false;
                NewRemito.Visible = true;
                btnCancelarFactura.Visible = false;

            }
        }

        private void agregararticulo(string idart, string cant)
        {
            int idarticulo = int.Parse(idart);
            Data2.Class.Struct_Remito R = ObtenerRemito();
            R.AddArticle(idarticulo, cant);
            R.ListaArticulos[R.ListaArticulos.Count - 1].setCANT(cant);
            Session.Remove(RemitoString);
            Session.Add(RemitoString, R);
            redirectome();
        }

        void Interpretar() 
        {
            if (Request["addart"]!=null && Request["cant"]!="undefined") agregararticulo(Request["addart"],Request["cant"]);
            if (Request["delart"] != null) borrarart(Request["delart"]);
            
        }

        private void borrarart(string delart)
        {
            if (ObtenerRemito() != null) 
            {
                Data2.Class.Struct_Remito R = Session[RemitoString] as Data2.Class.Struct_Remito;
                foreach (Data2.Class.Struct_DetalleRemito ART in R.ListaArticulos) 
                {
                    if (ART.P.Id.ToString() == delart) 
                    {
                        R.ListaArticulos.Remove(ART);
                        Session.Remove(RemitoString);
                        Session.Add(RemitoString, R);
                        break;
                    }
                }
                redirectome();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadControls();
                LoadProviders();
                Interpretar();
                CargarGrilla();
                LoadRemitos();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        void CargarGrilla() 
        {

            decimal total=0;
            tb_Detalle.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            if (ObtenerRemito() != null && ObtenerRemito().ListaArticulos != null && ObtenerRemito().ListaArticulos.Count > 0) 
            {

                bool alternatecolorow = false;

                Data2.Class.Struct_Remito R = Session[RemitoString] as Data2.Class.Struct_Remito;
                for (int a = 0; a < R.ListaArticulos.Count; a++) 
                {

                    string classnamerow = "";

                    HtmlGenericControl TR = new HtmlGenericControl("tr"); // fila
                    HtmlGenericControl td_borrar = new HtmlGenericControl("td");
                    HtmlGenericControl td_detalle = new HtmlGenericControl("td");
                    HtmlGenericControl td_cantidad = new HtmlGenericControl("td");
                    HtmlGenericControl td_precio = new HtmlGenericControl("td");
                    HtmlGenericControl td_total = new HtmlGenericControl("td");

                    if (alternatecolorow == true)
                    {
                        alternatecolorow = false;
                        classnamerow = "metroparline";
                    }
                    else 
                    {
                        alternatecolorow = true;
                        classnamerow = "metroimparline";
                    }


                    classnamerow = classnamerow + " animationline";
                    TR.Attributes.Add("class", classnamerow);



                    td_borrar.InnerText = "Borrar Art.";
                    td_borrar.Attributes.Add("class", "BorrarArtButton");
                    td_borrar.Attributes.Add("onclick", "BorrarArt('" + R.ListaArticulos[a].P.Id + "');");

                    td_detalle.InnerText = R.ListaArticulos[a].P.Descripcion;
                    td_detalle.Attributes.Add("class", "descripcionarticulo");


                    decimal cant = 0;
                    if (R.ListaArticulos[a].IsDecimal == true)
                    {
                        td_cantidad.InnerText = R.ListaArticulos[a].CANTDEC.ToString();
                        cant = R.ListaArticulos[a].CANTDEC;
                    }
                    else 
                    {
                        td_cantidad.InnerText = R.ListaArticulos[a].CANTINT.ToString();
                        cant = Data2.Statics.Conversion.GetDecimal(R.ListaArticulos[a].CANTINT.ToString());
                    }
                    td_precio.InnerText = R.ListaArticulos[a].P.PrecioCompra.ToString();
                    total = total + (cant * R.ListaArticulos[a].P.PrecioCompra);
                    td_total.InnerText = (cant * R.ListaArticulos[a].P.PrecioCompra).ToString();



                    TR.Controls.Add(td_borrar);
                    TR.Controls.Add(td_detalle);
                    TR.Controls.Add(td_cantidad);
                    TR.Controls.Add(td_precio);
                    TR.Controls.Add(td_total);
                    

                    tb_Detalle.Controls.Add(TR);

                }

                HtmlGenericControl TR_rowtotal = new HtmlGenericControl("tr");
                TR_rowtotal.Attributes.Add("class", "colorpiedefactura");
                HtmlGenericControl TD_total = new HtmlGenericControl("td");
                HtmlGenericControl TD_totalmonto = new HtmlGenericControl("td");
                TD_total.Attributes.Add("class", "labelpiedefactura");
                TD_total.Attributes.Add("colspan", "4");

                TD_total.InnerText = "Total:";
                TD_totalmonto.InnerText = total.ToString();
                TD_totalmonto.Attributes.Add("class", "labelpiedefactura");

                TR_rowtotal.Controls.Add(TD_total);
                TR_rowtotal.Controls.Add(TD_totalmonto);
                tb_Detalle.Controls.Add(TR_rowtotal);




            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        void redirectome()
        {
            Response.Redirect("http://" + Request.Url.Host +"/MyManager/Remitos");

        }

        protected void NewRemito_Click(object sender, EventArgs e)
        {
            Data2.Class.Struct_Remito R =  new Data2.Class.Struct_Remito(UserId);
            R.set_SUPPLIER(int.Parse(cmb_Providers.SelectedValue));
            Session.Add(RemitoString,R);
            redirectome();
        }

        protected void cmb_Providers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data2.Class.Struct_Remito R = Session[RemitoString] as Data2.Class.Struct_Remito;
            R.set_SUPPLIER(int.Parse(cmb_Providers.SelectedValue));

            Session.Remove(RemitoString);
            Session.Add(RemitoString, R);
        }

        protected void btnCancelarFactura_Click(object sender, EventArgs e)
        {
            Session.Remove(RemitoString);
            redirectome();
        }

        protected void btnAceptarFactura_Click(object sender, EventArgs e)
        {
            Data2.Class.Struct_Remito Remito = ObtenerRemito();
            if (Remito != null) 
            {
                Data2.Statics.Log.ADD(txt_numeroremito.Text, this);
                Remito.NUMEROREMITO = txt_numeroremito.Text;
                if (Remito.SaveRemito())
                {
                    
                    Session.Remove(RemitoString);
                    redirectome();
                }
                else 
                {

                    redirectome();
                }
            }
        }
    }
}