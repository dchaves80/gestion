<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Products.View" %>
<div>
    <asp:Button runat="server" OnClientClick="ToggleForm();return false;" Class="FormButton FirstElement LastElement" ClientIDMode="Static" Text="Agregar Producto"/>

</div>

<div id="AtroxForm" class="AtroxForm" >
   <div>
       <span class="FormLabel">Proveedor:</span><asp:DropDownList runat="server" id="cmbProveedor"></asp:DropDownList>
   </div>
   <div>
   <span class="FormLabel">Código:</span><asp:TextBox ClientIDMode="Static"  class="AtroxTextBox" type="text" id="txtCodigo" runat="server" />
   </div>
   <div>
   <span class="FormLabel">Código de barra:</span><asp:TextBox ClientIDMode="Static"  class="AtroxTextBox" type="text" id="txtCodigoDeBarra" runat="server" />
   </div>
   <div>
   <span class="FormLabel">Descripcion:</span><asp:TextBox ClientIDMode="Static"  class="AtroxTextBox" type="text" id="txtNombre" runat="server" />
   </div>
   <div>
   <span class="FormLabel">Precio Neto:</span>$<asp:TextBox ClientIDMode="Static"  class="AtroxTextBoxMount TextBoxCurrency" type="text" id="txtPrecioNeto" runat="server" />
   </div>
    <div>
   <span class="FormLabel">Porcentaje IVA:</span><asp:TextBox ClientIDMode="Static"  class="AtroxTextBoxMount TextBoxPercent" type="text" id="txtPorcentajeIVA" runat="server" />%
   </div>
    <div>
   <span class="FormLabel">Precio Compra:</span>$<asp:TextBox ClientIDMode="Static"  class="AtroxTextBoxMount TextBoxCurrency" type="text" id="txtPrecioCompra" runat="server" />
   </div>
    <div>
   <span class="FormLabel">Porcentaje Ganancia:</span><asp:TextBox ClientIDMode="Static"  class="AtroxTextBoxMount TextBoxPercent" type="text" id="txtPorcentajeGanancia" runat="server" />%
   </div>
   <div>
   <span class="FormLabel">Precio Final:</span>$<asp:TextBox ClientIDMode="Static"  class="AtroxTextBoxMount TextBoxCurrency" type="text" id="txtPrecioFinal" runat="server" />
   </div>
   <div>
   <span class="FormLabel">Unidad:</span><asp:DropDownList ClientIDMode="Static" runat="server" ID="cmbUnidades"></asp:DropDownList>
   </div>
   <div id="fileList" runat="server"></div>
    <div>
    <div>
    <asp:Button ID="btnGuardar" runat="server" CssClass="FormButton FirstElement" ClientIDMode="Static" Text="Guardar" OnClientClick="ProcesarEdit()" OnClick="btnGuardar_Click"/>
    <asp:Button OnClientClick="ClearForm();return false;" ID="btnCancelar" runat="server" CssClass="FormButton LastElement" ClientIDMode="Static" Text="Cancelar"/>
     </div>
    </div>
   <!--<asp:FileUpload id="MyFile" runat="server" AllowMultiple="false" />-->
</div>
<asp:HiddenField ID="Mode" Value="None" runat="server" ClientIDMode="Static"/>
<asp:HiddenField ID="IdArt" Value="0"  runat="server" ClientIDMode="Static"/>
<div id="MessageBox" runat="server"></div>
  <div>
            <asp:Button OnClientClick="ShowCargaMasiva();return false;" runat="server" ID="Button1" CssClass="FormButton FirstElement LastElement" ClientIDMode="Static" Text="Carga Masiva"/>
  </div>
<div id="FileUpload"  style="margin-top:20px;margin-bottom:20px;padding: 10px 10px 10px 10px; background-color:#d9d9d9">
     <div>
   <span class="FormLabel">Seleccionar TXT:</span><asp:FileUpload id="FileUploader" runat="server" AllowMultiple="false" />
   </div>
    <div>
        Debe respetar los campos obligatorios en la tabla y exportarla a txt como campos separados por tabulación
        <br />
        Campos Obligatorios: <b>codigo,descripcion,precioneto,iva,preciocompra,porcentajeganancia,preciofinal
