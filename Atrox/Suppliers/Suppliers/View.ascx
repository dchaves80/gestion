
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Suppliers.View" %>

<div id="AtroxMenu">
    <asp:Button ClientIDMode="Static" class="FormButton FirstElement LastElement" id="btnAgregarProveedor" runat="server" Text="Agregar Proveedor"  />
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
   <span class="FormLabel">Numero Documento:</span><asp:TextBox  class="AtroxTextBox" ClientIDMode="Static" type="text" id="txtNumeroDocumento" runat="server" />
   </div>

    <div>
    <asp:Button ClientIDMode="Static" class="FormButton FirstElement" id="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
    <asp:Button ClientIDMode="Static" class="FormButton LastElement" id="btnCancelar" runat="server" Text="Cancelar" />
    <asp:HiddenField ClientIDMode="Static" ID="ModeField" Value="None" runat="server" />
    <asp:HiddenField ClientIDMode="Static" ID="SupplierIdField" Value="0" runat="server" />
    </div>
   
</div>   
 <div style="width:100%">
    <asp:GridView runat="server" ID="SearchGrid" HeaderStyle-CssClass="AtroxHeaderGrid" CssClass="AtroxTableGrid" RowStyle-CssClass="AtroxRowTable" AutoGenerateColumns="false">
        <Columns>
            
            <asp:BoundField HeaderText="Id" DataField="Id"/>
            <asp:BoundField HeaderText="Nombre Fantasía" DataField="NombreFantasía"/>
            <asp:BoundField HeaderText="Telefono" DataField="Telefono"/>
            <asp:BoundField HeaderText="Mail" DataField="Mail"/>
            <asp:HyperLinkField HeaderText="Modificar/Borrar"/>
            

            

        </Columns>
    </asp:GridView>
 </div>


<script>
    
    function Cancel_<%=this.ClientID%>()
    {
        $("#AtroxMenu").show(1000);
        $("#AtroxForm").hide(1000);
        $("#ModeField").val("None");
    }

    function NewSupplier_<%=this.ClientID%>()
    {
        $("#AtroxMenu").hide(1000);
        $("#AtroxForm").show(1000);
        $("#ModeField").val("New");
    }


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

    if ($("#ModeField").val() == "Edit")
    {
        $("#AtroxMenu").hide(1000);
        $("#AtroxForm").show(1000);
        $("#cmbProvincia").show(1000);
    }

</script>

