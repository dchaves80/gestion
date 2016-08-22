<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Factura2.View" %>
<div id="DatosFactura" runat="server">
    <div>
        <span class="FormLabel">Tipo Factura:</span>
        <asp:DropDownList runat="server" ClientIDMode="Static" ID="LB_TipoFactura" OnSelectedIndexChanged="LB_TipoFactura_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem> Factura A </asp:ListItem>
            <asp:ListItem> Factura B </asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <span class="FormLabel">Señor/es: </span>
        <asp:TextBox ID="txt_senores" runat="server" CssClass="AtroxTextBox"></asp:TextBox>
    </div>
    <div>
        <span class="FormLabel">Domicilio </span>
        <asp:TextBox ID="txt_Domicilio" runat="server" CssClass="AtroxTextBox"></asp:TextBox>
        <span class="FormLabel">Teléfono </span>
        <asp:TextBox ID="txt_Telefono" runat="server" CssClass="AtroxTextBox"></asp:TextBox>
    </div>
    <div>
        <span class="FormLabel">Localidad </span>
        <asp:TextBox ID="txt_Localidad" runat="server" CssClass="AtroxTextBox"></asp:TextBox>
        <span class="FormLabel">Cuit </span>
        <asp:TextBox ID="txt_Cuit" runat="server" CssClass="AtroxTextBox"></asp:TextBox>
    </div>
    <asp:Button Text="Guardar Datos Factura" CssClass="FormButton FirstElement LastElement" runat="server" ID="btn_setearFactura" OnClick="btn_setearFactura_Click" />

    <div runat="server" id="IVA_FacturaA">
        <span>IVA: 
            <asp:RadioButtonList runat="server" ID="Chk_IVA_A" AutoPostBack="true" OnSelectedIndexChanged="Chk_IVA_A_SelectedIndexChanged" >
                <asp:ListItem Text="Resp. Insc."></asp:ListItem>
                <asp:ListItem Text="Resp. No Insc."></asp:ListItem>
            </asp:RadioButtonList>

        </span>
    </div>
    <div runat="server" id="IVA_FacturaB">
        <span>IVA: 
            <asp:RadioButtonList runat="server" ID="Chk_IVA_B" AutoPostBack="true" OnSelectedIndexChanged="Chk_IVA_B_SelectedIndexChanged">
                <asp:ListItem Text="Exento"></asp:ListItem>
                <asp:ListItem Text="Consumidor Final"></asp:ListItem>
                <asp:ListItem Text="Resp. Monotributo"></asp:ListItem>
            </asp:RadioButtonList>
        </span>
    </div>
    <div>
        <span>Condicion de Venta: 
            <asp:RadioButtonList runat="server" ID="Chk_Condicion_Venta" AutoPostBack="true" OnSelectedIndexChanged="Chk_Condicion_Venta_SelectedIndexChanged">
                <asp:ListItem Text="Contado."></asp:ListItem>
                <asp:ListItem Text="Cta. Cte"></asp:ListItem>
            </asp:RadioButtonList>

        </span>
    </div>
    
</div>
<div id="bodyfactura" runat="server">
</div>
<asp:HiddenField ID="hf_Scroll" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hf_ScrollControl" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hf_URL" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hf_fixedfields" runat="server" ClientIDMode="Static"></asp:HiddenField>

<!-- Prueba de offset en total
<asp:HiddenField ID="hf_OffsetTotal" runat="server" ClientIDMode="Static" />
<div style="background-color:yellow">
<span id="prueba" style="background-color:red"> $000000 </span>
<span style="margin-left:60%"> hola que tal?</span>
    </div>-->
<div id="placeholder_totales" runat="server" style="width:100%"> 


</div>
<div runat="server" id="div_GuardarFactura">
    <span class="FormLabel">Vendedor:</span><asp:DropDownList runat="server" ID="cmb_Vendedor"></asp:DropDownList><br />
    <asp:Button ID="GuardarFactura" runat="server" Text="Guardar Factura" OnClick="GuardarFactura_Click" CssClass="FormButton FirstElement LastElement"/>
</div>

<script>

    var scrollme = $("#hf_Scroll");
    var ControlScrollName = $("#hf_ScrollControl").val();
    var hf_URL = $("#hf_URL").val();
    var ValuesToFix = $("#hf_fixedfields").val().split(",");

    /*var select_tipofactura = $("#LB_TipoFactura");
    select_tipofactura.selectmenu();*/

    if ($("#hf_fixedfields").val() != "")
    {
        for (a = 0; a < ValuesToFix.length; a++)
        {
            fixField(ValuesToFix[a]);
           
            
        }
    }


    function fixField(namefield)
    {
        if (namefield != "") {
            var fixtop = $("#" + namefield).offset().top;
            var hf_totalcolumn = $("#column_total").offset().left;
            var hf_totalcolumn_width = $("#column_total").width();
            $("#" + namefield).offset({ top: fixtop, left: hf_totalcolumn });
            $("#" + namefield).width(hf_totalcolumn_width);
        }

    }


    /* Tratamiento de offset campo de valoor en pesos en total
    var hf_OffsetTotal = $("#hf_OffsetTotal");
    var hf_totalcolumn = $("#column_total");
    hf_OffsetTotal.val(hf_totalcolumn.offset().left);
    var topoffset = $("#prueba").offset().top;
    $("#prueba").offset({ top: topoffset, left: hf_totalcolumn.offset().left});
    $("#prueba").width(hf_totalcolumn.width());
    */


    function KP(E, ButtonName, TextBoxName, K) {
        if (E.which == "13") {
            setCant(ButtonName, TextBoxName, K);
        }
        return false;

    }

    function setCant(ButtonName, TextBoxName, K) {
        var valtxt = $(TextBoxName).val();
        window.location.href = hf_URL + "?setcant=" + valtxt + "&k=" + K;
    }



    function scroll() {
        if (scrollme.val() == "1") {
            $('html, body').animate({
                scrollTop: $(ControlScrollName).offset().top + 'px'
            }, 500);
        }
    }
    scroll();
</script>