</b><br /> [La cabecera de las columnas deben ser escritas en minúscula como estan indicados en los requerimientos]

    </div>
    <div>
    <asp:Button ID="btnSubirArchivo" Text="Subir Archivo" runat="server" Class="FormButton FirstElement LastElement" OnClick="btnSubirArchivo_Click" OnClientClick="ShowUploadProcessing()"/>
    <div Id="MessageBoxUpload" class="ProcessingAlert">Subiendo Archivo...</div>
    </div>
  
    <div id="CargaMasiva">
        <asp:DropDownList runat="server" ID="CmbUpdateProviders" ClientIDMode="Static"></asp:DropDownList>
        <asp:DropDownList runat="server" ID="CmbFileList" ClientIDMode="Static"></asp:DropDownList>
        <asp:Button ID="btnProcesar" Text="Procesar Archivo" runat="server" Class="FormButton FirstElement LastElement" OnClick="btnProcesar_Click" OnClientClick="ShowFileProcessing()"/>
        <div Id="MessageBoxProcessing" class="ProcessingAlert">Procesando Archivo...</div>
        <div>
            <asp:Button OnClientClick="HideCargaMasiva();return false;" runat="server" ID="btnCancelFile" CssClass="FormButton FirstElement LastElement" ClientIDMode="Static" Text="Cancelar"/>
        </div>
    </div>
</div>
<div id="IndiceArticulos" runat="server">

</div>
<div id="ListadoArticulos" runat="server">
        
    </div>

<div id="IndiceNumerico" runat="server">

</div>

