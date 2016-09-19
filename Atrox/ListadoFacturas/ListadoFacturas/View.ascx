<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.ListadoFacturas.View" %>

<div>
    <div class="ResponsiveDiv">
        <span class="FormLabel">Fecha desde:</span>
        <asp:TextBox data-inline="true" runat="server" ID="txt_fechadesde" ClientIDMode="Static" autocomplete="off"></asp:TextBox>  
    </div>
    <div class="ResponsiveDiv">
        <span class="FormLabel">Fecha hasta:</span>
        <asp:TextBox data-inline="true" runat="server" ID="txt_fechahasta" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
    </div>
    <div class="ResponsiveDiv">
        <span class="FormLabel">Tipo de Factura:</span>
        <asp:DropDownList runat="server" ID="cmb_TipoComprobante">
            <asp:ListItem Text="Todas" Value="0"/>
            <asp:ListItem Text="Factura A" Value="A" />
            <asp:ListItem Text="Factura B" Value="B" />
            <asp:ListItem Text="Factura B" Value="C" />
        </asp:DropDownList><br />
        <asp:Button runat="server" Text="Buscar Facturas" ID="btnBuscar" CssClass="FormButton FirstElement LastElement" onclick="btnBuscar_Click"/>
    </div>


    <div class="ResponsiveDiv">
        <canvas id="myChart"></canvas>
    </div>

    <div runat="server" id="ListadoFacturas" class="ResponsiveDiv LimitedHeightList">
    </div>
    <div runat="server" Id="Det" class="ResponsiveDiv LimitedHeightList">
        <div>
        <table style="width:100%">
            <tbody>
                <tr><td>Fecha:</td><td runat="server" id="field_date">[Place Holder Fecha]</td></tr>
                <tr runat="server" id="row_name"><td>Señore/s:</td><td colspan="2" runat="server" id="field_name">[Place Holder Razon Social]</td></tr>
                <tr runat="server" id="row_domi"><td>Domicilio:</td><td colspan="2" runat="server" id="field_domi">[Place Holder Domicilio]</td></tr>
                <tr runat="server" id="telefono"><td>Telefono:</td><td colspan="2" runat="server" id="field_phone">[Place Holder Telefono]</td></tr>
                <tr ><td>Sit. Iva:</td><td runat="server" id="field_iva">[Place Holder IVA]</td><td>Forma de pago:</td><td runat="server" id="field_pay">[Place Holder Pago]</td></tr>
            </tbody>
        </table>
        <table style="width:100%">
            <tbody runat="server" id="Table_detail">
                <tr class="metroheader"><td>Cant.</td><td>Descripcion</td><td>Precio U.</td><td>Total</td></tr>
                
            </tbody>
        </table>
            </div>
    </div>
</div>
<asp:HiddenField ID="urlbase" ClientIDMode="Static" runat="server" value=""/>
<asp:HiddenField ID="HF_Data" ClientIDMode="Static" runat="server" Value="" />
<asp:HiddenField ID="HF_DataCant" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="HF_DataColors" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="HF_DataTitle" ClientIDMode="Static" runat="server" />
<script>

    $('#txt_fechadesde').datepicker({
        dateFormat: 'dd/mm/yy'
    }
        );

    $('#txt_fechahasta').datepicker({
        dateFormat: 'dd/mm/yy'
    }
        );
    

    function OpenC(key)
    {
        
        window.location.href = $('#urlbase').val() + "?VC=" + key;
    }
    var A_labels = new Array();
    var A_data = new Array();
    var A_colors = new Array();
    var chartinvar = $('#myChart');
    var A_title;

    if ($('#HF_Data').val() == '1')
    {
        GetCants();
        GetColors();
        GetTitle();
        $('html, body').animate({
            scrollTop: $("#myChart").offset().top
        }, 1);
    }

    function GetTitle()
    {
        A_title = $('#HF_DataTitle').val();
    }

    function GetCants()
    {
        var cants = $('#HF_DataCant').val().split(',');
        for (a=0;a<cants.length;a++)
        {
            A_data.push(parseInt(cants[a]));
        }

    }

    function GetColors() {
        var cants = $('#HF_DataColors').val().split(',');
        for (a = 0; a < cants.length; a++) {
            A_colors.push(parseInt(cants[a]));
        }

    }

    
    A_labels.push('Fact. A', 'Fact. B', 'Fact. C');
    


    var data = {
        labels: A_labels,
        datasets: [
            {
                data: A_data,
                backgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56"
                ],
                hoverBackgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56"
                ]
            }]
    };

    var mypiechart = new Chart(chartinvar,
        {
            type: 'horizontalBar',
            labels: ['Fact. A', 'Fact. B', 'Fact. C'],
            data: data,
            animation: {
                animateScale: true
            },
            options:
                {
                    responsive: true,
                    legend:
                        {
                            display:false
                        },
                    title:
                        {
                            display: true,
                            text: A_title
                        },
                    scales:
                        {
                            xAxes:[
                                {
                                    ticks:
                                        {
                                            beginAtZero:true
                                        }
                                }]
                        }
                }
        });

</script>