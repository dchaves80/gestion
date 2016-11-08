<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Facturacion3.View" %>
<div>
    <asp:Button Text="Nueva Venta" runat="server" ID="btn_NuevaVenta" CssClass="FormButton FirstElement LastElement" OnClientClick="NewFactura()" OnClick="btn_NuevaVenta_Click" />


    <div id="controlesFactura" runat="server">
        <div>
            <span class="FormLabel">Tipo de Factura</span>
            <asp:DropDownList runat="server" ID="Factura_Tipo" ClientIDMode="Static">
                <asp:ListItem Text="Factura A" Value="A"></asp:ListItem>
                <asp:ListItem Text="Factura B" Value="B" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Factura C" Value="C"></asp:ListItem>
                <asp:ListItem Text="Factura X" Value="X"></asp:ListItem>
                <asp:ListItem Text="Presupuesto" Value="P"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div runat="server" id="ControlesFacturaA">

            <table>
                <tr>
                    <td>
                        <div>
                            <span class="FormLabel">Nombre</span>
                            <asp:TextBox runat="server" CssClass="AtroxTextBox" ID="txt_Nombre" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div>
                            <span class="FormLabel">Domicilio</span>
                            <asp:TextBox runat="server" CssClass="AtroxTextBox" ID="txt_Domicilio" ClientIDMode="Static"></asp:TextBox>
                        </div>

                    </td>

                    <td>
                        <div>
                            <span class="FormLabel">Localidad</span>
                            <asp:TextBox runat="server" CssClass="AtroxTextBox" ID="txt_Localidad" ClientIDMode="Static"></asp:TextBox>
                        </div>

                        <div>
                            <span class="FormLabel">Teléfono</span>
                            <asp:TextBox runat="server" CssClass="AtroxTextBox" ID="txt_Telefono" ClientIDMode="Static"></asp:TextBox>
                        </div>

                        <div>
                            <span class="FormLabel">Cuit</span>
                            <asp:TextBox runat="server" CssClass="AtroxTextBox" ID="txt_CUIT" ClientIDMode="Static"></asp:TextBox>
                        </div>

                    </td>

                </tr>

            </table>




        </div>

        <div id="IIVAA">
            <span class="FormLabel">IVA:</span>
            <asp:DropDownList runat="server" ClientIDMode="Static" ID="IVAFA">
                <asp:ListItem Text="Responsable Inscripto" Value="RI"></asp:ListItem>
                <asp:ListItem Text="Responsable No Inscripto" Value="RNI"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id="IIVAB">
            <span class="FormLabel">IVA:</span>
            <asp:DropDownList runat="server" ClientIDMode="Static" ID="IVAFB">
                <asp:ListItem Text="Exento" Value="E"></asp:ListItem>
                <asp:ListItem Text="Consumidor Final" Value="CF"></asp:ListItem>
                <asp:ListItem Text="Responsable Monotributo" Value="RM"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <span class="FormLabel">Forma de pago:</span>
            <asp:DropDownList runat="server" ClientIDMode="Static" ID="cmbFormaPago">
                <asp:ListItem Text="Contado" Value="C"></asp:ListItem>
                <asp:ListItem Text="Cuenta Corriente" Value="CC"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div style="background-color:#78cc96;margin-top:10px;margin-bottom:10px" id="DivBusquedaCliente">
            <span class="FormLabel" style="font-size:15px">Busqueda de Cliente</span>
            <div>
                <span class="FormLabel">Buscar:</span>
                <asp:TextBox runat="server" CssClass="AtroxTextBox" ID="txtBusquedaCliente" ClientIDMode="Static"></asp:TextBox>
                <asp:Button Text="Buscar Cliente" runat="server" ID="btnBuscarCliente" CssClass="FormButton FirstElement LastElement" OnClientClick="SearchClient(); return false;"/>
            </div>
            <span class="FormLabel">Selecciona Cliente:</span>
            <select id="ClientsId">

            </select>
        </div>

        <div runat="server" id="VisualizadorFactura">
            <table style="width: 100%" id="mytable">
                <tbody class="detailfactura" id="detailfactura" runat="server">
                    <tr class="metroheader">
                        <td>Borrar.</td>
                        <td>Cant.</td>
                        <td>Detalle</td>
                        <td>Prec. Uni.</td>
                        <td>Total</td>
                        <td class="detailwithiva">%IVA</td>
                        <td class="detailwithiva">Total IVA</td>
                    </tr>
                </tbody>
            </table>

        </div>
        <div>
            <input value="Buscar por Descripcion" type="button" class="FormButton FirstElement" onclick="OpenSearcher('de')" />
            <input value="Buscar por Cod. Interno" type="button" class="FormButton" onclick="OpenSearcher('ci')" />
            <input value="Buscar por Cod. Barra" type="button" class="FormButton LastElement" onclick="OpenSearcher('cb')" />
        </div>
        <div>
            <span class="FormLabel">Vendedor:</span>
            <asp:DropDownList ID="cmbVendedor" runat="server" ClientIDMode="Static">
            </asp:DropDownList>
        </div>
        <asp:Button Text="Aceptar Venta" runat="server" ID="btn_AceptarVenta" CssClass="FormButton FirstElement LastElement" OnClientClick="AceptarVenta()" OnClick="btn_AceptarVenta_Click" />
        <div>

            <asp:Button Text="Cancelar Venta" runat="server" ID="btn_CancelarVenta" CssClass="FormButton FirstElement LastElement" OnClick="btn_CancelarVenta_Click" OnClientClick="CancelarVenta()"></asp:Button>

        </div>
    </div>
    <div id="messagebox" runat="server"></div>
