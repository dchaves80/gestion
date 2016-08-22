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
using System.Web.UI.HtmlControls;
using Data2.Class;
using Data2.Statics;
using System.Collections.Generic;

namespace Christoc.Modules.Factura2
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from Factura2ModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Factura2ModuleBase, IActionable
    {

        string MyURL;
        string FacturaSesssionKEY = "FacturaSessionKey";
        Struct_Factura _F;

        void redirecttome(string par) 
        {
            if (par == "")
            {
                Response.Redirect(MyURL);
            }
            else 
            {
                Response.Redirect(MyURL+par);
            }
        }

        void AgregarProducto(int p_IdProducto, Data2.Class.Struct_Factura p_Factura) 
        {
            p_Factura.AddDetail(p_IdProducto);
            Log.ADD("Agregando Articulo:", this);
            Session.Remove(FacturaSesssionKEY);
            Session.Add(FacturaSesssionKEY, p_Factura);
            redirecttome("");
            
        }


        void AddCant(string value, string k) 
        {
            if (Session[FacturaSesssionKEY] != null)
            {
                Struct_Factura Factura = Session[FacturaSesssionKEY] as Struct_Factura;
                Factura.SetCant(value, k);
                Session.Remove(FacturaSesssionKEY);
                Session.Add(FacturaSesssionKEY, Factura);
                redirecttome("?scroll=true&k=" + k);
                
            }
        }

        void InterpretarRequest() 
        {

            if (Request["scroll"] != null && Request["k"] != null && Session[FacturaSesssionKEY] != null) 
            {
                Data2.Class.Struct_Factura FACTURA = Session[FacturaSesssionKEY] as Data2.Class.Struct_Factura;
                for (int a = 0; a < FACTURA.GetDetalle().Count; a++) 
                {
                    if (FACTURA.GetDetalle()[a].ACCESSKEY == Request["k"].ToString()) 
                    {
                        hf_Scroll.Value = "1";
                        hf_ScrollControl.Value = "#SetCant" + a.ToString();
                    }
                }
            }

            if (Request["setcant"] != null && Request["k"] != null) 
            {
                string value = Request["setcant"];
                string k = Request["k"];
                AddCant(value, k);
               
                
            }

            if (Request["Del"] != null) 
            {
                string k = Request["Del"].ToString();
                Data2.Class.Struct_Factura FACTURA = Session[FacturaSesssionKEY] as Data2.Class.Struct_Factura;
                FACTURA.BorrarDetalle(k);
                Session.Remove(FacturaSesssionKEY);
                Session.Add(FacturaSesssionKEY, FACTURA);
                redirecttome("");
            }

            if (Request["Add"] != null) 
            {
                try 
                {
                    
                    int IdProduct = int.Parse(Request["Add"].ToString());
                    if (Session[FacturaSesssionKEY] != null) 
                    {
                        Data2.Class.Struct_Factura FACTURA = Session[FacturaSesssionKEY] as Data2.Class.Struct_Factura;
                       
                        if (IdProduct!=0){
                            AgregarProducto(IdProduct, FACTURA);
                            
                        }
                    }

                }
                catch (Exception E)
                {
                    Log.ADD("Error Maestro:" + "[" + E.Message + "]\n[" + E.StackTrace + "]" , this);
                }
            }
        }

        void ConfigurarModulo() 
        {
            DotNetNuke.Entities.Tabs.TabController TC = new DotNetNuke.Entities.Tabs.TabController();
            DotNetNuke.Entities.Tabs.TabInfo TI = TC.GetTab(TabId, PortalId);
            MyURL = TI.FullUrl;
            Log.ADD("Configuracion Modulo", this);
            hf_URL.Value = MyURL;

            if (cmb_Vendedor.Items.Count == 0) 
            {
                System.Web.UI.WebControls.ListItem LI = new System.Web.UI.WebControls.ListItem("Ninguno", "0");
                cmb_Vendedor.Items.Add(LI);
                List<Struct_Vendedores> LV = Data2.Class.Struct_Vendedores.GetAllVendedores(UserId);
                if (LV != null) 
                {
                    foreach (Struct_Vendedores V in LV) 
                    {
                        System.Web.UI.WebControls.ListItem _LI = new System.Web.UI.WebControls.ListItem(V.NombreVendedor, V.Id.ToString());
                        cmb_Vendedor.Items.Add(_LI);
                    }
                }
            }

        }

        void ConfigurarControlesTipoFactura(Struct_Factura p_f) 
        {
            if (p_f != null) 
            {
                if (p_f.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA)
                {
                    IVA_FacturaA.Visible = true;
                    IVA_FacturaB.Visible = false;
                }
                else 
                {
                    IVA_FacturaA.Visible = false;
                    IVA_FacturaB.Visible = true;
                }
            }
        }

        void InterpretarFactura()
        {
            Struct_Factura Factura = Session[FacturaSesssionKEY] as Struct_Factura;
            ConfigurarControlesTipoFactura(Factura);
            if (Factura != null) 
            {
                bodyfactura.Controls.Clear();
                Log.ADD("Factura existe en interpretacion", this);
                if (Factura.GetDetalle() != null)
                {
                    List<Struct_DetalleFactura> Detalle = Factura.GetDetalle();

                    HtmlGenericControl _Table = new HtmlGenericControl("Table");
                    _Table.Attributes.Add("cellspacing", "0");
                    _Table.Attributes.Add("border", "1");
                    _Table.Attributes.Add("style", "border-collapse:collapse;width:100%");
                    HtmlGenericControl _Tbody = new HtmlGenericControl("tbody");


                    string[] _Headers = { "Cant.","Descripción","P. Unit.","Total","Elim." };

                    HtmlGenericControl _HeaderRow = new HtmlGenericControl("tr");
                    _HeaderRow.Attributes.Add("Class", "AtroxHeaderGrid");

                    for (int a = 0; a < _Headers.Length; a++) 
                    {
                        HtmlGenericControl _HeaderCell = new HtmlGenericControl("th");
                        _HeaderCell.InnerText = _Headers[a];
                        if (_HeaderCell.InnerText == "Total") 
                        {
                            _HeaderCell.Attributes.Add("id", "column_total");
                        }
                        _HeaderRow.Controls.Add(_HeaderCell);
                        
                    }
                    _Tbody.Controls.Add(_HeaderRow);

                    for (int a = 0; a < Detalle.Count; a++) 
                    {
                        HtmlGenericControl _FacturaRow = new HtmlGenericControl("tr");
                        _FacturaRow.Attributes.Add("Class", "AtroxRowTable");
                        HtmlGenericControl _CellCant = new HtmlGenericControl("td");
                        HtmlGenericControl _CellDescripcion = new HtmlGenericControl("td");
                        HtmlGenericControl _CellPrecio = new HtmlGenericControl("td");
                        HtmlGenericControl _CellTotal = new HtmlGenericControl("td");
                        HtmlGenericControl _CellEliminar = new HtmlGenericControl("td");

                        if (Factura.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA) 
                        {
                            _CellTotal.InnerText = "$ " + Detalle[a].getTotalSinIva().ToString("0.00");
                        }
                        else if (Factura.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaB) 
                        {
                            _CellTotal.InnerText = "$ " + Detalle[a].getTotalConIva().ToString("0.00");
                        }

                        _CellDescripcion.InnerText  = Detalle[a].PRODUCTO.Descripcion;

                        if (Factura.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA)
                        {
                            _CellPrecio.InnerText = "$ " + Detalle[a].getPrecioFinalSinIva().ToString("0.00");
                        }
                        else if (Factura.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaB)
                        {
                            _CellPrecio.InnerText = "$ " + Detalle[a].PRODUCTO.PrecioFinal.ToString("0.00");
                        }
                        



                        _CellEliminar.Attributes.Add("Class", "AtroxDarkLink");

                        _CellPrecio.Attributes.Add("Class", "CeldaImporte");
                        _CellTotal.Attributes.Add("Class", "CeldaImporte");

                        HtmlGenericControl _AEliminar = new HtmlGenericControl("a");
                        _AEliminar.Attributes.Add("href", MyURL + "?Del=" + Detalle[a].ACCESSKEY);
                        _AEliminar.InnerText = "Elim.";
                        _CellEliminar.Controls.Add(_AEliminar);

                        HtmlGenericControl SetCant = new HtmlGenericControl("input");
                        SetCant.Attributes.Add("type", "button");
                        SetCant.Attributes.Add("value", "SetCant");
                        SetCant.Attributes.Add("Class", "FormButton FirstElement LastElement");
                        SetCant.ID = "SetCant" + a.ToString();
                        SetCant.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        SetCant.Attributes.Add("OnClick", "setCant(\"#SetCant" + a.ToString() + "\",\""+ "#TxtCant" + a.ToString() + "\",\"" + Detalle[a].ACCESSKEY + "\")");

                        string _cant;
                        if (Detalle[a].isdec == true)
                        {
                            _cant = Detalle[a].DETALLEDEC.ToString();
                        }
                        else 
                        {
                            _cant = Detalle[a].DETALLEINT.ToString();
                        }
                        
                        HtmlGenericControl TxtCant = new HtmlGenericControl("input");
                        TxtCant.Attributes.Add("type", "text");
                        TxtCant.Attributes.Add("Class", "AtroxTextBoxMount TextBoxCurrency");
                        TxtCant.Attributes.Add("Value", _cant);
                        TxtCant.ID = "TxtCant" + a.ToString();
                        TxtCant.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                        TxtCant.Attributes.Add("onkeyup", "KP(event,\"#SetCant" + a.ToString() + "\",\"#TxtCant" + a.ToString() + "\",\"" + Detalle[a].ACCESSKEY + "\")");
                        

                        _CellCant.Controls.Add(TxtCant);
                        _CellCant.Controls.Add(SetCant);
                        

                        _FacturaRow.Controls.Add(_CellCant);
                        _FacturaRow.Controls.Add(_CellDescripcion);
                        _FacturaRow.Controls.Add(_CellPrecio);
                        _FacturaRow.Controls.Add(_CellTotal);
                        _FacturaRow.Controls.Add(_CellEliminar);

                        _Tbody.Controls.Add(_FacturaRow);


                    }
                    _Table.Controls.Add(_Tbody);
                    bodyfactura.Controls.Add(_Table);

                    //CONSTRUCCION FACTURA A
                    //Construccion Subtotal

                    placeholder_totales.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                    placeholder_totales.Controls.Clear();

                    string fieldstofixed = "";

                    if (Factura.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA) 
                    {
                        decimal total = 0m;
                        string stringLabel = "SubTotal:";
                        string stringTotal = Factura.GetTotalSinIva().ToString("0.00");
                        
                        placeholder_totales.Controls.Add(GetTotalControl("SubTotal",stringLabel,stringTotal));
                        fieldstofixed = fieldstofixed + "SubTotalValue";
                        List<decimal> MisIvas = Factura.GetIvasInscriptos();
                        total = total + Factura.GetTotalSinIva();

                       

                        if (MisIvas != null) 
                        {
                            for (int a=0;a<MisIvas.Count;a++)
                            {
                            decimal totaliva = Factura.GetTotalDeInsceripcionIva(MisIvas[a]); 
                            placeholder_totales.Controls.Add(GetTotalControl("IVAInsc"+a.ToString(), "IVA. Insc: " + MisIvas[a] + " % ", totaliva.ToString("0.00")));
                            fieldstofixed = fieldstofixed + "," + "IVAInsc" + a.ToString() + "Value";
                            total = total + totaliva;
                            }
                        }

                        placeholder_totales.Controls.Add(GetTotalControl("FinalTotal", "Total:", total.ToString("0.00")));
                        fieldstofixed = fieldstofixed + "," + "FinalTotalValue";

                     
                    }

                    if (Factura.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaB) 
                    {
                        placeholder_totales.Controls.Add(GetTotalControl("FinalTotal","Total:", Factura.GetTotalConIvaIncluido().ToString("0.00")));
                        fieldstofixed = fieldstofixed + "FinalTotalValue";
                    }


                    hf_fixedfields.Value = fieldstofixed;






                }    
            }
        }


        HtmlGenericControl GetTotalControl(string p_nameControl, string p_Label, string p_value) 
        {
            HtmlGenericControl SubTotalContainer = new HtmlGenericControl("div");
            HtmlGenericControl SubTotalValue = new HtmlGenericControl("span");
            HtmlGenericControl SubTotalLabel = new HtmlGenericControl("span");
            SubTotalValue.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            SubTotalLabel.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            SubTotalLabel.ID = p_nameControl + "Label";
            SubTotalValue.ID = p_nameControl + "Value";
            SubTotalLabel.InnerText = p_Label;
            SubTotalValue.InnerText = "$ " + p_value;
            SubTotalLabel.Attributes.Add("style", "margin-left:55%");
            SubTotalValue.Attributes.Add("Class", "CeldaImporte TotalPositionFix");
            SubTotalContainer.Controls.Add(SubTotalValue);
            SubTotalContainer.Controls.Add(SubTotalLabel);
            return SubTotalContainer;
        }
    

        protected void Page_Load(object sender, EventArgs e)
        {

            ConfigurarModulo();
            InterpretarRequest();


            

            if (Session[FacturaSesssionKEY] != null)
            {
                InterpretarFactura();
                Log.ADD("La sesion de la factura existe",this);
                _F = Session[FacturaSesssionKEY] as Struct_Factura;
                DatosFactura.Visible = true;
                if (_F.GetDetalle()!=null)
                {
                    div_GuardarFactura.Visible = true;
                }
                else 
                {
                    div_GuardarFactura.Visible = false;
                }
            }
            else 
            {
                Log.ADD("La sesion de la factura NO existe", this);
                _F = null;
                DatosFactura.Visible = false;
                div_GuardarFactura.Visible = false;
            }


            if (!IsPostBack && _F!=null) 
            {
                RecuperarCampos(); 
               
            }


            try
            {

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        void RecuperarCampos()
        {

            txt_Cuit.Text = _F.cuit;
            txt_Domicilio.Text = _F.domicilio;
            txt_Localidad.Text = _F.localidad;
            txt_senores.Text = _F.senores;
            txt_Telefono.Text = _F.telefono;

            if (_F.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA)
            {
                LB_TipoFactura.SelectedIndex = 0;

            }
            if (_F.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaB)
            {
                LB_TipoFactura.SelectedIndex = 1;
            }

            if (_F.Pago == Struct_Factura.CondicionPago.Contado) 
            {
                Chk_Condicion_Venta.SelectedIndex = 0;
            }
            else if (_F.Pago == Struct_Factura.CondicionPago.CtaCte) 
            {
                Chk_Condicion_Venta.SelectedIndex = 1;
            }


            if (_F.Condicion_IVA == Struct_Factura.CondicionIVA.RespInscripto)
            {
                Chk_IVA_A.SelectedIndex = 0;
            }
            else if (_F.Condicion_IVA == Struct_Factura.CondicionIVA.RespNoInscripto)
            {
                Chk_IVA_A.SelectedIndex = 1;
            }
            else if (_F.Condicion_IVA == Struct_Factura.CondicionIVA.Exento)
            {
                Chk_IVA_B.SelectedIndex = 0;
            }
            else if (_F.Condicion_IVA == Struct_Factura.CondicionIVA.ConsumidorFinal)
            {
                Chk_IVA_B.SelectedIndex = 1;
            }
            else if (_F.Condicion_IVA == Struct_Factura.CondicionIVA.RespMonotributo)
            {
                Chk_IVA_B.SelectedIndex = 2;
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

        protected void btn_setearFactura_Click(object sender, EventArgs e)
        {
            if (_F != null) 
            {
                _F.senores = txt_senores.Text;
                _F.domicilio = txt_Domicilio.Text;
                _F.telefono = txt_Telefono.Text;
                _F.localidad = txt_Localidad.Text;
                _F.cuit = txt_Cuit.Text;
                Session.Remove(FacturaSesssionKEY);
                Session.Add(FacturaSesssionKEY, _F);
                redirecttome("");
            }
        }

        protected void LB_TipoFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_F != null)
            {
                if (LB_TipoFactura.SelectedIndex == 0)
                {
                    _F.FacturaTipo = Struct_Factura.TipoDeFactura.FacturaA;
                    Chk_IVA_A.SelectedIndex = 0;
                    _F.Condicion_IVA = Struct_Factura.CondicionIVA.RespInscripto;
                    
                }
                else 
                {
                    _F.FacturaTipo = Struct_Factura.TipoDeFactura.FacturaB;
                    Chk_IVA_B.SelectedIndex = 0;
                    _F.Condicion_IVA = Struct_Factura.CondicionIVA.Exento;
                }
                Session.Remove(FacturaSesssionKEY);
                Session.Add(FacturaSesssionKEY, _F);
                redirecttome("");
            }
        }

        protected void Chk_IVA_B_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_F != null)
            {


                if (Chk_IVA_B.SelectedIndex == 0)
                {
                    _F.Condicion_IVA = Struct_Factura.CondicionIVA.Exento;
                }
                else if (Chk_IVA_B.SelectedIndex == 1)
                {
                    _F.Condicion_IVA = Struct_Factura.CondicionIVA.ConsumidorFinal;
                } else if (Chk_IVA_B.SelectedIndex == 2)
                {
                    _F.Condicion_IVA = Struct_Factura.CondicionIVA.RespMonotributo;
                }
                Session.Remove(FacturaSesssionKEY);
                Session.Add(FacturaSesssionKEY, _F);
                redirecttome("");
            }
        }

        protected void Chk_IVA_A_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_F != null)
            {


                if (Chk_IVA_A.SelectedIndex == 0)
                {
                    _F.Condicion_IVA = Struct_Factura.CondicionIVA.RespInscripto;
                }
                else if (Chk_IVA_A.SelectedIndex == 1)
                {
                    _F.Condicion_IVA = Struct_Factura.CondicionIVA.RespNoInscripto;
                }
                Session.Remove(FacturaSesssionKEY);
                Session.Add(FacturaSesssionKEY, _F);
                redirecttome("");
            }
        }

        protected void Chk_Condicion_Venta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_F != null) 
            {
                if (Chk_Condicion_Venta.SelectedIndex == 0)
                {
                    _F.Pago = Struct_Factura.CondicionPago.Contado;
                }
                else if (Chk_Condicion_Venta.SelectedIndex == 1)
                {
                    _F.Pago = Struct_Factura.CondicionPago.CtaCte;
                }
            }
        }

        protected void GuardarFactura_Click(object sender, EventArgs e)
        {
            int _Vendedor = int.Parse(cmb_Vendedor.SelectedValue);

            if (_F.GuardarFactura(_Vendedor) == true)
            {
                Session.Remove(FacturaSesssionKEY);
                redirecttome("");
            }
            else 
            {

            }

        }

    

       

       

       
    }
}