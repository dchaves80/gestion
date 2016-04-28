<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.Menu.View" %>
<style>

  
.AtroxLabel {
	font-family:"Josefin Sans";
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
	font-family:'Poiret One';
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




<div class="AtroxTitle">Menu Principal<div id="userName" class="AtroxUserName" runat="server">OOOOOO</div> </div>
<div class="AtroxLabel">ADMINISTRACION PROVEEDORES</div>
<div class="AtroxLabel">MANEJO DE ARTICULOS</div>
<div class="AtroxLabel">MANEJO DE STOCK</div>
<div class="AtroxLabel ImportCSV">IMPORTACION DE CSV</div>



