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
using System.Collections.Generic;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using System.Web.UI.HtmlControls;
using Data2.Class;

namespace Christoc.Modules.ListadoFacturas
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ListadoFacturasModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ListadoFacturasModuleBase, IActionable
    {

        string sessionkey = "SearchFacturas";

        HtmlGenericControl GenerarFilaPieFactura(string p_label, string p_value) 
        {
            HtmlGenericControl _row = new HtmlGenericControl("tr");
            HtmlGenericControl _label = new HtmlGenericControl("td");
            HtmlGenericControl _value = new HtmlGenericControl("td");
            _row.Attributes.Add("Class","colorpiedefactura");
            _label.Attributes.Add("colspan", "3");
            _label.InnerText = p_label;
            _label.Attributes.Add("Class", "valuepiedefactura labelpiedefactura");
            _value.InnerText = "$ " + p_value;
            _value.Attributes.Add("Class", "valuepiedefactura");
           
            _row.Controls.Add(_label);
            _row.Controls.Add(_value);
            return _row;
        }

        HtmlGenericControl GenerarFilaDetalle(string cant, string detalle, string importe, string total, bool alternatecolor) 
        {
            string classrow;
            if (alternatecolor) { classrow = "metroparline animationline"; } else { classrow = "metroimparline animationline"; }
            HtmlGenericControl _row = new HtmlGenericControl("tr");
            HtmlGenericControl _cant = new HtmlGenericControl("td");
            HtmlGenericControl _desc = new HtmlGenericControl("td");
            HtmlGenericControl _precu = new HtmlGenericControl("td");
            HtmlGenericControl _total = new HtmlGenericControl("td");
            _desc.Attributes.Add("Class", "descripcionarticulo");
            _cant.Attributes.Add("class", "valuepiedefactura");
            _precu.Attributes.Add("class", "valuepiedefactura");
            _total.Attributes.Add("class", "valuepiedefactura");
            _cant.InnerText = cant.ToString();
            _desc.InnerText = detalle;
            _precu.InnerText = "$ " + importe.ToString();
            _total.InnerText = "$ " + total;
            _row.Attributes.Add("Class", classrow);
            _row.Controls.Add(_cant);
            _row.Controls.Add(_desc);
            _row.Controls.Add(_precu);
            _row.Controls.Add(_total);
            return _row;
        }

        void LlenarFactura(int facturaid) 
        {
            Struct_Factura _F = Struct_Factura.GetFacturaById(UserId, facturaid);
            if (_F != null) 
            {
                field_date.InnerText = _F.Fecha.ToLongDateString();
                field_domi.InnerText = _F.domicilio;
                field_iva.InnerText = _F.Condicion_IVA.ToString();
                field_name.InnerText = _F.senores;
                field_pay.InnerText = _F.Pago.ToString();
                field_phone.InnerText = _F.telefono;

                List<Struct_DetalleFactura> List_detalle = _F.GetDetalle();
                if (List_detalle != null && List_detalle.Count > 0) 
                {
                    int renglones = 1;
                    bool alternate = false;
                    foreach (Struct_DetalleFactura D in List_detalle) 
                    {
                        string _cantidad;
                        string _preciou;
                        string _total;

                        if (D.isdec) {_cantidad = D.DETALLEDEC.ToString("#.00");} else {_cantidad=D.DETALLEINT.ToString();}
                        if (_F.FacturaTipo==Struct_Factura.TipoDeFactura.FacturaA) {_preciou=D.getPrecioFinalSinIva().ToString("#.00");} else {_preciou=D.PRODUCTO.PrecioFinal.ToString("#.00");}
                        if (_F.FacturaTipo==Struct_Factura.TipoDeFactura.FacturaA){_total = D.getTotalSinIva().ToString("#.00");} else {_total = D.getTotalConIva().ToString("#.00");}

                        HtmlGenericControl _HTMLCONTROL = GenerarFilaDetalle(_cantidad,D.PRODUCTO.Descripcion,_preciou,_total,alternate);
                        Table_detail.Controls.AddAt(renglones, _HTMLCONTROL);
                        if (alternate) { alternate = false; } else { alternate = true; }
                        
                        renglones++;//Para que los meta en orden correspondiente
                    }
                }

                if (_F.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA)
                {

                    Table_detail.Controls.Add(GenerarFilaPieFactura("Sub Total:", _F.subtotal.ToString("#.00")));

                    foreach (decimal DEC in _F.GetIvasInscriptos())
                    {
                        decimal _CurrentIva = _F.GetTotalDeInsceripcionIva(DEC);
                        Table_detail.Controls.Add(GenerarFilaPieFactura("IVA Insc: " + DEC.ToString("#.00") + "%", _CurrentIva.ToString("#.00")));
                    }
                }

                Table_detail.Controls.Add(GenerarFilaPieFactura("Total:",_F.total.ToString("#.00")));

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] split = {"?"};
            urlbase.Value = Request.RawUrl.Split(split,StringSplitOptions.None)[0]; 
            try
            {

                if (Request["VC"] != null) 
                {
                    LlenarFactura(int.Parse(Request["VC"].ToString()));
                }

                if (Session[sessionkey] != null) 
                {
                    BuildSearch();
                }

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
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


        void BuildSearch()
        {
            ListadoFacturas.Controls.Clear();
            if (Session[sessionkey] != null)
            {

                List<Data2.Class.Struct_Factura> _LF = Session[sessionkey] as List<Data2.Class.Struct_Factura>;

                foreach (Data2.Class.Struct_Factura F in _LF)
                {
                    HtmlGenericControl ParentDiv = new HtmlGenericControl("Div");
                    HtmlGenericControl ParentTable = new HtmlGenericControl("Table");
                    HtmlGenericControl ParentRow = new HtmlGenericControl("tr");
                    HtmlGenericControl ColumnLetter = new HtmlGenericControl("td");
                    HtmlGenericControl ColumnInfo = new HtmlGenericControl("td");
                    HtmlGenericControl Letter = new HtmlGenericControl("Div");
                    HtmlGenericControl Info = new HtmlGenericControl("Div");




                    string facturatipo = "";
                    string infofactura;

                    if (F.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA) facturatipo = "A";
                    if (F.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaB) facturatipo = "B";
                    if (F.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaC) facturatipo = "C";
                    Letter.Attributes.Add("Class", "LetraFactura");
                    string total = F.total.ToString("#.00");
                    string[] splitters = { ".", "," };
                    string strentero = "00";
                    string strdecimal = "00";
                    try
                    {
                        strentero = total.Split(splitters, StringSplitOptions.None)[0];
                    }
                    catch { }
                    try
                    {
                        strdecimal = total.Split(splitters, StringSplitOptions.None)[1];
                    }
                    catch { }

                    

                    infofactura = "<b>Fecha:</b> " +
                        F.Fecha.ToShortDateString() +
                        "<br/><b>Total:</b> " +
                        strentero + "<sup>" + strdecimal + "</sup>"+
                        "<br/><b>IID: </b>" + F.Id.ToString();

                    if (F.FacturaTipo == Struct_Factura.TipoDeFactura.FacturaA) 
                    {
                        infofactura += "</br><b>Razon social: </b>" + F.senores;
                    }

                    Letter.InnerText = facturatipo;
                    Info.InnerHtml = infofactura;
                    Info.Attributes.Add("Class", "InfoFactura");
                    ParentDiv.Attributes.Add("Class", "ContainerFactura");
                    ParentDiv.Attributes.Add("OnClick", "OpenC("+F.Id.ToString()+")");
                    


                    ParentDiv.Controls.Add(ParentTable);
                    ParentTable.Controls.Add(ParentRow);
                    ParentRow.Controls.Add(ColumnLetter);
                    ParentRow.Controls.Add(ColumnInfo);
                    ColumnLetter.Controls.Add(Letter);
                    ColumnInfo.Controls.Add(Info);
                    ListadoFacturas.Controls.Add(ParentDiv);
                }
            }
        
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime Start = CalendarDesde.SelectedDate;
            DateTime End = CalendarHasta.SelectedDate;
            Start = Start.AddHours(-Start.Hour);
            End = End.AddHours(24 - End.Hour);
            
            Data2.Class.Struct_Factura.TipoDeFactura TF = Data2.Class.Struct_Factura.TipoDeFactura.Null;
            
            if (cmb_TipoComprobante.SelectedValue=="0") TF = Data2.Class.Struct_Factura.TipoDeFactura.Null;
            if (cmb_TipoComprobante.SelectedValue=="A") TF = Data2.Class.Struct_Factura.TipoDeFactura.FacturaA;
            if (cmb_TipoComprobante.SelectedValue=="B") TF = Data2.Class.Struct_Factura.TipoDeFactura.FacturaB;
            if (cmb_TipoComprobante.SelectedValue == "C") TF = Data2.Class.Struct_Factura.TipoDeFactura.FacturaC;
            

            List<Data2.Class.Struct_Factura> _LF = Data2.Class.Struct_Factura.GetFacturasBetweenDates(Start, End, UserId, false, TF);


            if (_LF != null && _LF.Count > 0) 
            {
                if (Session != null) 
                {
                    Session.Remove(sessionkey);
                }
                Session.Add(sessionkey, _LF);
                BuildSearch();
            }



        }
    }
}