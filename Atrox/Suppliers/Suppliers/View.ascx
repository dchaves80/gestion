<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Suppliers.View" %>

<div id="Menu">
    <asp:Button ClientIDMode="Static" class="FormButton FirstElement" id="btnAgregarProveedor" runat="server" Text="Agregar Proveedor" />
    <asp:Button ClientIDMode="Static" class="FormButton" id="btnModificarProveedor" runat="server" Text="Modificar Proveedor" />
    <asp:Button ClientIDMode="Static" class="FormButton LastElement" id="btnBorrarProveedor" runat="server" Text="Borrar Proveedor" />
</div>
<div></div>
<div id="AtroxForm">
   <div>
   <span class="FormLabel">Nombre:</span><asp:TextBox ClientIDMode="Static"  class="AtroxTextBox" type="text" id="txtNombre" runat="server" />
   </div>

   <div> 
   <span class="FormLabel">Nombre Fantasía:</span><asp:TextBox ClientIDMode="Static"  class="AtroxTextBox" type="text" id="txtNombreFantasia" runat="server" />
   </div>

   

   <div> 
   <span class="FormLabel">País:</span> 
      <asp:DropDownList id="cmbPais" ClientIDMode="Static" runat="server">
          <asp:ListItem Text="(Ninguno)" Value="0"></asp:ListItem>
          <asp:ListItem Text="Argentina" Value="1"></asp:ListItem>
      </asp:DropDownList>
   </div>
   <div>
    <span class="FormLabel">Provincia:</span> 
       <asp:DropDownList id="cmbProvincia" ClientIDMode="Static" runat="server">

      </asp:DropDownList>
   </div>

    <div> 
   <span class="FormLabel">Localidad:</span><asp:TextBox class="AtroxTextBox" ClientIDMode="Static" id="txtLocalidad" runat="server" />
   </div>

      <div> 
   <span class="FormLabel">Domicilio:</span><asp:TextBox  class="AtroxTextBox" ClientIDMode="Static" type="text" id="txtDomicilio" runat="server" />
   </div>

      <div> 
   <span class="FormLabel">Teléfono 1:</span><asp:TextBox  class="AtroxTextBox" ClientIDMode="Static" type="text" id="txtTelefono1" runat="server" />
   </div>

       <div> 
   <span class="FormLabel">Teléfono 2:</span><asp:TextBox  class="AtroxTextBox" ClientIDMode="Static" type="text" id="txtTelefono2" runat="server" />
   </div>

      <div> 
   <span class="FormLabel">Mail Contacto:</span><asp:TextBox  class="AtroxTextBox" ClientIDMode="Static" type="text" id="txtMailContacto" runat="server" />
   </div>

      <div> 
   <span class="FormLabel">Mail Pedidos:</span><asp:TextBox  class="AtroxTextBox" ClientIDMode="Static" type="text" id="txtMailPedidos" runat="server" />
   </div>
<div>
     <span class="FormLabel">Categoría AFIP:</span> 
      <asp:DropDownList id="cmbCategoríaAFIP" ClientIDMode="Static" runat="server">
          
      </asp:DropDownList>
   </div>
    
     <div> 
   <span class="FormLabel">Ingresos Brutos:</span><asp:TextBox  class="AtroxTextBox" ClientIDMode="Static" type="text" id="txtIngresosBrutos" runat="server" />
   </div>

    <div>
     <span class="FormLabel">Tipo Documento:</span> 
      <asp:DropDownList id="cmbTipoDocumento" ClientIDMode="Static" runat="server">
      </asp:DropDownList>
   </div>
     <div> 
   <span class="FormLabel">Numero Documento:</span><asp:TextBox  class="AtroxTextBox" ClientIDMode="Static" type="text" id="TextBox1" runat="server" />
   </div>
</div>   
<script>
    $("#AtroxForm").hide();
    $("#cmbProvincia").hide();
    $("#cmbPais").change(function ()
    {
        if ($("#cmbPais").val() != "0") {
            $("#cmbProvincia").show(1000);
        } else
        {
            $("#cmbProvincia").hide(1000);
        }
    });
</script>

