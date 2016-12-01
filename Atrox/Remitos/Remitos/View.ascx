<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Remitos.View" %>
<div>
<asp:Button id="NewRemito" CssClass="FormButton" Text="Nuevo Remito" runat="server" OnClick="NewRemito_Click"/> 
    </div>
<div id="FormRemito" runat="server">
     <div >
           <span class="FormLabel">Proveedor:</span>
           <asp:DropDownList runat="server" id="cmb_Providers" ClientIDMode="Static" OnSelectedIndexChanged="cmb_Providers_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
     </div>
     <div>
           <span class="FormLabel">Nro Remito:</span>
           <asp:TextBox ID="txt_numeroremito" runat="server" ClientIDMode="Static" CssClass="AtroxTextBox"></asp:TextBox>
     </div>
    <div>
        <table style="width:100%">
            <tbody runat="server" id="tb_Detalle" class="detailfactura">
                <tr class="metroheader">
                    <td>Borrar</td>
                    <td>Detalle</td>
                    <td>Cantidad</td>
                    <td>Precio</td>
                    <td>Total</td>
                </tr>
            </tbody>
        </table>
    </div>
    <div>
    <asp:Button id="btnSearchByDesc" CssClass="FormButton" Text="Buscar por descripción" runat="server" OnClientClick="openSearcher('de');return false"/> 
    <asp:Button id="btnSearchByCode" CssClass="FormButton" Text="Buscar por código interno" runat="server" OnClientClick="openSearcher('ci');return false"/> 
    <asp:Button id="btnSearchByBarCode" CssClass="FormButton" Text="Buscar por código de barra" runat="server" OnClientClick="openSearcher('cb');return false"/> 
    </div>
        <div>
    <asp:Button id="btnCancelarFactura" CssClass="FormButton" Text="Cancelar Remito" runat="server" OnClick="btnCancelarFactura_Click"/> 
    <asp:Button id="btnAceptarFactura" CssClass="FormButton" Text="Aceptar Remito" runat="server" OnClick="btnAceptarFactura_Click"/> 

    </div>
</div>
<div style="width:100%;background-color:#0094ff;color:#fff">
    Listado de remitos:
</div>
<div>
    <table style="width:100%">
        <tbody runat="server" id="tbl_ListadoFacturas" class="detailfactura">
            <tr class="metroheader">
                <td>Nro. Remito</td>
                <td>Proveedor</td>
                <td>Fecha</td>
                <td>Total</td>
                <td>Mostrar</td>
            </tr>
        </tbody>
    </table>
</div>
<div id="searchwindow">
    <div>
        <span class="FormLabel">Busqueda:</span>
        <asp:TextBox ID="txtSearcher" runat="server" ClientIDMode="Static" CssClass="AtroxTextBox"></asp:TextBox>
    </div>
    <div>
         <span class="FormLabel">Cantidad:</span>
        <asp:TextBox ID="txtCantidad" runat="server" ClientIDMode="Static" CssClass="AtroxTextBoxMount"></asp:TextBox>
    </div>
    
    <div style="width: auto; display: inline-block; max-height: 100px; overflow-y: scroll">
         <table>
            <tbody id="tableresults">
                <tr class="metroheader">
                    <td>
                        Código.Int.
                    </td>
                    <td>
                        Código.Bar.
                    </td>
                    <td>
                        Descripción
                    </td>
                    <td>
                        Precio Lista
                    </td>
                    <td>
                        Seleccionar
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div>
        <asp:Button runat="server" id="btnCerrar" Text="Cerrar" ClientIDMode="Static" OnClientClick="closeSearcher();return false;" />
        

    </div>
</div>
<asp:HiddenField ID="hf_key" runat="server" ClientIDMode="Static"/>
<script type="text/javascript">
    var searchcondition = 'de';
    var Searcher = $('#searchwindow').dialog(
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
    function openSearcher(condition)
    {
        searchcondition = condition;
        Searcher.dialog('open');
    }
    function closeSearcher() {
        Searcher.dialog('close');
    }

    function search(searchChain) {
        var key = $('#hf_key').val();
        var prov = $('#cmb_Providers').val();
        if (prov==undefined)
        {
            prov='-1';
        }
        $.ajax({
            url: '/DesktopModules/Facturacion3/API/ModuleTask/SA',
            cache: false,
            data: { k: key, ss: searchChain, sc:searchcondition,ip:prov },
            dataType: 'json',
            method: 'GET',
            success: function (data) {
                $("#tableresults").find(".resultline").remove();
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


                            addRow(row.CodigoInterno, row.CodigoBarra, row.Descripcion, row.PrecioCompra, classtopass, row.Id);
                        }

                    }
                }
            },
            error: function () {
                $("#tableresults").find(".resultline").remove();
            }

        });
    }

    function addRow(I, C, D, PF, CLS, ID) {

        if ($('#txtCantidad').val() == undefined) {
            alert("Debe ingresar una cantidad");
        } else {

            var CLSString = 'animationline resultline ' + CLS;
            var button = '<div class="buttoncell" onclick="IncludeArt(' + ID + ')">Seleccionar</div>';
            $('#tableresults').append('<tr id="row' + ID + '" class="' + CLSString + '"><td class="fixcell">' + I + '</td><td  class="fixcell">' + C + '</td><td class="descripcionarticulo fixcell">' + D + '</td><td class="valuepiedefactura fixcell">$' + PF.toFixed(2) + '</td><td class="fixcell">' + button + '</td></tr>');
        }
    }


    function BorrarArt(IDArt)
    {
        var url = $('#url').val();
        window.location.href = "/MyManager/Remitos?delart=" + IDArt;
    }

    function IncludeArt(IDart) {
        var url = $('#url').val();
        window.location.href = "/MyManager/Remitos?addart=" + IDart + "&cant=" + $('#txtCantidad').val();
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
                $("#tableresults").find(".resultline").remove();
            }
        }
        if (counter > 1) {
            counter = 2;
        }

    }, 1800);

</script>