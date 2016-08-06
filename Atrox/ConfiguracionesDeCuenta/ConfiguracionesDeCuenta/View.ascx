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
        <span class="FormLabel">Modelo Impresora:</span>
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
        <span class="FormLabel">Puerto:</span>
        <asp:DropDownList ID="cmbPuerto" runat="server" >
            <asp:ListItem Text="COM1" Value="COM1"></asp:ListItem>
            <asp:ListItem Text="COM2" Value="COM2"></asp:ListItem>
            <asp:ListItem Text="COM3" Value="COM3"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="fieldrows">
        <span class="FormLabel">Baudios:</span>
        <asp:TextBox  ID="txtBaudios" runat="server" Text=""></asp:TextBox>
    </div>
    
    <div>
      <asp:Button runat="server" ID="btnGuardar" CssClass="FormButton FirstElement LastElement" Text="Guardar" OnClick="btnGuardar_Click" />
    </div>

    <div class="fieldrows">
        <asp:Button runat="server" ID="btnX" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" Text="Imprimir X" OnClientClick="SendCommand('PRINTX');return false;" />
        <asp:Button runat="server" ID="btnZ" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" Text="Imprimir Z" OnClientClick="SendCommand('PRINTZ');return false;"/>
        <asp:Button runat="server" ID="btnAvanzarPapel" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" Text="Avanzar Papel" OnClientClick="SendCommand('AVANZARPAPEL');return false;"/>
        <br />
        <asp:Button runat="server" ID="btnPrintText" CssClass="FormButton FirstElement LastElement" Text="Imprimir Texto" OnClientClick="return false;"/>
        <asp:TextBox runat="server" ID="txtTextoPrueba" CssClass="AtroxTextBox" Text="" MaxLength="100"/>
    </div>
    
    
  </div>
    <h3>Configuraciones de negocio</h3>
    
    <div>
        <div runat="server" id="MensajeConfigUsuario" class="MessageBox"></div>
        <div class="SubMenu2">
        <span class="FormLabel">Nombre de Empresa:</span><asp:TextBox runat="server" ID="txt_NombreNegocio"></asp:TextBox>
        </div>
        <div class="SubMenu2">
        <span class="FormLabel">Factura por defecto:</span><asp:DropDownList runat="server" ID="cmb_FacturaPorDefecto">
            <asp:ListItem Value="A" Text="A"></asp:ListItem>
            <asp:ListItem Value="B" Text="B"></asp:ListItem>
                                         </asp:DropDownList>
            </div>
        <div class="SubMenu2">
            <span class="FormLabel">Subir Logo:</span><asp:FileUpload runat="server" ID="uploadfile"/>
            <asp:Button Text="Guardar Logo" ID="btn_SubirLogo" runat="server" OnClick="btn_SubirLogo_Click" /> 

        </div>
        <div class="SubMenu2">
            <span class="FormLabel">Logo de la empresa:</span><asp:Image runat="server" ID="img_logo"/>

        </div>
        <div class="SubMenu2">
            <span class="FormLabel">¿ Mostrar Logo ?</span><asp:CheckBox Id="chk_MostrarLogoNegocio" runat="server"/>
        </div>
        <div>
            <asp:Button Text="Guardar" ID="btnGuardarConfigNegocio" runat="server" OnClick="btnGuardarConfigNegocio_Click" /> 
            </div>
    </div>
    <h3>Vendedores</h3>
    <div>
        
        <div class="SubMenu2">
            <span class="FormLabel">Nombre Vendedor:</span><asp:TextBox runat="server" ID="txt_NombreVendedor"></asp:TextBox>
            <br />
            <span class="FormLabel">Porcentaje:</span><asp:TextBox runat="server" ID="txt_PorcentajeVendedor"></asp:TextBox>
            <br />
            <asp:Button Text="Agregar Vendedor" ID="btn_AgregarVendedor" runat="server" /> 
         </div>

        <div class="SubMenu2">
            <span class="FormLabel">Vendedores disponibles:</span><asp:DropDownList runat="server" ID="cmb_ListadoVendedores"></asp:DropDownList>
            <br />
            <asp:Button Text="Editar Vendedor" ID="btn_EditarVendedor" runat="server" /> 
            <asp:Button Text="Borrar Vendedor" ID="btn_BorrarVendedor" runat="server" /> 

        </div>



    </div>
</div>
<asp:HiddenField ID="KEY" Value="" ClientIDMode="Static" runat="server" />
<script>

    var Key = $("#KEY").val();

    $("#accordionConfiguration").accordion({
        active:false,
        collapsible: true,
        create: function (event, ui)
        {

        }

    });



    function SendCommand(Command) {

        var request = $.ajax({
            url: "../DesktopModules/ConfiguracionesDeCuenta/API/ModuleTask/SendPrinterCommand",
            method: "GET",
            data: { KEY: Key, COMMAND: Command},
            dataType: "html",
            cache: false
        });

        request.done(function (msg) {

            if (msg == "OK") {

            } else {
               
            }
        });

        request.fail(function (jqXHR, textStatus) {
            alert(textStatus);
        });
    }
    
</script>