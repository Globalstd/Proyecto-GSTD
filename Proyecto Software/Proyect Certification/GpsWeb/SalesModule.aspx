<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SalesModule.aspx.cs" Inherits="GpsWeb.SalesModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorLateral" runat="server">
    <ul class="list-group" >
        <li><a class="list-group-item" style="border: none; color: #111;" href="SalesAddProspect.aspx"><span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>&nbsp;&nbsp;<asp:Literal ID="Lite" runat="server" Text="<%$ Resources:Idioma, lblAddProspect %>"></asp:Literal></a></li>
        <li><a class="list-group-item" style="border: none; color: #111;" href="#"><span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>&nbsp;&nbsp;Formas de Aplicación</a></li>
        <li><a class="list-group-item" style="border: none; color: #111;" href="#"><span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>&nbsp;&nbsp;Propuestas</a></li>
        <li><a class="list-group-item" style="border: none; color: #111;" href="#"><span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>&nbsp;&nbsp;Contratos</a></li>
        <li><a class="list-group-item" style="border: none; color: #000;" href="#"><span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>&nbsp;&nbsp;Reportes</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <div class="block-area" runat="server" ID="pnlProspect">
        <div class="row">
            <div class="col-md-1" >
            </div>
            <div class="col-lg-pull-10" >
            <div class="tile">
                <h2 class="tile-title">Clientes</h2>
                <div class="tile-config dropdown">
                    <a data-toggle="dropdown" href="#" class="tile-menu"></a>
                    <ul class="dropdown-menu pull-right text-right">
                        <li>
                            <asp:Button ID="btnAddProspect" runat="server" 
                                Text="<%$ Resources:Idioma, lblAddProspect %>" CssClass="btn_menu" 
                                onclick="btnAddProspect_Click"/>
                        </li>
                        <li>
                            <asp:Button ID="btnProspectAct" runat="server" Text="<%$ Resources:Idioma, lblClienteAct %>" CssClass="btn_menu"/>
                        </li>
                        <li>
                            <asp:Button ID="btnProspectIna" runat="server" Text="<%$ Resources:Idioma, lblClienteIna %>" CssClass="btn_menu" />
                        </li>
                    </ul>
                </div>
            
            <div  class="p-10" role="form">
                <asp:GridView ID="gdvCliente" runat="server" 
                    EmptyDataText="No se han registrado clientes" ShowHeaderWhenEmpty="True" Width="100%"
                        AutoGenerateColumns="False" AllowPaging="True" 
                    onpageindexchanging="gdvCliente_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ClaveCliente" HeaderText="Clave Cliente" />
                        <asp:BoundField DataField="NombreCliente" HeaderText="Nombre Cliente" />
                        <asp:BoundField DataField="UserName" HeaderText="Usuario" />
                        <asp:TemplateField HeaderText="Propuestas">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="App_Themes/Certification/img/icon/contract.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contratos">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="App_Themes/Certification/img/icon/contract.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="App_Themes/Certification/img/icon/edit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            </div>
    </div>
            <div class="col-md-1" >
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</asp:Content>
