<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewCostum.ascx.cs" Inherits="GpsWeb.Control.GridViewCostum" %>
<script type="text/javascript" src="~/App_Themes/Certification/js/jquery.min.js"></script>
<script type="text/javascript" src="http://cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="http://cdn.datatables.net/plug-ins/f2c75b7247b/api/fnReloadAjax.js"> </script>
<link rel="stylesheet" href="http://cdn.datatables.net/1.10.5/css/jquery.dataTables.css"/>
<script type="text/javascript">
    //Inicialización de scripts
    $(document).ready(function () {
            var $table = $('#<%=id %>').DataTable({
                "bProcessing": true,
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": false,
                "sAjaxDataProp": "",
                "bDestroy": true
            });

            $table.draw();
    });
</script>
<asp:GridView ID="gdvCliente" runat="server" CssClass="display"
    EmptyDataText="No se han registrado clientes" ShowHeaderWhenEmpty="True" Width="100%"
        AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="ClaveCliente" HeaderText="Clave Cliente" />
        <asp:BoundField DataField="NombreCliente" HeaderText="Nombre Cliente" />
        <asp:TemplateField HeaderText="Forma de Aplicación" >
            <ItemTemplate>
                <asp:ImageButton ID="imgAplication" runat="server" ImageUrl="App_Themes/Certification/img/icon/editar.png" />
                <asp:Label ID="lblAplication" runat="server" Text="<strong>(2)</strong>"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Propuestas" >
            <ItemTemplate>
                <asp:ImageButton ID="imgProposal" runat="server" ImageUrl="App_Themes/Certification/img/icon/proposal.png" />
                <asp:Label ID="lblProposal" runat="server" Text="<strong>(5)</strong>"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Contratos">
            <ItemTemplate>
                <asp:ImageButton ID="imgContract" runat="server" ImageUrl="App_Themes/Certification/img/icon/icoContratos16-1.gif" />
                <asp:Label ID="lblContract" runat="server" Text="<strong>(1)</strong>"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Editar">
            <ItemTemplate>
                <asp:ImageButton ID="imgProperties" runat="server" ImageUrl="App_Themes/Certification/img/icon/icoPropiedades16-1.gif" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>