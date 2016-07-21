<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.MenuTree.View" %>
<script type="text/javascript" src="http://190.105.214.230/Portals/0/Scripts/jquery.sapling.min.js"></script>
<script>
$(document).ready(function() {

			$('#demoList').sapling();

});
</script>
<ul id="demoList">
		<li>Lorem ipsum dolor sit amet
            <ul>
                <li>Aguanten</li>
                <li>Los</li>
                <li>Gatos</li>
                <li>Carajo
                    <ul>
                        <li>Mas carajo...</li>
                    </ul>

                </li>

            </ul>
		</li>
		<li><a href="">Morbi id nisl sed elit</a>
		</li>
		<li>Vestibulum vulputate</li>
		<li>Vivamus sed enim ut mauris
		</li>
	</ul>