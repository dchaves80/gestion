<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.ListadoFacturas.View" %>
<div>
    <div class="ResponsiveDiv">
        <span class="FormLabel">Fecha desde:</span><asp:Calendar runat="server" ID="CalendarDesde" DayNameFormat="FirstLetter" CssClass="myCalendar" SelectedDate="<%#DateTime.Today %>">
            <OtherMonthDayStyle ForeColor="#b0b0b0" />
            <DayStyle CssClass="myCalendarDay" ForeColor="#2d3338" />
            <DayHeaderStyle CssClass="myCalendarDayHeader" ForeColor="#2d3338" />
            <SelectedDayStyle Font-Bold="True" Font-Size="12px" CssClass="myCalendarSelector" />
            <TodayDayStyle CssClass="myCalendarToday" />
            <SelectorStyle CssClass="myCalendarSelector" />
            <NextPrevStyle CssClass="myCalendarNextPrev" />
            <TitleStyle CssClass="myCalendarTitle" />
        </asp:Calendar>
    </div>
    <div class="ResponsiveDiv">
        <span class="FormLabel">Fecha hasta:</span><asp:Calendar runat="server" ID="CalendarHasta" DayNameFormat="FirstLetter" CssClass="myCalendar" SelectedDate="<%#DateTime.Today %>" >
            <OtherMonthDayStyle ForeColor="#b0b0b0" />
            <DayStyle CssClass="myCalendarDay" ForeColor="#2d3338" />
            <DayHeaderStyle CssClass="myCalendarDayHeader" ForeColor="#2d3338" />
            <SelectedDayStyle Font-Bold="True" Font-Size="12px" CssClass="myCalendarSelector" />
            <TodayDayStyle CssClass="myCalendarToday" />
            <SelectorStyle CssClass="myCalendarSelector" />
            <NextPrevStyle CssClass="myCalendarNextPrev" />
            <TitleStyle CssClass="myCalendarTitle" />
        </asp:Calendar>
    </div>
    <div class="ResponsiveDiv">
        <span class="FormLabel">Tipo de Factura:</span>
        <asp:DropDownList runat="server" ID="cmb_TipoComprobante">
            <asp:ListItem Text="Todas" Value="0"/>
            <asp:ListItem Text="Factura A" Value="A" />
            <asp:ListItem Text="Factura B" Value="B" />
        </asp:DropDownList><br />
        <asp:Button runat="server" Text="Buscar Facturas" ID="btnBuscar" CssClass="FormButton FirstElement LastElement" onclick="btnBuscar_Click"/>
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
<script>
    function OpenC(key)
    {
        
        window.location.href = $('#urlbase').val() + "?VC=" + key;
    }
</script>