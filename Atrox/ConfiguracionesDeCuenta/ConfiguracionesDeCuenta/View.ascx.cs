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

        void LlenarCamposUserConfig() 
        {
            


            Data2.Class.Struct_UserConfig UC = Data2.Class.Struct_UserConfig.getUserConfig(UserId);
            if (UC != null)
            {
                MensajeConfigUsuario.Visible = false;
                txt_NombreNegocio.Text = UC.NombreNegocio;
                chk_MostrarLogoNegocio.Checked = UC.MostrarLogoNegocio;
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            try
            {
                Data2.Connection.D_StaticWebService SWS = new Data2.Connection.D_StaticWebService();
                KEY.Value = SWS.GetPrivateKeyByIdUser(UserId);
              
                    PC = Data2.Class.Struct_PrintConfiguration.GetPrintConfiguration(UserId);
                    LlenarIMGLogo();
                    if (!IsPostBack) 
                    {
                        LlenarCamposPrinter();
                        
                        LlenarCamposUserConfig();
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
    }
}