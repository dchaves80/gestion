<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Stock.View" %>
<div>
    <span class="FormLabel"><b>Buscar Articulo:</b></span><asp:TextBox ID="txtBuscar" ClientIDMode="static" runat="server" CssClass="AtroxTextBox"></asp:TextBox><asp:Button id ="btnBuscar" ClientIDMode="static" runat="server" CssClass="FormButton FirstElement LastElement" Text="Buscar" OnClick="btnBuscar_Click"/>
</div>
<div><asp:Button Text="Busqueda Avanzada" ClientIDMode="static"  id="btnBusquedaAvanzada" runat="server" OnClientClick="togglePanelDeBusqueda();return false;" CssClass="FormButton FirstElement LastElement"/></div>

<div id="BusquedaAvanzada" class="SubMenu">
    <div>
        <span class="FormLabel"> Proveedor </span>
        <asp:DropDownList runat="server" ID="cmbProveedores" ClientIDMode="static">
            </asp:DropDownList>
</div>
    
    <div>
    <asp:RadioButtonList ID="RadioButtonList" runat="server" ClientIDMode="static">
        <asp:ListItem runat="server" Text="Buscar por Codigo" id="CHKbyCode" Selected="true"></asp:ListItem>
        <asp:ListItem runat="server" Text="Buscar por Nombre" id="CHKbyName"></asp:ListItem>
    </asp:RadioButtonList>
        </div>
    <asp:CheckBox Text=" Conservar los Parametros de busqueda" runat="server" ClientIDMode="static" ID="chkParameters" OnCheckedChanged="chkParameters_CheckedChanged" AutoPostBack="true"/>
</div>
<div id="ListadoArticulos" runat="server">
  
</div>
<div id="DivMessage" runat="server">
    
</div>
<div runat="server" id="cool01"></div>
<asp:HiddenField id="KEY" runat="server" ClientIDMode="Static" Value="" ></asp:HiddenField>
<script>
    var PanelBusquedaAvanzada = $("#BusquedaAvanzada");

    PanelBusquedaAvanzada.hide();
    function togglePanelDeBusqueda()
    {
        PanelBusquedaAvanzada.toggle(1000);
    }


    $(".OKStatus").hide();
    $(".ErrorStatus").hide();


    function SetearCantidad(p1, p2,controlSubfix)
    {
        var KEYVALUE = $("#KEY").val();
        var TextBoxName = "#TB" + controlSubfix;
        var SelectName = "#SL" + controlSubfix;
        var OkMessage = "#Ok" + controlSubfix;
        var ErrorMessage = "#Error" + controlSubfix;
        var Cell = "#CantCell" + controlSubfix;
        var cantidad = $(TextBoxName).val();
        var unidad = $(SelectName + " option:selected").val();
        var result;
        var p_UID = "UID=" + p1;
        var p_PID = "PID=" + p2;
        var p_cant = "cant=" + cantidad;
        var request = $.ajax({
            url: "../DesktopModules/Stock/API/ModuleTask/UpdateStock",
            method: "GET",
            data: { UID: p1,PID:p2,cant:cantidad,unit:unidad,key:KEYVALUE},
            dataType: "html",
            cache:false
        });
        
        request.done(function (msg) {

            if (msg == "ok") {
                $(Cell).text(cantidad);
                $(OkMessage).show(1000);
                setTimeout(function () { $(OkMessage).hide(1000); }, 10000);
            } else
            {
                $(ErrorMessage).show(1000);
                setTimeout(function () { $(ErrorMessage).hide(1000); }, 10000);
            }
        });

        request.fail(function (jqXHR, textStatus) {
            $(ErrorMessage).show(1000);
            setTimeout(function () { $(ErrorMessage).hide(1000); }, 10000);
        });
    }

</script>
