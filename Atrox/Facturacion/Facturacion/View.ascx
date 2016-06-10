<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Facturacion.View" %>
<div>
    <div>
        <asp:Button Text="Crear Factura" runat="server" ID="btnCrearFactura" CssClass="FormButton FirstElement LastElement" OnClick="btnCrearFactura_Click"/>
        <asp:Button Text="Cancelar Factura" runat="server" ID="btnBorrarFactura" CssClass="FormButton FirstElement LastElement" OnClick="btnBorrarFactura_Click"/>
        <div runat="server" id="MenuBusqueda" Class="SubMenu">
            <div><span class="FormLabel">Busqueda:</span><asp:TextBox ID="txtBuscar" ClientIDMode="Static" runat="server"  CssClass="AtroxTextBox"></asp:TextBox><asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="FormButton FirstElement LastElement" OnClick="btnBuscar_Click"  /> </div>
            <div>  <asp:RadioButtonList ID="SearchOption" runat="server" ClientIDMode="Static">
                <asp:ListItem Text="Buscar por Código" Value="0"></asp:ListItem>
                <asp:ListItem Text="Buscar por Nombre" Value="1" Selected="True"></asp:ListItem>
            </asp:RadioButtonList> </div>
            <div id="tableBody" runat="server"></div>
            
        </div>
    </div>
</div>
<div id="debugDIV" runat="server"></div>

<script>

    var txtBuscar = $("#txtBuscar");
    var SearchOption = $("#SearchOption");

    

    txtBuscar.keyup(function () {
        Buscar(txtBuscar.val());




    });



    function Buscar(toBuscar) {
       
        var value = "";
        var input = SearchOption.find("input:checked");
        value = input.val();
        var ByCodet = false;
        if (value == 1) {
            ByCodet = false;
        } else
        {
            ByCodet = true;
        }

        var replacedcharacters = toBuscar.replace(" ", "_");
        var request = $.ajax({
            url: "../DesktopModules/Facturacion/API/ModuleTask/SearchProduct",
            method: "GET",
            data: { Search: replacedcharacters, ByCode:ByCodet },
            dataType: "html",
            cache: false
        });

        request.done(function (msg) {
            if (msg != "null") {
                var availableTags = msg.split("/");
                txtBuscar.autocomplete({
                    source: availableTags,
                    open: function (event, ui)
                    {

                    }
                });
            } else {
                
            }
        });
    }
</script>