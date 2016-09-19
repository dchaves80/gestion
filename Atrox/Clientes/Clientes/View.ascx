<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Clientes.View" %>
<div>

    <div>
        <asp:Button Text="Agregar cliente" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" runat="server" ID="btnAgregarCliente" OnClientClick="ShowABM();return false;" />
        <asp:Button Text="Cancelar" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" runat="server" ID="btnCancelar" OnClientClick="HideABM();return false;" />
        <asp:Button Text="Guardar" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click"/>
        <div id="ABM_Controls">
            <div class="DivMobileFix">
                <span class="FormLabel">Razón social:</span>
                <asp:TextBox ID="txt_razonsocial" runat="server" ClientIDMode="Static" CssClass="AtroxTextBox"></asp:TextBox>
            </div>
            <div class="DivMobileFix">
                <span class="FormLabel">DNI/CUIT/CUIL:</span>
                <asp:TextBox ID="txt_dnicuilcuit" runat="server" ClientIDMode="Static" CssClass="AtroxTextBox"></asp:TextBox>
            </div>

            <div class="DivMobileFix">
                <span class="FormLabel">Pais:</span>
                <asp:DropDownList runat="server" ID="cmbpais" ClientIDMode="Static">
                    <asp:ListItem>Argentina</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="DivMobileFix">
                <span class="FormLabel">Provincia:</span>
                <asp:DropDownList runat="server" ID="cmbprovincia" ClientIDMode="Static">
                </asp:DropDownList>
            </div>

            <div class="DivMobileFix">
                <span class="FormLabel">Localidad:</span>
                <asp:TextBox ID="txt_localidad" runat="server" ClientIDMode="Static" CssClass="AtroxTextBox"></asp:TextBox>
            </div>
            <div class="DivMobileFix">
                <span class="FormLabel">Domicilio:</span>
                <asp:TextBox ID="txt_domicilio" runat="server" ClientIDMode="Static" CssClass="AtroxTextBox"></asp:TextBox>
            </div>
             <div class="DivMobileFix">
                <span class="FormLabel">Email:</span>
                <asp:TextBox ID="txt_email" runat="server" ClientIDMode="Static" CssClass="AtroxTextBox"></asp:TextBox>
            </div>
            <div class="DivMobileFix">
                <span class="FormLabel">Descuento:</span>
                <asp:TextBox placeholder="0" ID="txt_descuento" runat="server" ClientIDMode="Static" CssClass="AtroxTextBoxMount"></asp:TextBox>
            </div>
            <div class="DivMobileFix">
                <span class="FormLabel">Observaciones:</span>
                <asp:TextBox ID="txt_observaciones" runat="server" ClientIDMode="Static" CssClass="AtroxTextBox" TextMode="MultiLine" Columns="20" Rows="10"></asp:TextBox>

            </div>
            <div class="DivMobileFix">
                <span class="FormLabel">Situacion IVA:</span>
                <asp:DropDownList runat="server" ID="cmbsituacion" ClientIDMode="Static">
                    <asp:ListItem>RESPONSABLE INSCRIPTO</asp:ListItem>
                    <asp:ListItem>RESPONSABLE NO INSCRIPTO</asp:ListItem>
                    <asp:ListItem>MONOTRIBUTISTA</asp:ListItem>
                    <asp:ListItem>EXENTO</asp:ListItem>
                    <asp:ListItem>SIN SITUACION ANTE EL IVA</asp:ListItem>
                </asp:DropDownList>
            </div>
            
            <div class="atroxseparator"></div>
        </div>
    </div>

    <div style="float:left">
        <span class="FormLabel">Buscar cliente:</span>
        <asp:TextBox runat="server" ID="txtBuscar" CssClass="AtroxTextBox"></asp:TextBox>
        <asp:Button runat="server" ID="btnBuscar" CssClass="FormButton FirstElement Lastelement" Text="Buscar" OnClientClick="Search();return false;" />
    </div>



    <div id="_ProgressBarClientes">
    </div>

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HF_Host" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="K" />



</div>





<script>

    var MyHost = $('HF_Host').val();
    $("#ABM_Controls").hide();
    $("#btnCancelar").hide();
    $("#btnGuardar").hide();

    function ShowABM()
    {
        $("#ABM_Controls").show(1000);
        $("#btnCancelar").show(1000);
        $("#btnAgregarCliente").hide(1000);
        $("#btnGuardar").show(1000);

        
    }

    function HideABM() {
        $("#ABM_Controls").hide(1000);
        $("#btnCancelar").hide(1000);
        $("#btnAgregarCliente").show(1000);
        $("#btnGuardar").hide(1000);


    }

    var ProgressBar = new ProgressBar.Circle('#_ProgressBarClientes',
        {
            strokeWidth: 12,
            //color: '#b3e5fc',
            trailColor: '#b3e5fc',
            trailWidth: 10,
            svgStyle: null,
            text:
                {
                    //color:'#111111',
                    value: 'TextoPrueba',
                    alignToBottom: false

                },

            from: { color: '#f44336' },
            to: { color: '#4caf50' },
            step: function (state, bar, attachment) {
                bar.path.setAttribute('stroke', state.color);
            }
        });
    ProgressBar.text.style.fontFamily = '"Roboto"';
    ProgressBar.text.style.fontSize = '15px';
    ProgressBar.text.style.color = '#4caf50';
    ProgressBar.text.style.fontWeight = 'Bolder';
    $('#_ProgressBarClientes').hide();

    function Search() {
        $('#_ProgressBarClientes').show('slide', 500);
        var ocuppied = false;
        var bool = false;
        $.ajax(
            {
                cache: false,
                async: true,
                url: 'http://190.105.214.230/DesktopModules/Facturacion3/API/ModuleTask/SA?k=111AAA&ss=%%&sc=de',
                data: { k: '111AA', ss: 'caja', sc: 'de' },
                dataType: 'json',
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();
                    xhr.upload.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = evt.loaded / evt.total;
                            //Do something with upload progress here
                        }
                    }, false);

                    xhr.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {

                            progress = evt.loaded / evt.total;
                            ProgressBar.setText(Math.round(progress * 100) + ' %');
                            ocuppied = true;
                            ProgressBar.animate(progress,
                            {
                                duration: 100,
                                easing: 'easeIn'

                            }, function () {
                                ocuppied = false;
                            });
                            //Do something with download progress
                        }
                    }, false);

                    return xhr;
                },
                success: function (MyData) {
                    $('#_ProgressBarClientes').hide('slide', 500);
                }
            });
    }



</script>
