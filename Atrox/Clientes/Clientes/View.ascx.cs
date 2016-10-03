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
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Christoc.Modules.Clientes
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClientesModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ClientesModuleBase, IActionable
    {

        string KeyIDC = "IDC";

        void ConfigHF() 
        {
            string[] splitter = { "?" };
            HF_Host.Value = "/DesktopModules/Clientes/API/ModuleTask/";
            HF_RawHost.Value = Request.RawUrl.Split(splitter,StringSplitOptions.None)[0];
            Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
            K.Value = SWS.GetPrivateKeyByIdUser(UserId);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                ConfigHF();
                ConfigFields();

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void ConfigFields()
        {
           
            if (cmbprovincia.Items.Count == 0)
            {
                cmbprovincia.Items.Clear();
                List<Data2.Class.Struct_Provincia> LP = Data2.Class.Struct_Provincia.GetAll();
                Data2.Statics.Log.ADD(LP.Count.ToString(), this);
                if (LP != null && LP.Count > 0)
                {
                    for (int a = 0; a < LP.Count; a++)
                    {
                        ListItem LI = new ListItem(LP[a].getName(), LP[a].getName());

                        cmbprovincia.Items.Add(LI);
                    }
                }
            }

            if (Request[KeyIDC] != null && !IsPostBack)
            {
                Session.Remove(KeyIDC);
                Session.Add(KeyIDC, Request[KeyIDC].ToString());

                Data2.Class.Struct_Cliente SC = Data2.Class.Struct_Cliente.GetClient(int.Parse(Session[KeyIDC].ToString()), UserId);
                if (SC != null && !IsPostBack)
                {
                    txt_descuento.Text = SC.DESCUENTO.ToString("#.00");
                    txt_dnicuilcuit.Text = SC.DNI;
                    txt_domicilio.Text = SC.DOMICILIO;
                    txt_email.Text = SC.EMAIL;
                    txt_limite.Text = SC.LIMITEDECREDITO.ToString("#.00");
                    txt_localidad.Text = SC.LOCALIDAD;
                    txt_observaciones.Text = SC.OBSERVACIONES;
                    txt_razonsocial.Text = SC.RS;
                    cmbpais.SelectedValue = SC.PAIS;
                    cmbprovincia.SelectedValue = SC.PROVINCIA;
                    cmbsituacion.SelectedValue = SC.TIPOIVA;
                    HF_EU.Value = SC.ID.ToString();

                }

            }
            else 
            {
                HF_EU.Value = "0";
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

            if (Session[KeyIDC] == null)
            {

                Data2.Class.Struct_Cliente CL = new Data2.Class.Struct_Cliente(
                    txt_razonsocial.Text,
                    txt_dnicuilcuit.Text,
                    cmbpais.SelectedValue,
                    cmbprovincia.SelectedValue,
                    txt_localidad.Text,
                    txt_domicilio.Text,
                    txt_observaciones.Text,
                    cmbsituacion.SelectedValue,
                    Data2.Statics.Conversion.GetDecimal(txt_descuento.Text),
                    txt_email.Text,
                    UserId,
                    Data2.Statics.Conversion.GetDecimal(txt_limite.Text),
                    chk_Suspendida.Checked);
                CL.Guardar();
            }
            else 
            {
                Data2.Class.Struct_Cliente SC = Data2.Class.Struct_Cliente.GetClient(int.Parse(Session[KeyIDC].ToString()), UserId);
                if (SC != null && Request[KeyIDC] != null)
                {
                    SC.DESCUENTO = Data2.Statics.Conversion.GetDecimal(txt_descuento.Text);
                    SC.DNI = txt_dnicuilcuit.Text;
                    SC.DOMICILIO = txt_domicilio.Text;
                    SC.EMAIL = txt_domicilio.Text;
                    SC.LIMITEDECREDITO = Data2.Statics.Conversion.GetDecimal(txt_limite.Text);
                    SC.LOCALIDAD = txt_localidad.Text;
                    SC.OBSERVACIONES = txt_observaciones.Text;
                    SC.PAIS = cmbpais.SelectedValue;
                    SC.PROVINCIA = cmbprovincia.SelectedValue;
                    SC.RS = txt_razonsocial.Text;
                    SC.TIPOIVA = cmbsituacion.SelectedValue;
                    SC.Guardar();
                    Session.Remove(KeyIDC);
                    Response.Redirect(Request.RawUrl.Split('?')[0]);
                }
                else 
                {
                    Session.Remove(KeyIDC);
                    HF_EU.Value = "0";
                    Response.Redirect(Request.RawUrl.Split('?')[0]);

                }
            }

        }
    }
}