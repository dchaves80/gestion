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
using Data2;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using DotNetNuke.Services.ClientCapability;


namespace Christoc.Modules.Suppliers
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from SuppliersModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : SuppliersModuleBase, IActionable
    {


        
        void LlenarProvincias() 
        {
            if (cmbProvincia.Items.Count == 0)
            {
                //cmbProvincia.Items.Add(new System.Web.UI.WebControls.ListItem("(Ninguno)", "0"));
                List<Data2.Class.Struct_Provincia> MyProvinciaList = Data2.Class.Struct_Provincia.GetAll();
                if (MyProvinciaList != null)
                {
                    for (int a = 0; a < MyProvinciaList.Count; a++)
                    {
                        cmbProvincia.Items.Add(new System.Web.UI.WebControls.ListItem(MyProvinciaList[a].getName().ToString(), MyProvinciaList[a].getId().ToString()));
                    }
                }
            }
        }

        void LlenarCategorias()
        {
            if (cmbCategoríaAFIP.Items.Count == 0)
            {

                cmbCategoríaAFIP.Items.Add(new System.Web.UI.WebControls.ListItem("(Ninguno)", "0"));
                List<Data2.Class.Struct_CategoriaAFIP> MyCategorias = Data2.Class.Struct_CategoriaAFIP.GetAll();
                if (MyCategorias != null)
                {
                    for (int a = 0; a < MyCategorias.Count; a++)
                    {
                        cmbCategoríaAFIP.Items.Add(new System.Web.UI.WebControls.ListItem(MyCategorias[a].get_Nombre(), MyCategorias[a].get_Id().ToString()));
                    }
                }
            }
        }

        void LlenarTiposDocumentos()
        {
            if (cmbTipoDocumento.Items.Count == 0)
            {

                cmbTipoDocumento.Items.Add(new System.Web.UI.WebControls.ListItem("(Ninguno)", "0"));
                List<Data2.Class.Struct_TipoDocumento> MyCategorias = Data2.Class.Struct_TipoDocumento.GetAll();
                if (MyCategorias != null)
                {
                    for (int a = 0; a < MyCategorias.Count; a++)
                    {
                        cmbTipoDocumento.Items.Add(new System.Web.UI.WebControls.ListItem(MyCategorias[a].get_Nombre(), MyCategorias[a].get_Id().ToString()));
                    }
                }
            }
        }

        private void ScriptController() 
        {
            //OnClientClick=<%# "NewSupplier_" + this.ClientID + "()"%>

            if (btnAgregarProveedor.Attributes["onClick"] == null) 
            {
                btnAgregarProveedor.Attributes.Add("OnClick", "NewSupplier_" + this.ClientID + "(); return false;");
            }

            //OnClientClick=<%# "Cancel_" + this.ClientID + "()"%> 

            if (btnCancelar.Attributes["onClick"] == null)
            {
                btnCancelar.Attributes.Add("OnClick", "Cancel_" + this.ClientID + "(); return false;");
            }




        }


        void FormatGrid() 
        {
            if (SearchGrid.Rows.Count == 0)
            {
                DataTable DT = Data2.Connection.D_Supplier.Get_AllShort(UserId);
                if (DT != null)
                {
                    SearchGrid.DataSource = DT;
                    SearchGrid.DataBind();
                }



                for (int a = 0; a < SearchGrid.Rows.Count; a++)
                {
                    HyperLink HLEdit = new HyperLink();
                    HLEdit.Text = "Editar";
                    HLEdit.NavigateUrl = "/MyManager/Proveedores?edt=" + SearchGrid.Rows[a].Cells[0].Text.ToString();
                    HyperLink HLDelete = new HyperLink();
                    HLDelete.Text = "Borrar";
                    HLDelete.NavigateUrl = "/MyManager/Proveedores?del=" + SearchGrid.Rows[a].Cells[0].Text.ToString();
                    HtmlGenericControl HTMLSeparator = new HtmlGenericControl("span");
                   
                    SearchGrid.Rows[a].Cells[4].Controls.Add(HLEdit);
                    SearchGrid.Rows[a].Cells[4].Controls.Add(HLDelete);
                    SearchGrid.Rows[a].Cells[4].CssClass = "AtroxDarkLink";
                }

                IClientCapability MyCLient = ClientCapabilityProvider.CurrentClientCapability;
                if (MyCLient.IsMobile) 
                {
                    SearchGrid.Columns[3].Visible = false;
                    SearchGrid.Columns[4].Visible = false;
                    for (int a = 0; a < SearchGrid.Rows.Count; a++) 
                    {
                        string t_Telephone = SearchGrid.Rows[a].Cells[2].Text;
                        SearchGrid.Rows[a].Cells[2].Text = "";
                        HyperLink HLTelephone = new HyperLink();
                        HLTelephone.Text = t_Telephone;
                        HLTelephone.NavigateUrl = "tel:" + t_Telephone;
                        SearchGrid.Rows[a].Cells[2].Controls.Add(HLTelephone);

                    }
                }
            }

        }


        

        protected void Page_Load(object sender, EventArgs e)
        {

            LlenarProvincias();
            LlenarCategorias();
            LlenarTiposDocumentos();
            ScriptController();
            FormatGrid();
            CheckURLParameter();
            try
            {

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }



        private void CheckURLParameter()
        {
            if (Request["edt"] != null && ModeField.Value=="None") 
            {
                ModeField.Value = "Edit";
                int t_idSupplier = int.Parse( Request["edt"].ToString());
                Data2.Class.Struct_Supplier t_SP = new Data2.Class.Struct_Supplier(UserId, t_idSupplier);
                if (t_SP.Id != 0) 
                {
                    SupplierIdField.Value = t_SP.Id.ToString();
                    txtNombre.Text = t_SP.Nombre;
                    txtNombreFantasia.Text = t_SP.NombreFantasia;
                    Static.CmbController.SelectValue(cmbPais, t_SP.Pais.ToString());
                    Static.CmbController.SelectValue(cmbProvincia, t_SP.Provincia.ToString());
                    txtLocalidad.Text = t_SP.Localidad;
                    txtDomicilio.Text = t_SP.Domicilio;
                    txtTelefono1.Text = t_SP.Telefono1;
                    txtTelefono2.Text = t_SP.Telefono2;
                    txtMailContacto.Text = t_SP.MailContacto;
                    txtMailPedidos.Text = t_SP.MailPedidos;
                    Static.CmbController.SelectValue(cmbCategoríaAFIP, t_SP.IdCategoriaAfip.ToString());
                    txtIngresosBrutos.Text = t_SP.IngresosBrutos;
                    Static.CmbController.SelectValue(cmbTipoDocumento, t_SP.IdTipoDocumento.ToString());
                    txtNumeroDocumento.Text = t_SP.NroDocumento;
                }


            }

            if (Request["del"] != null) 
            {
                int t_idSupplier = int.Parse(Request["del"].ToString());
                Data2.Class.Struct_Supplier t_SP = new Data2.Class.Struct_Supplier(UserId, t_idSupplier);
                t_SP.Borrar(UserId);
                RedirectToBase();
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if (ModeField.Value == "New")
            {
                Data2.Class.Struct_Supplier t_Supplier = new Data2.Class.Struct_Supplier(
                    UserId, txtNombre.Text,
                    txtNombreFantasia.Text,
                    int.Parse(cmbPais.SelectedValue),
                    int.Parse(cmbProvincia.SelectedValue),
                    txtLocalidad.Text,
                    txtDomicilio.Text,
                    txtTelefono1.Text,
                    txtTelefono2.Text,
                    txtMailContacto.Text,
                    txtMailPedidos.Text,
                    int.Parse(cmbCategoríaAFIP.SelectedValue),
                    txtIngresosBrutos.Text,
                    int.Parse(cmbTipoDocumento.SelectedValue),
                    txtNumeroDocumento.Text);
                t_Supplier.Guardar();

                RedirectToBase();
            }

            if (ModeField.Value == "Edit") 
            {
                
                
                Data2.Class.Struct_Supplier t_SP = new Data2.Class.Struct_Supplier(UserId, int.Parse(SupplierIdField.Value));
                    t_SP.Nombre=txtNombre.Text;
                    t_SP.NombreFantasia=txtNombreFantasia.Text;
                    t_SP.Pais=int.Parse(cmbPais.SelectedValue);
                    t_SP.Provincia=int.Parse(cmbProvincia.SelectedValue);
                    t_SP.Localidad=txtLocalidad.Text;
                    t_SP.Domicilio=txtDomicilio.Text;
                    t_SP.Telefono1 = txtTelefono1.Text;
                    t_SP.Telefono2=txtTelefono2.Text;
                    t_SP.MailContacto=txtMailContacto.Text;
                    t_SP.MailPedidos=txtMailPedidos.Text;
                    t_SP.IdCategoriaAfip=int.Parse(cmbCategoríaAFIP.SelectedValue);
                    t_SP.IngresosBrutos=txtIngresosBrutos.Text;
                    t_SP.IdTipoDocumento=int.Parse(cmbTipoDocumento.SelectedValue);
                    t_SP.NroDocumento=txtNumeroDocumento.Text;
                t_SP.Actualizar(UserId);
                ModeField.Value = "None";
                SupplierIdField.Value = "0";
                RedirectToBase();

            }

         

        }
        void RedirectToBase()
        {
            
            if (Request.RawUrl.Contains("?"))
            {
                string[] splitter = { "?" };
                string newurl = Request.RawUrl.Split(splitter, StringSplitOptions.None)[0];
                Response.Redirect(newurl);
            }
            else 
            {
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}