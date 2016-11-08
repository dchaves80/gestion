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
using Data2.Class;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Christoc.Modules.Facturacion3
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from Facturacion3ModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------



    public partial class View : Facturacion3ModuleBase, IActionable
    {

        string key_session_factura = "MiFactura";

        void configmodule()
        {
            Data2.Connection.D_StaticWebService STWS = new Data2.Connection.D_StaticWebService();
            string K = STWS.GetPrivateKeyByIdUser(UserId);
            key.Value = K;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
    Request.ApplicationPath.TrimEnd('/') + "/";
            baseurl.Value = baseUrl;
            url.Value = Request.RawUrl.Split('?')[0];


        }

        void AgregarArticulo(int IdArt, string cant)
        {
            if (Session[key_session_factura] != null)
            {
                try
                {
                    Data2.Class.Struct_Factura SF = Session[key_session_factura] as Data2.Class.Struct_Factura;



                    SF.AddDetail(IdArt);
                    SF.GetDetalle()[SF.GetDetalle().Count - 1].set_cant(cant.ToString());
                    redirecttome();

                }
                catch { }
            }
        }




        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session[key_session_factura] != null)
            {
                ConfigurarControlesFactura();
                ArmarFactura();
            }
            else
            {
                controlesFactura.Visible = false;
                btn_CancelarVenta.Visible = false;
            }


            if (Request["ba"] != null)
            {
                BorrarArticulo(Request["ba"].ToString());
            }

            if (Request["addart"] != null && Request["cant"] != null)
            {
                try
                {
                    AgregarArticulo(int.Parse(Request["addart"].ToString()), Request["cant"].ToString());
                }
                catch { }
            }

            try
            {
                configmodule();


            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void BorrarArticulo(string p_keyart)
        {
            Struct_Factura Fact = getFactura();
            Fact.BorrarDetalle(p_keyart);
            replaceFactura(Fact);
            redirecttome();
        }

        private void ArmarFactura()
        {
            detailfactura.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            if (Session[key_session_factura] != null)
            {
                Struct_Factura F = Session[key_session_factura] as Struct_Factura;
                if (F.GetDetalle() != null && F.GetDetalle().Count > 0)
                {
                    //Listando ivas en hiddenfield
                    string ivas = "";
                    List<decimal> _ListadoDeIvas = F.GetIvasInscriptos();
                    foreach (decimal i in F.GetIvasInscriptos())
                    {

                        if (i != F.GetIvasInscriptos()[F.GetIvasInscriptos().Count - 1])
                        {
                            ivas = ivas + i.ToString() + "!";
                        }
                        else
                        {
                            ivas = ivas + i.ToString();
                        }

                    }
                    ivaslist.Value = ivas;
                    detail.Value = "1";
                    //armando detalle

                    bool par = true;
                    string keys = "";
                    for (int a = 0; a < F.GetDetalle().Count; a++)
                    {
                        if (a < F.GetDetalle().Count - 1)
                        {
                            keys = keys + F.GetDetalle()[a].ACCESSKEY + "!";
                        }
                        else
                        {
                            keys = keys + F.GetDetalle()[a].ACCESSKEY; ;
                        }
                    }
                    mykeys.Value = keys;

                    foreach (Struct_DetalleFactura _Det in F.GetDetalle())
                    {

                        HtmlGenericControl _row_detail = new HtmlGenericControl("tr");
                        HtmlGenericControl _borrar_detail = new HtmlGenericControl("td");
                        HtmlGenericControl _cant_detail = new HtmlGenericControl("td");
                        HtmlGenericControl _cant_input = new HtmlGenericControl("input");
                        HtmlGenericControl _detail_detail = new HtmlGenericControl("td");
                        HtmlGenericControl _pu_detail = new HtmlGenericControl("td");
                        HtmlGenericControl _total_detail = new HtmlGenericControl("td");

                        _detail_detail.InnerText = _Det.PRODUCTO.Descripcion;
                        _detail_detail.Attributes.Add("class", "descripcionarticulo");


                        HtmlGenericControl _puiva = new HtmlGenericControl("div");
                        HtmlGenericControl _punoiva = new HtmlGenericControl("div");

                        HtmlGenericControl _ivapercent = new HtmlGenericControl("div");
                        _ivapercent.Attributes.Add("style", "display:none");
                        _ivapercent.Attributes.Add("id", "iva_" + _Det.ACCESSKEY);
                        _puiva.Attributes.Add("class", "puiva valuepiedefactura");
                        _punoiva.Attributes.Add("class", "punoiva valuepiedefactura");



                        _puiva.InnerText = _Det.PRODUCTO.PrecioFinal.ToString("#.00");
                        _punoiva.InnerText = _Det.getPrecioFinalSinIva().ToString("#.00");
                        _ivapercent.InnerText = _Det.PRODUCTO.IVA.ToString("#.00");
                        _puiva.ID = "puiva_" + _Det.ACCESSKEY;
                        _punoiva.ID = "punoiva_" + _Det.ACCESSKEY;

                        _puiva.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        _punoiva.ClientIDMode = System.Web.UI.ClientIDMode.Static;


                        HtmlGenericControl _totaliva = new HtmlGenericControl("div");
                        HtmlGenericControl _totalnoiva = new HtmlGenericControl("div");

                        _totaliva.Attributes.Add("class", "totaliva valuepiedefactura");
                        _totalnoiva.Attributes.Add("class", "totalnoiva valuepiedefactura");
                        _totaliva.ID = "totaliva_" + _Det.ACCESSKEY;
                        _totalnoiva.ID = "totalnoiva_" + _Det.ACCESSKEY;
                        _totaliva.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        _totalnoiva.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                        _pu_detail.Controls.Add(_ivapercent);
                        _pu_detail.Controls.Add(_puiva);
                        _pu_detail.Controls.Add(_punoiva);
                        _total_detail.Controls.Add(_totaliva);
                        _total_detail.Controls.Add(_totalnoiva);

                        _cant_input.ID = "input_" + _Det.ACCESSKEY;
                        _cant_input.Attributes.Add("onkeyup", "changingcant('" + _Det.ACCESSKEY + "')");
                        _cant_input.Attributes.Add("class", "valuepiedefactura AtroxTextBoxMount");
                        _cant_input.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        if (_Det.isdec)
                        {
                            _cant_input.Attributes.Add("value", _Det.DETALLEDEC.ToString("#.00"));
                        }
                        else
                        {
                            _cant_input.Attributes.Add("value", _Det.DETALLEINT.ToString());
                        }
                        _cant_input.Disabled = true;

                        //Columnas de iva
                        HtmlGenericControl _ivaporcentage_detail = new HtmlGenericControl("td");
                        HtmlGenericControl _ivatotal_detail = new HtmlGenericControl("td");


                        _ivaporcentage_detail.Attributes.Add("class", "detailwithiva valuepiedefactura");
                        _ivatotal_detail.Attributes.Add("class", "detailwithiva valuepiedefactura");
                        _ivaporcentage_detail.InnerText = _Det.PRODUCTO.IVA.ToString("#.00") + "%";
                        _ivatotal_detail.ID = "iva_" + _Det.ACCESSKEY;
                        //fin columnas de iva

                        //agregar elementos

                        int ivaindex = 0;
                        for (int _ivas = 0; _ivas < _ListadoDeIvas.Count; _ivas++)
                        {
                            if (_ListadoDeIvas[_ivas] == _Det.PRODUCTO.IVA)
                            {
                                ivaindex = _ivas;
                                break;
                            }
                        }


                        //preparando boton borrar
                        _borrar_detail.InnerText = "Borrar";
                        _borrar_detail.Attributes.Add("onclick", "BA('" + _Det.ACCESSKEY + "');");
                        _borrar_detail.Attributes.Add("class", "BorrarArtButton");
                        //fin preparando boton borrar

                        //Preparando columna de total del iva
                        HtmlGenericControl _ivaunitarioHF = new HtmlGenericControl("div");
                        HtmlGenericControl _ivadetailtotal = new HtmlGenericControl("div");
                        _ivadetailtotal.ID = "ivadetailtotal_" + _Det.ACCESSKEY;
                        _ivadetailtotal.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        _ivaunitarioHF.InnerText = (_Det.PRODUCTO.PrecioFinal - _Det.getPrecioFinalSinIva()).ToString("#.00");
                        _ivaunitarioHF.ID = "ivaunitario_" + _Det.ACCESSKEY;
                        _ivaunitarioHF.Attributes.Add("style", "display:none");
                        _ivadetailtotal.Attributes.Add("class", "ivaindexvalue" + ivaindex);
                        _ivaunitarioHF.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        _ivatotal_detail.Controls.Add(_ivaunitarioHF);
                        _ivatotal_detail.Controls.Add(_ivadetailtotal);
                        //fin Preparando columna de total del iva

                        _cant_detail.Controls.Add(_cant_input);
                        _row_detail.Controls.Add(_borrar_detail);
                        _row_detail.Controls.Add(_cant_detail);
                        _row_detail.Controls.Add(_detail_detail);
                        _row_detail.Controls.Add(_pu_detail);
                        _row_detail.Controls.Add(_total_detail);
                        _row_detail.Controls.Add(_ivaporcentage_detail);
                        _row_detail.Controls.Add(_ivatotal_detail);


                        detailfactura.Controls.Add(_row_detail);
                        if (par)
                        {
                            _row_detail.Attributes.Add("class", "metroparline animationline");
                            par = false;
                        }
                        else
                        {
                            par = true;
                            _row_detail.Attributes.Add("class", "metroimparline animationline");

                        }

                        //fin agregar elementos






                    }

                    //SubTotal
                    HtmlGenericControl row_SubTotal = new HtmlGenericControl("tr");
                    HtmlGenericControl cell_label_subtotal = new HtmlGenericControl("td");
                    HtmlGenericControl cell_value_subtotal = new HtmlGenericControl("td");

                    cell_label_subtotal.Attributes.Add("colspan", "4");
                    row_SubTotal.Attributes.Add("class", "detailwithiva colorpiedefactura");
                    cell_value_subtotal.ID = "cell_value_subtotal";
                    cell_label_subtotal.Attributes.Add("class", "labelpiedefactura");
                    cell_value_subtotal.Attributes.Add("class", "valuepiedefactura");
                    cell_value_subtotal.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    cell_label_subtotal.InnerText = "SubTotal:";

                    row_SubTotal.Controls.Add(cell_label_subtotal);
                    row_SubTotal.Controls.Add(cell_value_subtotal);
                    detailfactura.Controls.Add(row_SubTotal);
                    //fin SubTotal

                    //ivas Inscriptos
                    List<decimal> _decimalinscriptos = F.GetIvasInscriptos();
                    for (int ivacounter = 0; ivacounter < _decimalinscriptos.Count; ivacounter++)
                    {
                        HtmlGenericControl row_total_ivainscripto = new HtmlGenericControl("tr");
                        HtmlGenericControl cell_label_ivainscripto = new HtmlGenericControl("td");
                        HtmlGenericControl cell_value_ivainscripto = new HtmlGenericControl("td");

                        row_total_ivainscripto.Attributes.Add("class", "detailwithiva colorpiedefactura");
                        cell_value_ivainscripto.ID = "ivaindex" + ivacounter;
                        cell_value_ivainscripto.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        cell_value_ivainscripto.Attributes.Add("class", "valuepiedefactura");

                        cell_label_ivainscripto.Attributes.Add("colspan", "4");
                        cell_label_ivainscripto.Attributes.Add("class", "labelpiedefactura");
                        cell_label_ivainscripto.InnerText = "Iva incripto: " + _decimalinscriptos[ivacounter].ToString("#.00") + "%";

                        row_total_ivainscripto.Controls.Add(cell_label_ivainscripto);
                        row_total_ivainscripto.Controls.Add(cell_value_ivainscripto);
                        detailfactura.Controls.Add(row_total_ivainscripto);

                    }
                    //Fin ivas Inscriptos

                    //total completo
                    HtmlGenericControl row_total_final = new HtmlGenericControl("tr");
                    HtmlGenericControl cell_label_final = new HtmlGenericControl("td");
                    HtmlGenericControl cell_value_final = new HtmlGenericControl("td");
                    cell_label_final.Attributes.Add("colspan", "4");

                    cell_label_final.InnerText = "Total:";
                    cell_value_final.ID = "finaltotal";
                    cell_value_final.Attributes.Add("class", "valuepiedefactura");
                    cell_label_final.Attributes.Add("class", "labelpiedefactura");
                    cell_value_final.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    row_total_final.Attributes.Add("class", "colorpiedefactura");
                    row_total_final.Controls.Add(cell_label_final);
                    row_total_final.Controls.Add(cell_value_final);
                    detailfactura.Controls.Add(row_total_final);


                    //fin total completo


                }
                else
                {
                    detail.Value = "0";
                }
            }
        }

        private void ConfigurarControlesFactura()
        {
            btn_NuevaVenta.Visible = false;
            btn_CancelarVenta.Visible = true;
            controlesFactura.Visible = true;
            controlesFactura.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            ControlesFacturaA.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            cmbVendedor.Items.Clear();
            List<Struct_Vendedores> SellerList = Data2.Class.Struct_Vendedores.GetAllVendedores(UserId);
            cmbVendedor.Items.Add(new System.Web.UI.WebControls.ListItem("Ninguno","0"));
            if (SellerList != null && SellerList.Count > 0)
            {
                for (int a = 0; a < SellerList.Count; a++)
                {
                    System.Web.UI.WebControls.ListItem ListI = new System.Web.UI.WebControls.ListItem(SellerList[a].NombreVendedor, SellerList[a].Id.ToString());
                    cmbVendedor.Items.Add(ListI);
                }
            }
            else 
            {

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

        void redirecttome()
        {
            Response.Redirect(url.Value = Request.RawUrl.Split('?')[0]);
        }
        void redirecttome(string parametersstring)
        {
            Response.Redirect(url.Value = Request.RawUrl.Split('?')[0] + parametersstring);
        }

        protected void btn_NuevaVenta_Click(object sender, EventArgs e)
        {
            Struct_Factura F = new Data2.Class.Struct_Factura(UserId);
            F.setFacturaTipo(Struct_Factura.TipoDeFactura.FacturaB);
            Session.Add(key_session_factura,F);
            redirecttome();

        }

        private void replaceFactura(Struct_Factura myFact)
        {
            if (Session[key_session_factura] != null)
            {
                Session.Remove(key_session_factura);
                Session.Add(key_session_factura, myFact);
            }

        }

        private Struct_Factura getFactura()
        {
            if (Session[key_session_factura] != null)
            {
                return Session[key_session_factura] as Struct_Factura;
            }
            else
            {
                return null;
            }
        }



        protected void btn_CancelarVenta_Click(object sender, EventArgs e)
        {
            if (Session[key_session_factura] != null)
            {
                Session.Remove(key_session_factura);
                redirecttome();
            }
        }

        protected void btn_AceptarVenta_Click(object sender, EventArgs e)
        {
            Struct_Factura F = getFactura();
            F.senores = txt_Nombre.Text;
            F.domicilio = txt_Domicilio.Text;
            F.localidad = txt_Localidad.Text;
            F.cuit = txt_CUIT.Text;
            F.telefono = txt_Telefono.Text;
            switch (Factura_Tipo.SelectedValue)
            {
                case "A":
                    F.setFacturaTipo(Struct_Factura.TipoDeFactura.FacturaA);
                    break;
                case "B":
                    F.setFacturaTipo(Struct_Factura.TipoDeFactura.FacturaB);
                    break;
                case "C":
                    F.setFacturaTipo(Struct_Factura.TipoDeFactura.FacturaC);
                    break;
                case "X":
                    F.setFacturaTipo(Struct_Factura.TipoDeFactura.FacturaX);
                    break;
                case "P":
                    F.setFacturaTipo(Struct_Factura.TipoDeFactura.Presupuesto);
                    break;

            }

            if (F.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA)
            {
                switch (IVAFA.SelectedValue)
                {
                    case "RI":
                        F.Condicion_IVA = Struct_Factura.CondicionIVA.RespInscripto;
                        break;
                    case "RNI":
                        F.Condicion_IVA = Struct_Factura.CondicionIVA.RespNoInscripto;
                        break;
                }
            }
            else 
            {
                switch (IVAFB.SelectedValue)
                {
                    case "E":
                        F.Condicion_IVA = Struct_Factura.CondicionIVA.Exento;
                        break;
                    case "CF":
                        F.Condicion_IVA = Struct_Factura.CondicionIVA.ConsumidorFinal;
                        break;
                    case "RM":
                        F.Condicion_IVA = Struct_Factura.CondicionIVA.RespMonotributo;
                        break;
                    
                }
            }

            switch (cmbFormaPago.SelectedValue) 
            {
                case "C":
                    F.Pago = Struct_Factura.CondicionPago.Contado;
                    //Control para que no se filtre IdCliente
                    IdCliente.Value = "0";
                    break;
                case "CC":
                    F.Pago = Struct_Factura.CondicionPago.CtaCte;
                    break;
            }
            bool succes = false;
            succes = F.GuardarFactura(int.Parse(cmbVendedor.SelectedValue.ToString()),int.Parse(IdCliente.Value));
            messagebox.Attributes.Clear();

            if (succes) 
            {
                messagebox.Attributes.Add("class", "MessageBox MessageSuccess");
                messagebox.InnerText = "Comprobante registrado en el sistema y pendiente de aprobación";
            } 
            else 
            {
                messagebox.Attributes.Add("class", "MessageBox MessageError");
                messagebox.InnerText = "Error al registrar comprobante";
            }
            
           
            
                Session.Remove(key_session_factura);
            erasef.Value = "1";
            
            

        }
    }
}