<script>

    setTimeout(function () {
        $('.MessageBox').fadeOut(3000);
    }, 5000);


    var txtPrecioNeto = $("#txtPrecioNeto");
    var txtPorcentajeIVA = $("#txtPorcentajeIVA");
    var txtPrecioCompra = $("#txtPrecioCompra");
    var txtPorcentajeGanancia = $("#txtPorcentajeGanancia");
    var txtPrecioFinal = $("#txtPrecioFinal");
    var decimalSeparator;
    
    $("#AtroxForm").hide();
    $("#MessageBoxUpload").hide();
    $("#MessageBoxProcessing").hide();
    $("#FileUpload").hide();


    function ProcesarEdit()
    {
        if ($("#Mode").val() == "edt")
        {
            $("#Mode").val("fedt");
        }

    }

    function ShowCargaMasiva()
    {
        $("#FileUpload").show(1000);
    }

    function HideCargaMasiva() {
        $("#FileUpload").hide(1000);
    }

    function startUploadAnim() {
        var div = $("#MessageBoxUpload");
        div.animate({ opacity: '0.4' }, "fast");
        div.animate({ opacity: '1' }, "fast");
        startUploadAnim();
    }

    

    function startProcessingAnim() {
        var div = $("#MessageBoxProcessing");
        div.animate({ opacity: '0.4' }, "fast");
        div.animate({ opacity: '1' }, "fast");
        startProcessingAnim();
    }

   
    function ShowUploadProcessing()
    {
        $("#MessageBoxUpload").show(100);
        startUploadAnim();
    }

    function ShowFileProcessing() {
        $("#MessageBoxProcessing").show(100);
        startProcessingAnim();
    }
    function HideFileProcessing() {
        $("#MessageBoxProcessing").hide(100);
        startProcessingAnim();
    }


    function ToggleForm()
    {
        $("#AtroxForm").toggle(1000);
    }
    
    function ClearForm()
    {
        txtPrecioNeto.val("");
        txtPorcentajeIVA.val("");
        txtPrecioCompra.val("");
        txtPorcentajeGanancia.val("");
        txtPrecioFinal.val("");
        $("#txtCodigo").val("");
        $("#txtCodigoDeBarra").val("");
        $("#txtNombre").val("");
        $("#AtroxForm").hide(1000);
        $("#Mode").val("None");
        $("IdArt").val("0");
    }



    function StablishSeparator()
    {
        if (isNaN(parseFloat("1.1"))) {
            decimalSeparator = ",";
        } else
        {
            decimalSeparator = ".";
        }
    }

    StablishSeparator();

    function ValidateField(field)
    {
        var MyVal = field.val();
        MyVal = MyVal.replace(",", decimalSeparator);
        MyVal = MyVal.replace(".", decimalSeparator);
        field.val(MyVal);
    }

    
    txtPrecioNeto.keyup(function()
    {
        ValidateField(txtPrecioNeto);
        calcularPrecioCompra();
    });
    txtPorcentajeIVA.keyup(function()
    {
        ValidateField(txtPorcentajeIVA);
        calcularPrecioCompra();
    });

    txtPrecioCompra.keyup(function ()
    {
        ValidateField(txtPrecioCompra);
        calcularPrecioNeto();
    });


    txtPorcentajeGanancia.keyup(function ()
    {
        ValidateField(txtPorcentajeGanancia);
        calcularPrecioFinal();
    });

    txtPrecioFinal.keyup(function ()
    {
        ValidateField(txtPorcentajeGanancia);
        calcularPorcentajeGanancia();

    });


    function calcularPorcentajeGanancia() {
        if (isNumeric(txtPorcentajeGanancia.val()) && isNumeric(txtPrecioFinal.val())) {
          
            var t_preciofinal = parseFloat(txtPrecioFinal.val());
            var t_preciocompra = parseFloat(txtPrecioCompra.val());
            var t_diferencia = t_preciofinal - t_preciocompra;
            var t_porcentaje = ((t_preciofinal * 100) / t_preciocompra)-100;


            txtPorcentajeGanancia.val(t_porcentaje.toFixed(2));
        }
    }

    function calcularPrecioFinal()
    {
        if (isNumeric(txtPrecioCompra.val()) && isNumeric(txtPorcentajeGanancia.val()))
        {
            var t_preciocompra = parseFloat( txtPrecioCompra.val());
            var t_porcentajeganancia =parseFloat( txtPorcentajeGanancia.val());
            var t_multiplicador = t_porcentajeganancia / 100;
            var t_recargatotal = t_preciocompra * t_multiplicador;
            var t_total = t_preciocompra + t_recargatotal;
            txtPrecioFinal.val(t_total.toFixed(2));
        }
    }

    function calcularPrecioNeto()
    {
        if (isNumeric(txtPrecioCompra.val()) && isNumeric(txtPorcentajeIVA.val()))
        {
            var t_preciocompra = txtPrecioCompra.val();
            var t_porcentajeiva = parseFloat(txtPorcentajeIVA.val());
            var t_divisor = 1 + t_porcentajeiva / 100;
            var t_amount = t_preciocompra / t_divisor;
            var t_total = t_amount;
            txtPrecioNeto.val(t_total.toFixed(2));
        }
    }

    function calcularPrecioCompra()
    {
        if (isNumeric(txtPrecioNeto.val()) && isNumeric(txtPorcentajeIVA.val()))
        {
            var t_precioneto = parseFloat( txtPrecioNeto.val());
            var t_porcentajeiva = parseFloat(txtPorcentajeIVA.val());
            var t_amount = t_precioneto * t_porcentajeiva / 100;
            var t_total = t_precioneto + t_amount;
            txtPrecioCompra.val(t_total.toFixed(2));
           
        }
    }
    
   
    var MODO = $("#Mode");

    if (MODO.val() == "edt")
    {
        $("#AtroxForm").show();
        ValidateField(txtPorcentajeGanancia);
        ValidateField(txtPorcentajeIVA);
        ValidateField(txtPrecioCompra);
        ValidateField(txtPrecioFinal);
        ValidateField(txtPrecioNeto);
    }


    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

</script>
