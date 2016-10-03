<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Clientes.View" %>
<div>

    <div>
        <asp:Button Text="Agregar cliente" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" runat="server" ID="btnAgregarCliente" OnClientClick="ShowABM();return false;" />
        <asp:Button Text="Cancelar" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" runat="server" ID="btnCancelar" OnClientClick="HideABM();return false;" />
        <asp:Button Text="Guardar" ClientIDMode="Static" CssClass="FormButton FirstElement LastElement" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" OnClientClick="Guardar()"/>
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
                <span class="FormLabel">Limite De Credito:</span>
                <asp:TextBox placeholder="0" ID="txt_limite" runat="server" ClientIDMode="Static" CssClass="AtroxTextBoxMount"></asp:TextBox>
            </div>
            <div class="DivMobileFix">
                <span class="FormLabel">Suspendida:</span>
                <asp:CheckBox ID="chk_Suspendida" runat="server" ClientIDMode="Static" CssClass="AtroxTextBoxMount"></asp:CheckBox>
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
   
    <div style="display:inline-block">
        <span class="FormLabel">Buscar cliente:</span>
        <asp:TextBox runat="server" ID="txtBuscar" ClientIDMode="Static" CssClass="AtroxTextBox"></asp:TextBox>
        <asp:Button runat="server" ID="btnBuscar" ClientIDMode="Static" CssClass="FormButton FirstElement Lastelement" Text="Buscar" OnClientClick="Search();return false;" />
    </div>



    <div id="_ProgressBarClientes">
    </div>
    

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HF_Host" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HF_RawHost" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="K" />
    <asp:HiddenField runat="server" ClientIdMode="Static" ID="HF_EU" />





</div>

<div id="ClientTableSearch">
        <table style="width:100%">
            <tbody id="CSR">
                <tr class="metroheader">
                <th>
                    Razon Social
                </th>
                <th>
                    Localidad
                </th>
                <th>
                    Domicilio
                </th>
                <th>
                    E-mail
                </th>
                <th>
                    Editar
                </th>
                </tr>
            </tbody>
        </table>
</div>



<script>
    var RawHost = $('#HF_RawHost').val();
    var MyHost = $('#HF_Host').val();
    var K = $('#K').val();
    var Clientes = null;
    $("#ABM_Controls").hide();
    $("#btnCancelar").hide();
    $("#btnGuardar").hide();

    var UIID = $('#HF_EU').val();

    if (UIID != "0")
    {
        ShowABM();
    }

    function ShowABM()
    {
        $("#ABM_Controls").show(250);
        $("#btnCancelar").show(250);
        $("#btnAgregarCliente").hide(250);
        $("#btnGuardar").show(250);

        
    }

    function HideABM() {
        $("#ABM_Controls").hide(250);
        $("#btnCancelar").hide(250);
        $("#btnAgregarCliente").show(250);
        $("#btnGuardar").hide(250);


    }

    var ProgressBar = new ProgressBar.Line('#_ProgressBarClientes',
        {
            strokeWidth: 12,
            //color: '#b3e5fc',
            trailColor: '#b3e5fc',
            trailWidth: 10,
            svgStyle: null,
            text:
                {
                    //color:'#111111',
                    alignToBottom: false

                },

            from: { color: '#f44336' },
            to: { color: '#4caf50' },
            step: function (state, bar, attachment) {
                bar.path.setAttribute('stroke', state.color);
            }
        });
    /*ProgressBar.text.style.fontFamily = '"Roboto"';
    ProgressBar.text.style.fontSize = '15px';
    ProgressBar.text.style.color = '#4caf50';
    ProgressBar.text.style.fontWeight = 'Bolder';
    $('#_ProgressBarClientes').hide();*/

    function Search() {
        Clientes = null;
        $("#CSR").find(".resultline").remove();
        //$('#_ProgressBarClientes').show('slide',1000);
        var ocuppied = false;
        var bool = false;
        var SS = $('#txtBuscar').val();
        var progress = 0;
        ProgressBar.set(0);
        $.ajax(
            {
                cache: false,
                async: true,
                url: MyHost + 'SC',
                data: { k: K, ss: SS,rnd:Math.random()},
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
                            
                            //ProgressBar.setText(Math.round(progress * 100) + ' %');
                            ocuppied = true;
                            ProgressBar.animate(progress,
                            {
                                duration: 500,
                                easing: 'easeOut'

                            }, function () {
                                ocuppied = false;
                                if (Math.round(progress * 100) == 100) {
                                    //$('#_ProgressBarClientes').hide('slide', 2000);
                                }
                            });
                            //Do something with download progress
                        }
                    }, false);

                    return xhr;
                },
                success: function (MyData) {

                    if (MyData != "null") {
                        InterpeterJason(MyData);
                    } else
                    {
                        Clientes = null;
                    }
                }
            });
    }

    function editclient(idc)
    {
       
        //$("#HF_EU").val("0");
            window.location.href = RawHost + "?IDC=" + idc;

       
    }

    function FillFields(C)
    {
        
    }

    function Guardar()
    {
        $('#HF_EU').val("0");
    }

    function InterpeterJason(DATA)
    {
        
        
        
        Clientes = JSON.parse(DATA);
        var c = "";
        for (i = 0; i < Clientes.length; i++)
        {
            if (c == "par") {
                c = "impar";
            } else
            {
                c = "par";
            }

            var r = "<tr class='animationline resultline metro[pi]line'><td class='descripcionarticulo' style='text-decoration:[td]'>[RS]<span style='color:red'>[Susp]</span></td><td class='fixcell'>[L]</td><td class='fixcell'>[D]</td><td class='fixcell'>[E]</td><td><div class='buttoncell' onclick='editclient([IDC])'>Editar</div></td></tr>"
            r = r.replace("[pi]", c);
            r = r.replace("[RS]", Clientes[i]["RS"]);
            r = r.replace("[L]", Clientes[i]["LOCALIDAD"]);
            r = r.replace("[D]", Clientes[i]["DOMICILIO"]);
            r = r.replace("[E]", Clientes[i]["EMAIL"]);
            r = r.replace("[IDC]", Clientes[i]["ID"]);
            
            if (Clientes[i]["SUSPENDIDA"] == true) {
                r = r.replace("[Susp]", " (Suspendida) ");
                r = r.replace("[td]", "line-through");

            } else
            {
                r = r.replace("[Susp]", " ");
                r = r.replace("[td]", 'none');
            }
            $("#CSR").append(r);

        }

    }

</script>
