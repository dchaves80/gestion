<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Stock.View" %>
<style>
    @font-face {
	 font-family: MetroAtrox;
    src: url('http://dnndev.me/Portals/0/Fonts/MetrostyleExtended.ttf');
}
.AtroxLabel {
	font-family:Metrostyle Extended;
    background-color: #00B4FF;
    color:#444444;
    padding-top:10px;
    padding-bottom:10px;
    margin-top:5px;
    text-align:center;
    vertical-align:central;
}

    .AtroxLabel:hover {
    background-color: #006bff;
    color:#eaeaea;
    }

    .AtroxTitle {
	font-family:Metrostyle Extended;
    
    color:#444444;
    padding-top:10px;
    padding-bottom:10px;
    margin-top:5px;
    margin-bottom:10px;
    text-align:left;
    font-size:30px;
}

    .AtroxUserName {
        font-size:10px;
        
    }
    .FormContainer {
        height:0%;
        text-align:left;
        vertical-align:central;
        background-color:#444444;
    }
    textarea {
        color:black;
    }
</style>




<div class="AtroxTitle">Articulos y Stock<div id="userName" class="AtroxUserName" runat="server">OOOOOO</div> </div>
<div class="AtroxLabel">
    
    PROVEEDORES
    
</div>
<div class="AtroxLabel">MANEJO DE ARTICULOS</div>
<div class="AtroxLabel">MANEJO DE STOCK</div>
<div class="AtroxLabel ImportCSV">IMPORTACION DE CSV
    <div class="FormContainer">
        Código del archivo: 
        <textarea id="CodigoArchivo" runat="server" style="width: 100%; height: 250px;"></textarea>
    </div>

</div>

<script>

    var div = $(".ImportCSVControl");
    div.toggle(1);
    $(".ImportCSV").click(

        function () {
          
            var div = $(".ImportCSVControl");
           
            div.toggle(1000);
        }
        );

    $(".FormContainer").click(function (event) {
        event.stopPropagation();
    });

</script>