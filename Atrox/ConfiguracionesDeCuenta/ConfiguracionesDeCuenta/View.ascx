<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.ConfiguracionesDeCuenta.View" %>
<style>
    .fieldrows {
        height:auto;
    }
</style>
<div id="accordionConfiguration">
  <h3>Impresora</h3>
  <div >
    
    <div class="fieldrows">
        <span>Modelo Impresora:</span>
        <asp:DropDownList ID="cmbPrintersModels" runat="server" >
            <asp:ListItem Text="HASAR 262" Value="HASAR-262" ></asp:ListItem>
            <asp:ListItem Text="HASAR 272" Value="HASAR-272" ></asp:ListItem>
            <asp:ListItem Text="HASAR 614" Value="HASAR-614" ></asp:ListItem>
            <asp:ListItem Text="HASAR 615" Value="HASAR-615" ></asp:ListItem>
            <asp:ListItem Text="HASAR 715" Value="HASAR-715" ></asp:ListItem>
            <asp:ListItem Text="HASAR 715_201" Value="HASAR-715_201" ></asp:ListItem>
            <asp:ListItem Text="HASAR 715_302" Value="HASAR-715_302" ></asp:ListItem>
            <asp:ListItem Text="HASAR 715_403" Value="HASAR-715_403" ></asp:ListItem>
            <asp:ListItem Text="HASAR 950" Value="HASAR-950" ></asp:ListItem>
            <asp:ListItem Text="HASAR 951" Value="HASAR-951" ></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="fieldrows">
        <span>Puerto:</span>
        <asp:DropDownList ID="cmbPuerto" runat="server" >
            <asp:ListItem Text="COM1" Value="COM1"></asp:ListItem>
            <asp:ListItem Text="COM2" Value="COM2"></asp:ListItem>
            <asp:ListItem Text="COM3" Value="COM3"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="fieldrows">
        <span>Baudios:</span>
        <asp:TextBox  ID="txtBaudios" runat="server" Text=""></asp:TextBox>
    </div>
    <div class="fieldrows">
        <asp:Button runat="server" ID="btnX" CssClass="FormButton FirstElement LastElement" Text="Imprimir X" OnClientClick="return false;" />
        <asp:Button runat="server" ID="btnZ" CssClass="FormButton FirstElement LastElement" Text="Imprimir Z" OnClientClick="return false;"/>
        <br />
        <asp:Button runat="server" ID="btnPrintText" CssClass="FormButton FirstElement LastElement" Text="Imprimir Texto" OnClientClick="return false;"/>
        <asp:TextBox runat="server" ID="txtTextoPrueba" CssClass="AtroxTextBox" Text="" MaxLength="100"/>
    </div>
    
    
  </div>
    <h3>OtrasConfiguraciones</h3>
    <div>

    </div>
</div>
<script>
    $("#accordionConfiguration").accordion({
        active:false,
        collapsible: true,
        create: function (event, ui)
        {

        }

    });
    
</script>