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
using System.IO;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;

namespace Christoc.Modules.ConfiguracionesDeCuenta
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ConfiguracionesDeCuentaModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ConfiguracionesDeCuentaModuleBase, IActionable
    {

        Struct_PrintConfiguration PC = null;

        void LlenarCamposPrinter() 
        {
            

            if (PC != null) 
            {
                txtBaudios.Text = PC.Baudios.ToString();
                
            }



            for (int a = 0; a < cmbPrintersModels.Items.Count;a++ )
            {
                if (PC!=null && cmbPrintersModels.Items[a].Text == PC.Modelo)
                {
                    cmbPrintersModels.SelectedIndex = a;
                    break;
                }
            }

            for (int a = 0; a < cmbPuerto.Items.Count; a++)
            {
                if (PC != null && cmbPuerto.Items[a].Text == PC.Puerto)
                {
                    cmbPuerto.SelectedIndex = a;
                    break;
                }
            }

        }

        void LlenarCamposVendedores() 
        {
            cmb_ListadoVendedores.Items.Clear();
            List<Struct_Vendedores> _LV  = Data2.Class.Struct_Vendedores.GetAllVendedores(UserId);
            if (_LV != null)
            {
                for (int a = 0; a < _LV.Count; a++)
                {
                    ListItem LI = new ListItem(_LV[a].NombreVendedor + " [" + _LV[a].Porcentaje.ToString("#.##") + "%]", _LV[a].Id.ToString());
                    cmb_ListadoVendedores.Items.Add(LI);
                }

            }
            else 
            {
                cmb_ListadoVendedores.Enabled = false;
            }

        }

        void LlenarCamposUserConfig() 
        {
            


            Data2.Class.Struct_UserConfig UC = Data2.Class.Struct_UserConfig.getUserConfig(UserId);
            if (UC != null)
            {
                MensajeConfigUsuario.Visible = false;
                txt_NombreNegocio.Text = UC.NombreNegocio;
                chk_MostrarLogoNegocio.Checked = UC.MostrarLogoNegocio;
                chk_HabilitarKiosco.Checked = UC.MostrarKiosco;
                for (int a = 0; a < cmb_FacturaPorDefecto.Items.Count; a++) 
                {
                    if (cmb_FacturaPorDefecto.Items[a].Value == UC.FacturaPorDefecto) 
                    {
                        cmb_FacturaPorDefecto.SelectedIndex = a;
                        break;
                    }
                }

            }
            else 
            {
                MensajeConfigUsuario.Visible = true;
                MensajeConfigUsuario.InnerText = "No exiten configuraciones del usuario";
            }
        }

        void LlenarIMGLogo() 
        {
            try
            {

                //http://190.105.214.230/Portals/0/UsersConfig/1/Logo.gif
                //Direcotory = PortalSettings.HomeDirectoryMapPath + "\\" + "UsersConfig\\" + UserId.ToString();
                string DIR = PortalSettings.HomeDirectoryMapPath + "\\" + "UsersConfig\\" + UserId.ToString();
                string[] FilesList = Directory.GetFiles(DIR, "Logo.*");
                string extension = "";
                if (FilesList.Length == 1)
                {
                    string[] splitter = { "." };
                    extension = FilesList[0].Split(splitter, StringSplitOptions.None)[1];
                }
                img_logo.ImageUrl = "~/Portals/" + PortalId.ToString() + "/UsersConfig/" + UserId.ToString() + "/Logo." + extension + "?preventcache=" + DateTime.Now.Millisecond.ToString();
            } catch{}
        }


        void BorrarVendedor(string p_ven) 
        {
            int idven = int.Parse(p_ven);
            List<Struct_Vendedores> LV = Data2.Class.Struct_Vendedores.GetAllVendedores(UserId);
            if (LV != null)
            {
                for (int a = 0; a < LV.Count; a++)
                {
                    if (LV[a].Id == idven)
                    {
                        LV[a].Delete();
                    }

                   
                }
            }
            string newurl = Request.RawUrl;
            if (newurl.Contains("?"))
            {
                newurl = newurl.Split('?')[0];
            }

            Response.Redirect(newurl);

        }

        void EdcVen() 
        {
            int idVen = int.Parse(Request["EdcVen"]);
            decimal porc = Data2.Statics.Conversion.GetDecimal(Request["PR"]);
            string name = Request["NV"];
            bool changename = false;
            bool changeporc = false;
            if (Request["PR"] != "")
            {
                changeporc = true;
            }
            if (Request["NV"] != "")
            {
                changename = true;
            }






            List<Data2.Class.Struct_Vendedores> LV = Data2.Class.Struct_Vendedores.GetAllVendedores(UserId);

            if (LV != null)
            {
                foreach (Data2.Class.Struct_Vendedores V in LV)
                {
                    if (V.Id == idVen)
                    {
                        if (changename) V.NombreVendedor = name;
                        if (changeporc) V.Porcentaje = porc;
                        V.Modify();
                        break;
                    }
                }
                
            }
            string newurl = Request.RawUrl;
            if (newurl.Contains("?"))
            {
                newurl = newurl.Split('?')[0];
            }
            Response.Redirect(newurl);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            


            try
            {

                string hostname = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/");
                hostn.Value = hostname;

                

                Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
                KEY.Value = SWS.GetPrivateKeyByIdUser(UserId);
              
                    PC = Data2.Class.Struct_PrintConfiguration.GetPrintConfiguration(UserId);
                    
                    if (!IsPostBack) 
                    {
                        if (Request["EdcVen"] != null 
                            && Request["NV"] != null 
                            && Request["PR"] != null) 
                        {
                            EdcVen();
                        }

                        if (Request["DelVen"] != null) 
                        {
                            BorrarVendedor(Request["DelVen"]);
                        }

                        if (Request["EdtVen"] != null)
                        {
                            
                            
                            int idVen = int.Parse(Request["EdtVen"]);

                            List<Data2.Class.Struct_Vendedores> LV = Data2.Class.Struct_Vendedores.GetAllVendedores(UserId);

                            if (LV!=null)
                            {
                                foreach (Data2.Class.Struct_Vendedores V in LV)
                                {
                                    if (V.Id==idVen)
                                    {
                                        txt_EdcNombre.Attributes.Add("placeholder",V.NombreVendedor);
                                        txt_EdcPorcentaje.Attributes.Add("placeholder",V.Porcentaje.ToString("#.##"));
                                        idEdition.Value = idVen.ToString();
                                    }
                                }
                            }

                        }
                        else 
                        {
                            idEdition.Value = "0";
                        }

                        LlenarCamposPrinter();
                        
                        LlenarCamposUserConfig();
                        
                    }
                    LlenarIMGLogo();
                    LlenarCamposVendedores();
              
                   

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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (PC == null)
            {

                PC = new Struct_PrintConfiguration(UserId, cmbPuerto.SelectedItem.Text, cmbPrintersModels.SelectedItem.Text, int.Parse(txtBaudios.Text));
            }
            else 
            {
                PC.Baudios = int.Parse(txtBaudios.Text);
                PC.Modelo = cmbPrintersModels.SelectedItem.Text;
                PC.Puerto = cmbPuerto.SelectedItem.Text;
                
            }
            PC.Guardar();
        }

        protected void btnGuardarConfigNegocio_Click(object sender, EventArgs e)
        {
            Data2.Class.Struct_UserConfig UC = Data2.Class.Struct_UserConfig.getUserConfig(UserId);
            if (UC == null) 
            {
                UC = new Struct_UserConfig();
            }

            UC.FacturaPorDefecto = cmb_FacturaPorDefecto.SelectedValue;
            UC.NombreNegocio = txt_NombreNegocio.Text;
            UC.MostrarLogoNegocio = chk_MostrarLogoNegocio.Checked;
            UC.PIN = "";
            UC.MostrarKiosco = chk_HabilitarKiosco.Checked;
            UC.Guardar(UserId);

        }

        protected void btn_SubirLogo_Click(object sender, EventArgs e)
        {

            string Direcotory = "";

            if (Directory.Exists(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersConfig") == false)
                {
                    Directory.CreateDirectory(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersConfig");
                }

                if (Directory.Exists(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersConfig\\" + UserId.ToString()) == false)
                {
                    Directory.CreateDirectory(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersConfig\\" + UserId.ToString());
                }
            Direcotory = PortalSettings.HomeDirectoryMapPath + "\\" + "UsersConfig\\" + UserId.ToString();

            if (uploadfile.HasFile) 
            {
                string[] splitter= {"."};
                string[] extensionsplitted = uploadfile.FileName.Split(splitter, StringSplitOptions.None);
                string extension="none";
                if (extensionsplitted.Length > 1) 
                {
                    for (int a = 0; a < extensionsplitted.Length; a++) 
                    {
                        bool foundit = false;
                        switch (extensionsplitted[a].ToLower()){
                            case "jpg":
                                extension="jpg";
                                foundit = true;
                                break;
                            case "png":
                                    extension = "png";
                                    foundit = true;
                                    break;
                            case "gif":
                                    extension = "gif";
                                    foundit = true;
                                    break;
                            case "jpeg":
                                    extension = "jpeg";
                                    foundit = true;
                                    break;
                           
                        }
                        if (foundit == true) break;
                    }
                }
                if (extension != "none")
                {
                    string[] files = Directory.GetFiles(Direcotory, "*.*",SearchOption.TopDirectoryOnly);
                    if (files.Length > 0)
                    {
                        for (int a = 0; a < files.Length; a++)
                        {
                            File.Delete(files[a]);
                        }
                    }

                    uploadfile.SaveAs(Direcotory + "\\Logo."+extension);
                    
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void btn_AgregarVendedor_Click(object sender, EventArgs e)
        {
            if (txt_NombreVendedor.Text != "" && txt_PorcentajeVendedor.Text != "")
            {
                Data2.Class.Struct_Vendedores.Insert_Vendedor(txt_NombreVendedor.Text,UserId, Data2.Statics.Conversion.GetDecimal(txt_PorcentajeVendedor.Text));
                Response.Redirect(Request.RawUrl);
            }
        }

      
       
    }
}