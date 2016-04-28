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

        protected void Page_Load(object sender, EventArgs e)
        {

            LlenarProvincias();
            LlenarCategorias();
            LlenarTiposDocumentos();
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
    }
}