</div>



<div title="Busqueda de articulos" id="popupsearcher">
    <div>
        <span class="FormLabel">Cantidad:</span>
        <asp:TextBox CssClass="AtroxTextBoxMount" ID="TxtCantidad" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div>
        <span class="FormLabel">Buscar:</span>
        <asp:TextBox CssClass="AtroxTextBox" ID="txtSearcher" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div style="width: auto; display: inline-block; max-height: 100px; overflow-y: scroll">
        <table style="table-layout: fixed;">
            <tbody id="Results">
                <tr class="metroheader">
                    <td>Cod. Interno</td>
                    <td>Cod. Barra</td>
                    <td>Descripcion</td>
                    <td>Precio Final</td>
                    <td>Seleccionar</td>
                </tr>
            </tbody>
        </table>
    </div>
    <div>
        <asp:Button ID="btnSalir" runat="server" ClientIDMode="Static" Text="Cerrar" CssClass="FormButton FirstElement LastElement" OnClientClick="CloseSearcher();return false;" />
    </div>

</div>
<asp:HiddenField runat="server" ID="key" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="baseurl" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="url" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="searchcondition" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="detail" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="ivaslist" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="cantidades" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="mykeys" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="erasef" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="IdCliente" ClientIDMode="Static" />
<script>

    function AceptarVenta()
    {
        if (Cookies.get('cookie_idcliente') != undefined) {
            var MCID = Cookies.get('cookie_idcliente');
            $("#IdCliente").val(MCID);

        } else
        {
            $("IdCliente").val("0");
        }

        
    }

    function NewFactura() {
        Cookies.set('cookie_condicioniva', 'E');
        Cookies.set('cookie_tipodefactura', 'B');
        Cookies.set('cookie_nombre', '');
        Cookies.set('cookie_domicilio', '');
        Cookies.set('cookie_localidad', '');
        Cookies.set('cookie_telefono', '');
        Cookies.set('cookie_CUIT', '');
        Cookies.set('cookie_vendedor', '0');
        Cookies.set('cookie_formapago', 'C');
        Cookies.set('cookie_idcliente', '0');
        $("#IdCliente").val("0");

    }

    var _TIPODEFACTURA;
    $("#DivBusquedaCliente").hide();
   

    function SimboloDecimal(value) {
        var decimalseparator = '';
        var t = (1 / 2).toString();
        if (t.indexOf(".") >= 0) {
            decimalseparator = ". punto";
        } else {
            decimalseparator = ", coma";
        }
        alert(decimalseparator);


    }


    function fixSymbol(value) {

        if (value != undefined) {

            var decimalseparator = '';
            var t = (1 / 2).toString();
            if (t.indexOf(".") >= 0) {
                decimalseparator = ".";
            } else {
                decimalseparator = ",";
            }
            var process1 = value.replace('.', decimalseparator);
            var process2 = value.replace(',', decimalseparator);
            return parseFloat(process2);
        }

    }


    var counter = 0;
    var MyKey = $('#key').val();
    var Searcher = $('#popupsearcher').dialog(
        {
            autoOpen: false,
            closeOnEscape: false,
            dialogClass: "noclose",
            modal: true,
            resizable: false,
            draggable: false,
            width: 'auto'

        }
        );
    function CloseSearcher() {
        Searcher.dialog('close');
    }
    function OpenSearcher(columnToSearch) {
        $("#searchcondition").val(columnToSearch);
        Searcher.dialog('open');
    }


    $('#txtSearcher').keyup(function (event) {

        counter = 0;
    })
    window.setInterval(function () {

        counter = counter + 1;
        if (counter == 1) {
            counter = 2;
            var value = $('#txtSearcher').val();
            if (value != '') {
                search(value);
            } else {
                $("#Results").find(".resultline").remove();
            }
        }
        if (counter > 1) {
            counter = 2;
        }

    }, 1800);

    $('#txt_Nombre').keyup(function (event) {
        Cookies.set("cookie_nombre", $('#txt_Nombre').val());
    });

    $('#txt_Domicilio').keyup(function (event) {
        Cookies.set("cookie_domicilio", $('#txt_Domicilio').val());
    });

    $('#txt_Localidad').keyup(function (event) {
        Cookies.set("cookie_localidad", $('#txt_Localidad').val());
    });

    $('#txt_Telefono').keyup(function (event) {
        Cookies.set("cookie_telefono", $('#txt_Telefono').val());
    });

    $('#txt_CUIT').keyup(function (event) {
        Cookies.set("cookie_CUIT", $('#txt_CUIT').val());
    });

    $("#cmbFormaPago").change(function () {
        if ($("#cmbFormaPago").val() == "CC") {
            $("#DivBusquedaCliente").show();
            Cookies.set("cookie_idcliente", $("#ClientsId").val())
        } else {
            Cookies.set("cookie_idcliente", "0");
            $("#DivBusquedaCliente").hide();
        }
    });

    $("#ClientsId").change(function () {
            Cookies.set("cookie_idcliente", $("#ClientsId").val())
    });



    $('#Factura_Tipo').on('change', function () {
        changeFactura(0);
    });

    $('#IVAFA').on('change', function () {
        changeCondition();
    });

    $('#IVAFB').on('change', function () {
        changeCondition();
    });

    $('#cmbFormaPago').on('change', function () {
        Cookies.set("cookie_formapago", $('#cmbFormaPago').val())
    });


    $('#cmbVendedor').on('change', function () {
        Cookies.set("cookie_vendedor", $('#cmbVendedor').val())
    });




    function changeCondition() {
        if ($('#Factura_Tipo').val() == 'A') {
            Cookies.set('cookie_condicioniva', $('#IVAFA').val());
        } else {
            Cookies.set('cookie_condicioniva', $('#IVAFB').val());
        }
    }

    function changingcant(key) {
        var totaliva = parseFloat(fixSymbol($('#puiva_' + key).text()) * fixSymbol($('#input_' + key).val())).toFixed(2);
        var totalnoiva = parseFloat(fixSymbol($('#punoiva_' + key).text()) * fixSymbol($('#input_' + key).val())).toFixed(2);
        $('#totaliva_' + key).text(totaliva);
        $('#totalnoiva_' + key).text(totalnoiva);
        //iva total
        var puiva = parseFloat(fixSymbol($('#ivaunitario_' + key).text())).toFixed(2);
        var cant = fixSymbol($('#input_' + key).val());
        var totiva = puiva * cant;
        $('#ivadetailtotal_' + key).text(parseFloat(totiva).toFixed(2));
        var elements = $('#mytable').find('.totalnoiva');
        var total = 0;
        for (var a = 0; a < elements.length; a++) {
            total = total + fixSymbol($(elements[a]).text());
        }
        $('#cell_value_subtotal').text(parseFloat(total).toFixed(2));

        var cantivas = $("#ivaslist").val().split('!').length;
        for (a = 0; a < cantivas; a++) {
            var elementswithivas = $('#mytable').find('.ivaindexvalue' + a);
            var totaliva = 0;
            for (b = 0; b < elementswithivas.length; b++) {
                totaliva = totaliva + fixSymbol($(elementswithivas[b]).text());
            }
            $('#ivaindex' + a).text(parseFloat(totaliva).toFixed(2));
        }

        var totalesproductos = $('#mytable').find('.totaliva');
        var result = 0;
        for (a = 0; a < totalesproductos.length; a++) {
            var valuetotalproducto = fixSymbol($(totalesproductos[a]).text());
            result = result + valuetotalproducto;
        }
        $('#finaltotal').text(parseFloat(result).toFixed(2));


    }

    function changeFactura(fromrecharge) {
        Cookies.set('cookie_tipodefactura', $('#Factura_Tipo').val());
        if ($('#Factura_Tipo').val() == 'A') {
            $('#ControlesFacturaA').show(500);
            $('.punoiva').show(500);
            $('.puiva').hide(500);
            $('.totalnoiva').show(500);
            $('.totaliva').hide(500);
            $('.detailwithiva').show(500);
            $('#IIVAA').show(500);
            $('#IIVAB').hide(500);
            $('#IVAFA').val('RI');
            if (fromrecharge == 0) {
                $('#IVAFA').val('RI');
                Cookies.set('cookie_condicioniva', $('#IVAFA').val());
            } else {
                $('#IVAFA').val(Cookies.get('cookie_condicioniva'));
            }

        } else {
            $('#ControlesFacturaA').hide(500);
            $('.punoiva').hide(500);
            $('.puiva').show(500);
            $('.totalnoiva').hide(500);
            $('.totaliva').show(500);
            $('.detailwithiva').hide(500);
            $('#IIVAA').hide(500);
            $('#IIVAB').show(500);
            $('#IVAFB').val('E');
            if (fromrecharge == 0) {
                $('#IVAFA').val('E');
                Cookies.set('cookie_condicioniva', $('#IVAFB').val());
            } else {
                $('#IVAFB').val(Cookies.get('cookie_condicioniva'));
            }
        }

    }

    function FillComboBoxCustomers(DATA)
    {
        Cookies.set("cookie_idcliente", "0");
        Clientes = JSON.parse(DATA);
        for (a = 0; a < Clientes.length; a++)
        {
            if (a==0)
            {
                Cookies.set("cookie_idcliente",Clientes[a].ID.toString());
            }
            
            $("#ClientsId").append("<option value='" + Clientes[a].ID + "'>" + Clientes[a].RS + "(" + Clientes[a].DNI + ")" + "</option>");

        }
    }

    function FillComboBoxSingleCustomer(DATA) {
        Cookies.set("cookie_idcliente", "0");
        Cliente = JSON.parse(DATA);
        
                Cookies.set("cookie_idcliente", Cliente.ID.toString());
        

                $("#ClientsId").append("<option value='" + Cliente.ID + "'>" + Cliente.RS + "(" + Cliente.DNI + ")" + "</option>");
        

        
    }

    

    function SearchSingleClient()
    {
        $.ajax(
            {
                url: $('#baseurl').val() + '/DesktopModules/Clientes/API/ModuleTask/SSC',
                data: { k: MyKey, rnd: 100, idc: Cookies.get('cookie_idcliente') },
                datatype: 'json',
                method: 'GET',
                success: function (data)
                {
                    $("#ClientsId").find("option").remove();
                    Clientes = JSON.parse(data);
                    FillComboBoxSingleCustomer(data);
                },
                error: function ()
                {
                   
                }
            });
    }

    function SearchClient()
    {
        $.ajax(
            {
                url: $('#baseurl').val() + '/DesktopModules/Clientes/API/ModuleTask/SC',
                data: { k: MyKey, rnd: 100, ss: $("#txtBusquedaCliente").val() },
                dataType: 'json',
                method: 'GET',
                success: function (data)
                {
                    $("#ClientsId").find("option").remove();
                    Clientes = JSON.parse(data);
                    FillComboBoxCustomers(data);
                    //Todo bien

                },
                error: function ()
                {
                    //error
                }

            });
    }

    function search(searchChain) {
        $.ajax({
            url: $('#baseurl').val() + '/DesktopModules/Facturacion3/API/ModuleTask/SA',
            cache: false,
            data: { k: MyKey, ss: searchChain, sc: $('#searchcondition').val() },
            dataType: 'json',
            method: 'GET',
            success: function (data) {
                $("#Results").find(".resultline").remove();
                if (data != 'null') {
                    var jsonobj = JSON.parse(data);
                    if (jsonobj.length != 0) {

                        var par = true;
                        var classtopass;
                        for (a = 0; a < jsonobj.length; a++) {
                            var row = jsonobj[a];
                            if (par == true) {
                                par = false;
                                classtopass = 'metroparline';
                            } else {
                                par = true;
                                classtopass = 'metroimparline';
                            }


                            addRow(row.CodigoInterno, row.CodigoBarra, row.Descripcion, row.PrecioFinal, classtopass, row.Id);
                        }

                    }
                }
            },
            error: function () {
                $("#Results").find(".resultline").remove();
            }

        });
    }


    function addRow(I, C, D, PF, CLS, ID) {
        var CLSString = 'animationline resultline ' + CLS;
        var button = '<div class="buttoncell" onclick="IncludeArt(' + ID + ')">Seleccionar</div>';
        $('#Results').append('<tr id="row' + ID + '" class="' + CLSString + '"><td class="fixcell">' + I + '</td><td  class="fixcell">' + C + '</td><td class="descripcionarticulo fixcell">' + D + '</td><td class="valuepiedefactura fixcell">$' + PF.toFixed(2) + '</td><td class="fixcell">' + button + '</td></tr>');
    }

    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    function IncludeArt(IDart) {
        var url = $('#url').val();
        window.location.href = url + "?addart=" + IDart + "&cant=" + $('#TxtCantidad').val();
    }



    if (Cookies.get('cookie_tipodefactura') != undefined) {
        $('#Factura_Tipo').val(Cookies.get('cookie_tipodefactura'));
    }

    if (Cookies.get('cookie_nombre') != undefined) {
        $('#txt_Nombre').val(Cookies.get('cookie_nombre'));
    }

    if (Cookies.get('cookie_domicilio') != undefined) {
        $('#txt_Domicilio').val(Cookies.get('cookie_domicilio'));
    }

    if (Cookies.get('cookie_localidad') != undefined) {
        $('#txt_Localidad').val(Cookies.get('cookie_localidad'));
    }

    if (Cookies.get('cookie_telefono') != undefined) {
        $('#txt_Telefono').val(Cookies.get('cookie_telefono'));
    }

    if (Cookies.get('cookie_CUIT') != undefined) {
        $('#txt_CUIT').val(Cookies.get('cookie_CUIT'));
    }

    if (Cookies.get('cookie_vendedor') != undefined) {
        $('#cmbVendedor').val(Cookies.get('cookie_vendedor'));
    }

    if (Cookies.get('cookie_formapago') != undefined) {
        $('#cmbFormaPago').val(Cookies.get('cookie_formapago'));
        if ($('#cmbFormaPago').val() == "CC") {
            $("#DivBusquedaCliente").show();
            if (Cookies.get('cookie_idcliente') != undefined) {
                SearchSingleClient();
                $('#ClientsId').val(Cookies.get('cookie_idcliente'));
            }
        } else
        {
            $("#DivBusquedaCliente").hide();
        }
        
    }


   




    if (Cookies.get('cookie_condicioniva') != undefined) {
        if (Cookies.get('cookie_tipodefactura') == 'A') {
            $('#IVAFA').val(Cookies.get('cookie_condicioniva'));
        } else {
            $('#IVAFB').val(Cookies.get('cookie_condicioniva'));
        }
    }

    changeFactura();


    var splitkeys = $("#mykeys").val().split("!");
    for (a = 0; a < splitkeys.length; a++) {
        changingcant(splitkeys[a]);
    }

    function BA(p_key) {
        var url = $('#url').val();
        window.location.href = url + "?ba=" + p_key;
    }

    function CancelarVenta() {

        Cookies.remove('cookie_condicioniva');
        Cookies.remove('cookie_tipodefactura');
        Cookies.remove('cookie_nombre');
        Cookies.remove('cookie_domicilio');
        Cookies.remove('cookie_localidad');
        Cookies.remove('cookie_telefono');
        Cookies.remove('cookie_CUIT');
        Cookies.remove('cookie_vendedor');
        Cookies.remove('cookie_formapago');
        Cookies.remove('cookie_idcliente');
        
        

    }

    function redirect()
    {
        window.location.href = url;
    }

    if ($('#erasef').val() != undefined && $('#erasef').val() == '1') {
        CancelarVenta();
        var url = $('#url').val();
        $("#controlesFactura").hide();
        setTimeout(redirect, 3000);
       
    }

</script>
