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

namespace Christoc.Modules.Facturacion
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from FacturacionModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : FacturacionModuleBase, IActionable
    {

        string FacturaSesssionKEY = "FacturaSessionKey";
        string GrillaBusquedaSession = "GrillaBusqueda";
        string MyURL = "/MyManager/Facturacion";
        bool modulofacturacion = false;
      

        void ComprobarFactura() 
        {
            if (Session[FacturaSesssionKEY] != null)
            {
                btnCrearFactura.Visible = false;
                MenuBusqueda.Visible = true;
            }
            else 
            {
                btnBorrarFactura.Visible = false;
                MenuBusqueda.Visible = false;
            }
        }

        void ArmarGrilla() 
        {
            if (Session[GrillaBusquedaSession]!=null) 
            {
                
                Data2.Class.Struct_Producto.ProductDATASET PDS = Session[GrillaBusquedaSession] as Data2.Class.Struct_Producto.ProductDATASET;

                if (PDS.Listado.Count > 0)
                {
                    HF_results.Value = "1";
                    tableBody.Controls.Clear();



                    HtmlGenericControl _table = new HtmlGenericControl("table");
                    _table.Attributes.Add("cellspacing", "0");
                    _table.Attributes.Add("border", "1");
                    _table.Attributes.Add("style", "border-collapse:collapse;width:100%");
                    HtmlGenericControl _tbody = new HtmlGenericControl("tbody");

                    HtmlGenericControl _trHeader = new HtmlGenericControl("tr");
                    _trHeader.Attributes.Add("class", "AtroxHeaderGrid");

                    string[] tableHeaders = { "0" };

                    if (modulofacturacion == true)
                    {
                        string[] H = { "Codigo", "Descripcion", "Stock", "Precio de Venta", "Agregar" };
                        tableHeaders = H;
                    }
                    else
                    {
                        string[] H = { "Codigo", "Descripcion", "Stock", "Precio de Venta" };
                        tableHeaders = H;
                    }

                    for (int a = 0; a < tableHeaders.Length; a++)
                    {
                        HtmlGenericControl _th = new HtmlGenericControl("th");
                        _th.InnerText = tableHeaders[a];
                        _trHeader.Controls.Add(_th);
                    }

                    _tbody.Controls.Add(_trHeader);
                    _table.Controls.Add(_tbody);
                    tableBody.Controls.Add(_table);

                    for (int a = 0; a < PDS.Listado.Count; a++)
                    {
                        HtmlGenericControl _Tr = new HtmlGenericControl("tr");
                        _Tr.Attributes.Add("class", "AtroxRowTable");

                        HtmlGenericControl Cell_Codigo = new HtmlGenericControl("td");
                        Cell_Codigo.InnerText = PDS.Listado[a].CodigoInterno;

                        HtmlGenericControl Cell_Descripcion = new HtmlGenericControl("td");
                        Cell_Descripcion.InnerText = PDS.Listado[a].Descripcion;

                        HtmlGenericControl Cell_Stock = new HtmlGenericControl("td");

                        Data2.Class.Struct_Unidades U = new Data2.Class.Struct_Unidades(PDS.Listado[a].IdUnidad);
                        if (U.Decimal == true)
                        {
                            Cell_Stock.InnerText = PDS.Listado[a].CantidadDEC.ToString() + " " + U.Simbolo;
                        }
                        else
                        {
                            Cell_Stock.InnerText = PDS.Listado[a].CantidadINT.ToString() + " " + U.Simbolo;
                        }

                        HtmlGenericControl Cell_PV = new HtmlGenericControl("td");
                        Cell_PV.InnerText = "$ " + PDS.Listado[a].PrecioFinal.ToString();

                        HtmlGenericControl Cell_Agregar = new HtmlGenericControl("td");
                        Cell_Agregar.Attributes.Add("class", "AtroxDarkLink");
                        HtmlGenericControl Agregar_Link = new HtmlGenericControl("a");
                        Agregar_Link.Attributes.Add("href", MyURL + "?Add=" + PDS.Listado[a].Id.ToString());
                        Agregar_Link.InnerText = "Agregar a factura";
                        Cell_Agregar.Controls.Add(Agregar_Link);



                        _Tr.Controls.Add(Cell_Codigo);
                        _Tr.Controls.Add(Cell_Descripcion);
                        _Tr.Controls.Add(Cell_Stock);
                        _Tr.Controls.Add(Cell_PV);

                        if (modulofacturacion == true)
                        {
                            _Tr.Controls.Add(Cell_Agregar);
                        }

                        _tbody.Controls.Add(_Tr);


                    }
                }
                else 
                {
                    HF_results.Value = "0";
                }

            }
        }

        private void ConfigurarModulo() 
        {
            tableBody.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            DotNetNuke.Entities.Tabs.TabController TC = new DotNetNuke.Entities.Tabs.TabController();
            DotNetNuke.Entities.Tabs.TabInfo TI = TC.GetTab(TabId, PortalId);
            MyURL = TI.FullUrl;
            for (int a = 0; a < TI.Modules.Count; a++)
            {
                DotNetNuke.Entities.Modules.ModuleInfo MyModule = TI.Modules[a] as ModuleInfo;
                if (MyModule.ModuleTitle == "Factura2") 
                {
                    modulofacturacion = true;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigurarModulo();
            ComprobarFactura();
            ArmarGrilla();
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

        void redirecttome(string parameters) 
        {
            if (parameters == "")
            {
                Response.Redirect(MyURL);
            }
            else 
            {
                Response.Redirect(MyURL + parameters);
            }
        }

        protected void btnCrearFactura_Click(object sender, EventArgs e)
        {
            Session.Add(FacturaSesssionKEY, new Data2.Class.Struct_Factura(UserId));
            
            redirecttome(null);

        }

        protected void btnBorrarFactura_Click(object sender, EventArgs e)
        {
            if (Session[FacturaSesssionKEY] != null) 
            {
                Session.Remove(FacturaSesssionKEY);
                Session.Remove(GrillaBusquedaSession);
                redirecttome(null);
            }
        }


        
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
           Data2.Class.Struct_Producto.ProductDATASET PDS;

           if (SearchOption.SelectedValue == "1")
           {
               PDS = Data2.Class.Struct_Producto.Get_ArticleFORSTOCK(UserId, 0, "%", txtBuscar.Text);
           }
           else 
           {
               PDS = Data2.Class.Struct_Producto.Get_ArticleFORSTOCK(UserId, 0, txtBuscar.Text, "%");
           }


           if (PDS != null) 
           {
               if (PDS.Listado != null && PDS.Listado.Count != 0) 
               {
                   if (Session[GrillaBusquedaSession] != null) 
                   {
                       Session.Remove(GrillaBusquedaSession);
                   }
                   Session.Add(GrillaBusquedaSession,PDS);
               }
           }
           redirecttome(null);

        }

       
    }
}