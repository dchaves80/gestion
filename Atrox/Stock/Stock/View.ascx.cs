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
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Christoc.Modules.Stock
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from StockModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : StockModuleBase, IActionable
    {

        string sessionname = "KeeySearchStock";

        private void CargarProveedores() 
        {
            if (cmbProveedores.Items.Count == 0) 
            {
                cmbProveedores.Items.Add(new System.Web.UI.WebControls.ListItem("[Todos]","0"));
                DataTable DT = Data2.Connection.D_Supplier.Get_AllNames(UserId);
                if (DT != null)
                {
                    for (int a = 0; a < DT.Rows.Count; a++)
                    {
                        string t_id = DT.Rows[a]["Id"].ToString();
                        string t_Nombre = DT.Rows[a]["Nombre"].ToString();
                        string t_NombreFantasia = DT.Rows[a]["NombreFantasía"].ToString();
                        ListItem t_LI = new ListItem(t_Nombre + "|" + t_NombreFantasia, t_id);
                        ListItem t_LIupdate = new ListItem(t_Nombre + "|" + t_NombreFantasia, t_id);
                        cmbProveedores.Items.Add(t_LI);
                    }
                }
                
            }
        }

        

        private void  LoadSearchConfiguration()
        {
            if (Session[sessionname] != null)
            {
                string[] splitter = { "-" };
                bool ByCode = false;
                bool ByName = false;
                int Provider = 0;
                string[] ValuesStore = Session[sessionname].ToString().Split(splitter, StringSplitOptions.None);
                ByCode = (ValuesStore[1].ToLower() == "true") ? true : false;
                ByName = (ValuesStore[2].ToLower() == "true") ? true : false;
                Provider = int.Parse(ValuesStore[0].ToString());
                for (int a = 0; a < cmbProveedores.Items.Count; a++)
                {
                    if (cmbProveedores.Items[a].Value == Provider.ToString())
                    {
                        cmbProveedores.SelectedIndex = a;
                        break;
                    }
                }

                CHKbyCode.Selected = ByCode;
                CHKbyName.Selected = ByName;
                chkParameters.Checked = true;
            }
            else 
            {
                chkParameters.Checked = false;
            }
        }


        

        protected void Page_Load(object sender, EventArgs e)
        {

            CargarProveedores();

            KEY.Value = new Data2.Connection.D_StaticWebService().GetPrivateKeyByIdUser(UserId).ToString();

            if (!IsPostBack) 
            {
                LoadSearchConfiguration();
            }

            try
            {

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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LoadSearchConfiguration();
            Data2.Class.Struct_Producto.ProductDATASET PDS=null;
            DivMessage.Controls.Clear();
            if (txtBuscar.Text.Length > 2)
            {

                if (CHKbyCode.Selected)
                {
                    PDS = Data2.Class.Struct_Producto.Get_ArticleFORSTOCK(UserId, int.Parse(cmbProveedores.SelectedValue), txtBuscar.Text, "%");
                }
                else
                {
                    PDS = Data2.Class.Struct_Producto.Get_ArticleFORSTOCK(UserId, int.Parse(cmbProveedores.SelectedValue), "%", txtBuscar.Text);
                }

                if (PDS != null)
                {
                    ArmarGrilla(PDS);
                }
            }
            else 
            {
                
                Data2.Statics.ConstAlert.AddMessage("Utilice al menos dos o más caracteres en el campo de busqueda", Data2.Statics.ConstAlert.TypeMessage.Alert,DivMessage);

            }

        }

        private void ArmarGrilla(Data2.Class.Struct_Producto.ProductDATASET p_PDS) 
        {
            ListadoArticulos.Controls.Clear();

            HtmlGenericControl MyTable = new HtmlGenericControl("table");
            MyTable.Attributes.Add("cellspacing", "0");
            MyTable.Attributes.Add("border", "1");
            MyTable.Attributes.Add("style", "border-collapse:collapse;");
            MyTable.Attributes.Add("class", "AtroxTableGrid");
            HtmlGenericControl MyTbody = new HtmlGenericControl("tbody");
            HtmlGenericControl MyRowHeader = new HtmlGenericControl("tr");
            MyRowHeader.Attributes.Add("class", "AtroxHeaderGrid");
            MyTbody.Controls.Add(MyRowHeader);
            MyTable.Controls.Add(MyTbody);

            Data2.Statics.Log.ADD("Resgistros finales:" + p_PDS.Listado.Count.ToString(),this);

            string[] Cabeceras = { "Codigo", "Descripcion", "Proveedor", "Cantidad", "Precio Venta", "Setear", "ESTADO" };



            for (int a = 0; a < Cabeceras.Length; a++)
            {
                HtmlGenericControl HTMLGENERIC = new HtmlGenericControl("th");
                HTMLGENERIC.InnerText = Cabeceras[a];
                MyRowHeader.Controls.Add(HTMLGENERIC);
            }
            Data2.Statics.Log.ADD("Resgistros finales2:" + p_PDS.Listado.Count.ToString(), this);
            ListadoArticulos.Controls.Add(MyTable);


            if (p_PDS != null)
            {

                List<Data2.Class.Struct_Unidades> UnitsList = Data2.Class.Struct_Unidades.GetAll();



                
                for (int a = 0; a < p_PDS.Listado.Count; a++)
                {

                    Data2.Statics.Log.ADD(a.ToString(), this);

                    HtmlGenericControl _TR = new HtmlGenericControl("tr");
                    _TR.Attributes.Add("class", "AtroxRowTable");

                    HtmlGenericControl _TDCodigo = new HtmlGenericControl("td");
                    HtmlGenericControl _TDDescripcion = new HtmlGenericControl("td");
                    HtmlGenericControl _TDProveedor = new HtmlGenericControl("td");
                    HtmlGenericControl _TDCantidad = new HtmlGenericControl("td");
                    _TDCantidad.Attributes.Add("Id", "CantCell" + a.ToString());
                    HtmlGenericControl _TDPrecioVenta = new HtmlGenericControl("td");
                    HtmlGenericControl _TDEditarBorrar = new HtmlGenericControl("td");
                    HtmlGenericControl _TDESTADO = new HtmlGenericControl("td");
                    _TDESTADO.Attributes.Add("style", "Width:150px;");

                    
                    
                        _TDCodigo.InnerText = p_PDS.Listado[a].CodigoInterno;
                        _TDDescripcion.InnerText = p_PDS.Listado[a].Descripcion;
                        _TDProveedor.InnerText = p_PDS.MyDATATABLE.Rows[a]["NombreProveedor"].ToString();

                    
                    
                    Data2.Class.Struct_Unidades U = new Data2.Class.Struct_Unidades(p_PDS.Listado[a].IdUnidad);
                    

                        if (U.Decimal==false)
                        {
                            _TDCantidad.InnerText = p_PDS.Listado[a].CantidadINT.ToString();
                        }
                        else 
                        {
                            _TDCantidad.InnerText = p_PDS.Listado[a].CantidadDEC.ToString();
                        }
                        
                        _TDPrecioVenta.InnerText = p_PDS.Listado[a].PrecioFinal.ToString();



                        HtmlGenericControl OkStatus = new HtmlGenericControl("div");
                        OkStatus.Attributes.Add("Id", "Ok" + a.ToString());
                        OkStatus.Attributes.Add("Class", "OKStatus");
                        OkStatus.InnerText="Cambio Guardado";

                        HtmlGenericControl ErrorStatus = new HtmlGenericControl("div");
                        ErrorStatus.Attributes.Add("Id", "Error" + a.ToString());
                        ErrorStatus.Attributes.Add("Class", "ErrorStatus");
                        ErrorStatus.InnerText = "Error...";


                        _TDESTADO.Controls.Add(OkStatus);

                        _TDESTADO.Controls.Add(ErrorStatus);

                    HtmlGenericControl TextBOX = new HtmlGenericControl("input");
                    TextBOX.Attributes.Add("type", "text");
                    TextBOX.Attributes.Add("ID", "TB" + a.ToString());
                    TextBOX.Attributes.Add("Class", "AtroxTextBoxMount");
                    _TDEditarBorrar.Controls.Add(TextBOX);

                    if (UnitsList != null && UnitsList.Count > 0)
                    {
                        
                        HtmlGenericControl ListBox = new HtmlGenericControl("select");
                        ListBox.Attributes.Add("ID", "SL" + a.ToString());
                        int hi = 0;
                        for (int b = 0; b < UnitsList.Count; b++) 
                        {
                            hi = b;
                            HtmlGenericControl OPT = new HtmlGenericControl("option");
                            OPT.Attributes.Add("value", UnitsList[b].Id.ToString());
                            
                            

                            OPT.InnerText = UnitsList[b].Nombre;
                            if (p_PDS.Listado[a].IdUnidad == UnitsList[b].Id) 
                            {
                                OPT.Attributes.Add("selected", "");
                            }
                            ListBox.Controls.Add(OPT);
                        }



                        _TDEditarBorrar.Controls.Add(ListBox);
                    }
                    


                    HtmlGenericControl BUTTON = new HtmlGenericControl("input");
                    BUTTON.Attributes.Add("type", "Button");
                    BUTTON.Attributes.Add("value", "Setear");
                    BUTTON.Attributes.Add("class", "FormButton FirstElement LastElement");
                    BUTTON.Attributes.Add("onclick", "SetearCantidad("+UserId.ToString()+","+p_PDS.Listado[a].Id.ToString()+",\""+a.ToString() +"\")");
                    _TDEditarBorrar.Controls.Add(BUTTON);




                    _TR.Controls.Add(_TDCodigo);
                    _TR.Controls.Add(_TDDescripcion);
                    _TR.Controls.Add(_TDProveedor);
                    _TR.Controls.Add(_TDCantidad);
                    _TR.Controls.Add(_TDPrecioVenta);
                    _TR.Controls.Add(_TDEditarBorrar);
                    _TR.Controls.Add(_TDESTADO);

                    MyTbody.Controls.Add(_TR);



                }
            }


        }

        protected void chkParameters_CheckedChanged(object sender, EventArgs e)
        {
            


            if (chkParameters.Checked == true)
            {

                bool ByCode = false;
                bool ByName = false;
                int Providervalue = 0;

                ByCode = CHKbyCode.Selected;
                ByName = CHKbyName.Selected;
                Providervalue = int.Parse(cmbProveedores.SelectedValue);

                if (Session[sessionname] == null)
                {
                    Session.Add(sessionname, Providervalue.ToString() + "-" + ByCode.ToString() + "-" + ByName.ToString());
                    Response.Redirect("~/MyManager/ControlDeStock");
                   
                }
                else
                {
                    Session[sessionname] = Providervalue.ToString() + "-" + ByCode.ToString() + "-" + ByName.ToString();
                    Response.Redirect("~/MyManager/ControlDeStock");
                }
            }
            else 
            {
                if (Session[sessionname] == null)
                {
                    Response.Redirect("~/MyManager/ControlDeStock");
                }
                else
                {
                    Session.Remove(sessionname);
                    Response.Redirect("~/MyManager/ControlDeStock");
                }
            }
        }
    }
}