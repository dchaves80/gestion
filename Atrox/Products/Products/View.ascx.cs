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
using System.IO;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

namespace Christoc.Modules.Products
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ProductsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ProductsModuleBase, IActionable
    {
        bool isOk = true;

        public class ElementoIndice 
        {
            public string Key;
            public string Url;

            public ElementoIndice(string p_KEY, string p_url) 
            {
                Key = p_KEY;
                Url = p_url;
            }

        }

        private void LlenarUnidades()
        {
            if (cmbUnidades.Items.Count == 0)
            {
                List<Data2.Class.Struct_Unidades> MyUnitsList = Data2.Class.Struct_Unidades.GetAll();
                if (MyUnitsList != null)
                {
                    for (int a = 0; a < MyUnitsList.Count; a++)
                    {
                        cmbUnidades.Items.Add(new System.Web.UI.WebControls.ListItem(MyUnitsList[a].Nombre, MyUnitsList[a].Id.ToString()));
                    }
                }

            }
        }

        private void LlenarProveedores()
        {
            if (cmbProveedor.Items.Count == 0 && CmbUpdateProviders.Items.Count == 0)
            {
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
                        cmbProveedor.Items.Add(t_LI);
                        CmbUpdateProviders.Items.Add(t_LIupdate);
                    }
                }
            }
        }

        

        private void ConstruirIndice() 
        {
            List<ElementoIndice> ListaElementosIndice = new List<ElementoIndice>();
            ListaElementosIndice.Add(new ElementoIndice("Todos", ""));
            ListaElementosIndice.Add(new ElementoIndice("A","Letter=A"));
            ListaElementosIndice.Add(new ElementoIndice("B", "Letter=B"));
            ListaElementosIndice.Add(new ElementoIndice("C", "Letter=C"));
            ListaElementosIndice.Add(new ElementoIndice("D", "Letter=D"));
            ListaElementosIndice.Add(new ElementoIndice("E", "Letter=E"));
            ListaElementosIndice.Add(new ElementoIndice("F", "Letter=F"));
            ListaElementosIndice.Add(new ElementoIndice("G", "Letter=G"));
            ListaElementosIndice.Add(new ElementoIndice("H", "Letter=H"));
            ListaElementosIndice.Add(new ElementoIndice("I", "Letter=I"));
            ListaElementosIndice.Add(new ElementoIndice("J", "Letter=J"));
            ListaElementosIndice.Add(new ElementoIndice("K", "Letter=K"));
            ListaElementosIndice.Add(new ElementoIndice("L", "Letter=L"));
            ListaElementosIndice.Add(new ElementoIndice("M", "Letter=M"));
            ListaElementosIndice.Add(new ElementoIndice("N", "Letter=N"));
            ListaElementosIndice.Add(new ElementoIndice("Ñ", "Letter=Ñ"));
            ListaElementosIndice.Add(new ElementoIndice("O", "Letter=O"));
            ListaElementosIndice.Add(new ElementoIndice("P", "Letter=P"));
            ListaElementosIndice.Add(new ElementoIndice("Q", "Letter=Q"));
            ListaElementosIndice.Add(new ElementoIndice("R", "Letter=R"));
            ListaElementosIndice.Add(new ElementoIndice("S", "Letter=S"));
            ListaElementosIndice.Add(new ElementoIndice("T", "Letter=T"));
            ListaElementosIndice.Add(new ElementoIndice("U", "Letter=U"));
            ListaElementosIndice.Add(new ElementoIndice("V", "Letter=V"));
            ListaElementosIndice.Add(new ElementoIndice("W", "Letter=W"));
            ListaElementosIndice.Add(new ElementoIndice("X", "Letter=X"));
            ListaElementosIndice.Add(new ElementoIndice("Y", "Letter=Y"));
            ListaElementosIndice.Add(new ElementoIndice("Z", "Letter=Z"));

            for (int a = 0; a < ListaElementosIndice.Count; a++) 
            {
                HtmlGenericControl t_span = new HtmlGenericControl("span");
                HtmlGenericControl t_a = new HtmlGenericControl("a");
                t_a.Attributes.Add("href", "/MyManager/Articulos?" + ListaElementosIndice[a].Url);
                t_span.Attributes.Add("Class", "IndexLetter");
                t_span.InnerText = ListaElementosIndice[a].Key;
                t_a.Controls.Add(t_span);
                IndiceArticulos.Controls.Add(t_a);
            }





        }

        private void EventsHandlers()
        {
            
        }

        bool parseToDEC(string p_str)
        {
            try
            {
                
                decimal.Parse(p_str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CheckFields() 
        {
            isOk = true;
            if (txtCodigo.Text == "") isOk = false;
            if (txtNombre.Text == "") isOk = false;
            if (!parseToDEC(txtPorcentajeGanancia.Text)) isOk = false;
            if (!parseToDEC(txtPorcentajeIVA.Text)) isOk = false;
            if (!parseToDEC(txtPrecioCompra.Text)) isOk = false;
            if (!parseToDEC(txtPrecioFinal.Text)) isOk = false;
            if (!parseToDEC(txtPrecioNeto.Text)) isOk = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CheckFields();
            if (isOk==true)
            {
                if (Mode.Value.ToLower() == "none")
                {

                    Data2.Class.Struct_Producto t_PRD = new Data2.Class.Struct_Producto(
                        UserId,
                        int.Parse(cmbProveedor.SelectedValue),
                        txtCodigo.Text,
                        txtCodigoDeBarra.Text,
                        txtNombre.Text,
                        Data2.Statics.Conversion.GetDecimal(txtPrecioNeto.Text),
                        Data2.Statics.Conversion.GetDecimal(txtPorcentajeIVA.Text),
                        Data2.Statics.Conversion.GetDecimal(txtPrecioCompra.Text),
                        Data2.Statics.Conversion.GetDecimal(txtPorcentajeGanancia.Text),
                        Data2.Statics.Conversion.GetDecimal(txtPrecioFinal.Text),
                        int.Parse(cmbUnidades.SelectedValue));
                    if (t_PRD.Guardar() == true)
                    {
                        Response.Redirect("~/MyManager/Articulos?Message=Success1");
                    }
                    else
                    {
                        Response.Redirect("~/MyManager/Articulos?Message=Error1");
                    };

                }
                else if (Mode.Value.ToLower()=="fedt") 
                {

                    if (IdArt.Value != "0") 
                    {
                        int IDART = int.Parse(IdArt.Value);

                        Data2.Class.Struct_Producto PRD = Data2.Class.Struct_Producto.Get_SingleArticle(UserId, IDART);
                        if (PRD != null) 
                        {
                          
                          PRD.IdProveedor =   int.Parse(cmbProveedor.SelectedValue);
                        PRD.CodigoInterno = txtCodigo.Text;
                        PRD.CodigoBarra = txtCodigoDeBarra.Text;
                        PRD.Descripcion = txtNombre.Text;
                        PRD.PrecioNeto =  Data2.Statics.Conversion.GetDecimal(txtPrecioNeto.Text);
                        PRD.IVA  = Data2.Statics.Conversion.GetDecimal(txtPorcentajeIVA.Text);
                        PRD.PrecioCompra = Data2.Statics.Conversion.GetDecimal(txtPrecioCompra.Text);
                        PRD.PorcentajeGanancia = Data2.Statics.Conversion.GetDecimal(txtPorcentajeGanancia.Text);
                        PRD.PrecioFinal = Data2.Statics.Conversion.GetDecimal(txtPrecioFinal.Text);
                        PRD.IdUnidad = int.Parse(cmbUnidades.SelectedValue);

                        if (PRD.Actualizar(UserId))
                        {
                            Response.Redirect("~/MyManager/Articulos?Message=Success1");
                        }
                        else 
                        {
                            Response.Redirect("~/MyManager/Articulos?Message=Error1");
                        }


                        }

                    }
                    
                }
               
            }
        }


        

        private void ConstruirListadoArticulos() 
        {


            /*
             * <table cellspacing="0" border="1" style="border-collapse:collapse;" class="AtroxTableGrid">
		<tbody id="BodyTable" runat="server">
        <tr id="HeadersListadoArticulos" runat="server" class="AtroxHeaderGrid">
    	</tr>
		</tbody></table>
            */
            //Cabeceras


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



            string[] Cabeceras = { "Codigo", "Descripcion", "IVA", "Precio Compra", "Precio Venta", "Editar/Borrar"};

            

            for (int a = 0; a < Cabeceras.Length; a++) 
            {
                HtmlGenericControl HTMLGENERIC = new HtmlGenericControl("th");
                HTMLGENERIC.InnerText = Cabeceras[a];
                MyRowHeader.Controls.Add(HTMLGENERIC);
            }

            ListadoArticulos.Controls.Add(MyTable);

            int _Page = 1;
            string _Letter = "";

            if (Request["Page"] != null) 
            {
                _Page = int.Parse(Request["Page"].ToString());

            }

            if (Request["Letter"] != null) 
            {
                _Letter = Request["Letter"].ToString();
            }

            Data2.Class.Struct_Producto.PagingArticles _PA = Data2.Class.Struct_Producto.GetArticlesPaging(_Letter, _Page, 150, UserId, 0);
            if (_PA != null) 
            {

                for (int a = 0; a < _PA.Listado.Count; a++) 
                {
                    HtmlGenericControl _TR = new HtmlGenericControl("tr");
                    _TR.Attributes.Add("class", "AtroxRowTable");

                    HtmlGenericControl _TDCodigo = new HtmlGenericControl("td");
                    HtmlGenericControl _TDDescripcion = new HtmlGenericControl("td");
                    HtmlGenericControl _TDIVA = new HtmlGenericControl("td");
                    HtmlGenericControl _TDPrecioCompra = new HtmlGenericControl("td");
                    HtmlGenericControl _TDPrecioVenta = new HtmlGenericControl("td");
                    HtmlGenericControl _TDEditarBorrar = new HtmlGenericControl("td");

                    _TDCodigo.InnerText = _PA.Listado[a].CodigoInterno;
                    _TDDescripcion.InnerText = _PA.Listado[a].Descripcion;
                    _TDIVA.InnerText = _PA.Listado[a].IVA.ToString();
                    _TDPrecioCompra.InnerText = _PA.Listado[a].PrecioCompra.ToString();
                    _TDPrecioVenta.InnerText = _PA.Listado[a].PrecioFinal.ToString();

                    HtmlGenericControl Editar = new HtmlGenericControl("a");
                    Editar.Attributes.Add("href", "/MyManager/Articulos?mode=edt&art=" + _PA.Listado[a].Id.ToString());
                    Editar.InnerText = "Editar";
                    HtmlGenericControl Borrar = new HtmlGenericControl("a");
                    Borrar.Attributes.Add("href", "/MyManager/Articulos?mode=del&art=" + _PA.Listado[a].Id.ToString());
                    Borrar.InnerText = "Borrar";
                    _TDEditarBorrar.Controls.Add(Editar);
                    _TDEditarBorrar.Controls.Add(Borrar);
                    _TDEditarBorrar.Attributes.Add("class", "AtroxDarkLink");
                    
                    
                    //_TDEditarBorrar.InnerText = "EditarBorrar";

                    _TR.Controls.Add(_TDCodigo);
                    _TR.Controls.Add(_TDDescripcion);
                    _TR.Controls.Add(_TDIVA);
                    _TR.Controls.Add(_TDPrecioCompra);
                    _TR.Controls.Add(_TDPrecioVenta);
                    _TR.Controls.Add(_TDEditarBorrar);

                    MyTbody.Controls.Add(_TR);
                    
                    

                }

            }

            if (_PA != null && _PA.NumeroDePaginas > 1) 
            {
                for (int a = 1; a <= _PA.NumeroDePaginas;a++ ) 
                {
                    HtmlGenericControl _NumberControl = new HtmlGenericControl("span");
                    _NumberControl.Attributes.Add("class","IndexLetter");
                    _NumberControl.InnerText = a.ToString();
                    
                    if (a != _Page)
                    {
                        _NumberControl.Attributes.Add("class", "IndexLetter");
                    }
                    else 
                    {
                        _NumberControl.Attributes.Add("class", "IndexLetter IndexNumberSelected");
                    }

                    HtmlGenericControl _myANumber = new HtmlGenericControl("a");

                    if (_Letter != "")
                    {
                        _myANumber.Attributes.Add("href", "/MyManager/Articulos?" + "Letter=" + _Letter + "&Page=" + a.ToString());
                    }
                    else 
                    {
                        _myANumber.Attributes.Add("href", "/MyManager/Articulos?" + "Page=" + a.ToString());
                    }

                    _myANumber.Controls.Add(_NumberControl);
                    IndiceNumerico.Controls.Add(_myANumber);
                    

                }
            }
            
            



        }

        private void LlenarArchivosCMB() 
        {



            if (CmbFileList.Items.Count == 0)
            {

                if (Directory.Exists(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersUpdate") == false)
                {
                    Directory.CreateDirectory(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersUpdate");
                }

                if (Directory.Exists(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersUpdate\\" + UserId.ToString()) == false)
                {
                    Directory.CreateDirectory(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersUpdate\\" + UserId.ToString());
                }

                string[] MyFileList = Directory.GetFiles(PortalSettings.HomeDirectoryMapPath + "\\" + "UsersUpdate\\" + UserId.ToString());

                if (MyFileList != null && MyFileList.Length > 0)
                {
                    string[] splitter = { "\\" };
                    for (int a = 0; a < MyFileList.Length; a++)
                    {
                        string[] filename = MyFileList[a].Split(splitter, StringSplitOptions.None);

                        CmbFileList.Items.Add(filename[filename.Length - 1]);
                    }
                }
            }

        }


        private void InterpretarMensajes() 
        {
            if (Request["Message"] != null)
            {
                if (Request["Message"] == "Error1")
                {
                    Data2.Statics.ConstAlert.AddMessage(Data2.Statics.ConstAlert.Art_Record_Error, Data2.Statics.ConstAlert.TypeMessage.Error, MessageBox);
                }

                if (Request["Message"] == "Success1")
                {
                    Data2.Statics.ConstAlert.AddMessage(Data2.Statics.ConstAlert.Art_Record_Success, Data2.Statics.ConstAlert.TypeMessage.Message, MessageBox);
                }

                if (Request["Message"] == "Success2")
                {
                    Data2.Statics.ConstAlert.AddMessage(Data2.Statics.ConstAlert.Art_File_SuccessUploaded, Data2.Statics.ConstAlert.TypeMessage.Message, MessageBox);
                }
                if (Request["Message"] == "Error2")
                {
                    Data2.Statics.ConstAlert.AddMessage(Data2.Statics.ConstAlert.Art_File_ErrorUploaded, Data2.Statics.ConstAlert.TypeMessage.Error, MessageBox);
                }
                if (Request["Message"] == "Success3")
                {
                    Data2.Statics.ConstAlert.AddMessage(Data2.Statics.ConstAlert.Art_File_SuccessProcessing, Data2.Statics.ConstAlert.TypeMessage.Message, MessageBox);
                }
                if (Request["Message"] == "Error3")
                {
                    Data2.Statics.ConstAlert.AddMessage(Data2.Statics.ConstAlert.Art_File_ErrorProcessing, Data2.Statics.ConstAlert.TypeMessage.Error, MessageBox);
                }

            }
        }

        private void InterpretarResultados() 
        {
            if (Request["Res"] != null) 
            {
                string[] splitter = {"-"};
                string[] RES = Request["Res"].ToString().Split(splitter, StringSplitOptions.None);
                Data2.Statics.ConstAlert.AddMessage("Articulos insertados: " + RES[0],Data2.Statics.ConstAlert.TypeMessage.Message,MessageBox);
                Data2.Statics.ConstAlert.AddMessage("Articulos erroneos: " + RES[1], Data2.Statics.ConstAlert.TypeMessage.Message, MessageBox);
                Data2.Statics.ConstAlert.AddMessage("Actualizaciones exitosas: " + RES[2], Data2.Statics.ConstAlert.TypeMessage.Message, MessageBox);
                Data2.Statics.ConstAlert.AddMessage("Actualizaciones erroneas: " + RES[3], Data2.Statics.ConstAlert.TypeMessage.Message, MessageBox);
            }
        }

        private void InterpretarModo() 
        {
            string mode="";

            if (Request["mode"] != null) 
            {
                mode = Request["mode"].ToString();
            }

            if (mode.ToLower() == "edt") 
            {
                if (Request["art"] != null) 
                {
                    int articleid = int.Parse(Request["art"]);
                    if (articleid!=0)
                    {
                        Data2.Class.Struct_Producto prod = Data2.Class.Struct_Producto.Get_SingleArticle(UserId, articleid);
                        if (prod != null) 
                        {
                            Mode.Value = "edt";
                            IdArt.Value = prod.Id.ToString();

                            txtCodigo.Text = prod.CodigoInterno;
                            txtCodigoDeBarra.Text = prod.CodigoBarra;
                            txtNombre.Text = prod.Descripcion;
                            txtPorcentajeGanancia.Text = prod.PorcentajeGanancia.ToString();
                            txtPorcentajeIVA.Text = prod.IVA.ToString();
                            txtPrecioCompra.Text = prod.PrecioCompra.ToString();
                            txtPrecioFinal.Text = prod.PrecioFinal.ToString();
                            txtPrecioNeto.Text = prod.PrecioNeto.ToString();
                            
                            for (int a = 0; a < cmbProveedor.Items.Count; a++) 
                            {
                                if (prod.IdProveedor.ToString() == cmbProveedor.Items[a].Value) 
                                {
                                    cmbProveedor.SelectedIndex = a;
                                    break;
                                }
                                    
                            }

                            for (int a = 0; a < cmbUnidades.Items.Count; a++)
                            {
                                if (prod.IdUnidad.ToString() == cmbUnidades.Items[a].Value)
                                {
                                    cmbUnidades.SelectedIndex = a;
                                    break;
                                }

                            }



                        }
                        
                    }
                }
            } else if (mode.ToLower() == "del")
            {
                if (Request["art"] != null) 
                {
                    int IdArt = int.Parse(Request["art"].ToString());
                    Data2.Class.Struct_Producto PRD = Data2.Class.Struct_Producto.Get_SingleArticle(UserId, IdArt);
                    if (PRD != null) 
                    {
                        PRD.Borrar(UserId);
                        Response.Redirect("/MyManager/Articulos");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            EventsHandlers();
            LlenarUnidades();
            LlenarProveedores();
            ConstruirIndice();
            ConstruirListadoArticulos();
            LlenarArchivosCMB();
            InterpretarMensajes();
            InterpretarResultados();
            if (!IsPostBack)
            {
                InterpretarModo();
            }
            

            //MANEJO DE ARCHIVO
            /*  DirectoryInfo DI = new DirectoryInfo(DotNetNuke.Entities.Portals.PortalSettings.Current.HomeDirectoryMapPath);
              FileInfo[] FI = DI.GetFiles();
              if (FI.Length > 0) 
              {
                  for (int a = 0; a < FI.Length; a++) 
                  {
                      HtmlGenericControl HTMLGC = new HtmlGenericControl("div");
                      HTMLGC.InnerText = FI[a].FullName;
                      HTMLGC.Visible = true;
                      fileList.Controls.Add(HTMLGC);

                   
                  }
              }

              HtmlGenericControl HTMLGC2 = new HtmlGenericControl("div");
              HTMLGC2.InnerText = DI.FullName;
              HTMLGC2.Visible = true;
              fileList.Controls.Add(HTMLGC2);*/

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

        protected void btnSubirArchivo_Click(object sender, EventArgs e)
        {
            
            
            
           

            string FileName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".user" + UserId.ToString();

            try{
            FileUploader.SaveAs(PortalSettings.HomeDirectoryMapPath +"\\" + "UsersUpdate\\" + UserId.ToString() + "\\" + FileName);
            Response.Redirect("~/MyManager/Articulos?Message=Success2");
            } catch
            {
                Response.Redirect("~/MyManager/Articulos?Message=Error2");
            }


            
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {

            List<Data2.Class.Struct_Unidades> t_unidades = Data2.Class.Struct_Unidades.GetAll();
            int UnidadPorDefecto=0; 
            if (t_unidades.Count>0){UnidadPorDefecto = t_unidades[0].Id;}
            
            if (CmbFileList.Items.Count > 0) 
            {
                string FileName=CmbFileList.SelectedItem.Text;
                Data2.Statics.Log.ADD("Examinando:" + FileName, this);
                System.IO.StreamReader file = new System.IO.StreamReader(PortalSettings.HomeDirectoryMapPath  + "\\" + "UsersUpdate\\" + UserId.ToString() + "\\" + FileName,System.Text.Encoding.Default);
                bool failure = false;

            
            string firstLine="";
            again:
            if (!file.EndOfStream)
            {
                firstLine = file.ReadLine().ToLower();
                /*firstLine = firstLine.Replace("\u001a", "");
                firstLine = firstLine.Replace("\u0011", "");
                firstLine = firstLine.Replace("\u0007", "");
                firstLine = firstLine.Replace("\u0000", "");
                firstLine = firstLine.Replace("\u0016", "");
                firstLine = firstLine.Replace("\u001d", "");*/
                if (!firstLine.Contains("codigo")) goto again;
                Data2.Statics.Log.ADD(firstLine, this);
            }
            


                //firstLine = firstLine.Replace("\u001a", "");
                //firstLine = firstLine.Replace("\u0007", "");
                //firstLine = firstLine.Replace("\u0000", "");
                
                
                

                if (!firstLine.Contains("codigo")) failure = true;
                if (!firstLine.Contains("precioneto")) failure = true;
                if (!firstLine.Contains("iva")) failure = true;
                if (!firstLine.Contains("preciocompra")) failure = true;
                if (!firstLine.Contains("porcentajeganancia")) failure = true;
                if (!firstLine.Contains("preciofinal")) failure = true;
                Data2.Statics.Log.ADD("Primera Linea:" + firstLine,this);
                if (failure == false)
                {

                    int indexcodigo = -1;
                    int indexprecioneto = -1;
                    int indexiva = -1;
                    int indexpreciocompra = -1;
                    int indexporcentajeganancia = -1;
                    int indexpreciofinal = -1;
                    int indexdescripcion = -1;
                    int indexcodigodebarra = -1;
                    string[] splitter = { "\t" };
                    string[] fields = firstLine.Split(splitter, StringSplitOptions.None);

                    for (int a = 0; a < fields.Length; a++)
                    {
                        if (fields[a] == "codigo") indexcodigo = a;
                        if (fields[a] == "precioneto") indexprecioneto = a;
                        if (fields[a] == "iva") indexiva = a;
                        if (fields[a] == "preciocompra") indexpreciocompra = a;
                        if (fields[a] == "porcentajeganancia") indexporcentajeganancia = a;
                        if (fields[a] == "preciofinal") indexpreciofinal = a;
                        if (fields[a] == "descripcion") indexdescripcion = a;
                        if (fields[a] == "codigobarra") indexcodigodebarra = a;
                    }


                    int UpdateSucces = 0;
                    int UpdateError = 0;
                    int NewSucces = 0;
                    int NewError = 0;

                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        Console.WriteLine(line);

                        string[] separatevalues = line.Split(splitter, StringSplitOptions.None);
                        if (separatevalues.Length >= 5)
                        {


                            
                            string artcod = separatevalues[indexcodigo];
                            decimal precioneto = Data2.Statics.Conversion.GetDecimal(separatevalues[indexprecioneto]);
                            decimal iva = Data2.Statics.Conversion.GetDecimal(separatevalues[indexiva]);
                            decimal preciocompra = Data2.Statics.Conversion.GetDecimal(separatevalues[indexpreciocompra]);
                            decimal porcentajeganancia = Data2.Statics.Conversion.GetDecimal(separatevalues[indexporcentajeganancia]);
                            decimal preciofinal = Data2.Statics.Conversion.GetDecimal(separatevalues[indexpreciofinal]);
                            string descripcion = artcod;
                            string codigobarra = artcod;
                            if (indexdescripcion != -1) { 
                                descripcion = separatevalues[indexdescripcion];
                                descripcion = descripcion.Replace("\"\"", "*");
                                descripcion = descripcion.Replace("\"", "");
                                descripcion = descripcion.Replace("*", "\"");
                            }
                            if (indexcodigodebarra != -1) codigobarra = separatevalues[indexcodigodebarra];
                            int IdProveddor = int.Parse(CmbUpdateProviders.SelectedValue);
                            Data2.Class.Struct_Producto MyProduct = Data2.Class.Struct_Producto.SelectSingleArticle(UserId, IdProveddor, artcod);
                            if (MyProduct != null)
                            {
                                MyProduct.PrecioNeto = precioneto;
                                MyProduct.IVA = iva;
                                MyProduct.PorcentajeGanancia = porcentajeganancia;
                                MyProduct.PrecioFinal = preciofinal;

                                if (MyProduct.ActualizarPrecios() == true)
                                {
                                    UpdateSucces++;
                                }
                                else 
                                {
                                    UpdateError++;
                                }
                                

                            }
                            else 
                            {
                                
                                
                                
                                MyProduct = new Data2.Class.Struct_Producto(UserId, IdProveddor, artcod, codigobarra, descripcion, precioneto, iva, preciocompra, porcentajeganancia, preciofinal, UnidadPorDefecto);
                                if (MyProduct.Guardar() == true)
                                {
                                    NewSucces++;
                                }
                                else 
                                {
                                    NewError++;
                                }
                            }




                        }

                    }

                    string RESVALUE = NewSucces.ToString() + "-" + NewError.ToString() + "-" + UpdateSucces.ToString() + "-" + UpdateError.ToString();
                    Data2.Statics.Log.ADD("NUEVOS EXITOSOS:" + NewSucces.ToString() + "NUEVOS ERROR:" + NewError.ToString() + "UPDATES EXITOSOS:" + UpdateSucces.ToString() + "NUEVOS EXITOSOS:" + UpdateError.ToString(), this);
                    file.Close();
                    //Fin de recorrido...
                    Response.Redirect("~/MyManager/Articulos?Message=Success3&Res=" + RESVALUE);


                }
                else 
                {
                    Data2.Statics.Log.ADD("Campos Incorrectos", this);
                    file.Close();
                    Response.Redirect("~/MyManager/Articulos?Message=Error3");
                }

            }

            

        }

        protected void btnGuardar_Click1(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click2(object sender, EventArgs e)
        {

        }
    }
}