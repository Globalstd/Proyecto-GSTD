<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SalesAddProspect.aspx.cs" Inherits="GpsWeb.SalesAddProspect" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<%@ Register src="Control/Autocomplete.ascx" tagName="AutoComplete" tagPrefix="uc"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorLateral" runat="server">
    <ul class="list-group" >
        <li><a class="list-group-item" style="border: none; color: #111;" href="SalesModule.aspx"><span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>&nbsp;&nbsp;Clientes</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <div class="block-area">
        <div class="col-md-1"></div>
        <div class="col-lg-pull-10" style="background: none repeat scroll 0 0 rgba(229, 229, 228, 0.8);">
        <h2 class="tile-title">
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Idioma, lblAddProspect %>"></asp:Literal>
        </h2>
        <div class="row">
            <div class="col-md-6">                     
                <div class="form-group m-10">
                    <h4>Ejecutivo de Cuenta: <b>Karla Ruiz</b></h4>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group m-10" style="text-align: right;">
                    <label><%=DateTime.Now.Date.ToString("D") %></label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-10" >
                <div class="form-group m-10" runat="server" ID="divClientName">
                    <label>Nombre Cliente</label>
                </div>
            </div>
            <div class="col-md-2" >
                <div class="form-group m-10">
                    <label>Estatus </label>
                    <asp:DropDownList ID="ddlStatusCliente" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
                <div class="col-md-8" >
                    <div class="form-group m-10" runat="server" ID="divClientRazon">
                        <label>Razón social</label>
                    </div>
                </div>  
                <div class="col-md-4" >                     
                <div class="form-group m-10" runat="server" ID="divClientRfc">
                    <label>RFC</label>
                </div>
            </div> 
        </div>
        <div class="row">    
            <div class="col-md-4" >                     
                <div class="form-group m-10">
                    <label>Sitio Web</label>
                    <asp:TextBox ID="txtSitioWeb" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4" >                     
                <div class="form-group m-10">
                    <label>Fuente </label>
                    <asp:DropDownList ID="ddlFuente" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">                     
                <div class="form-group m-10">
                    <label>Recomendado por </label>
                    <asp:TextBox ID="txtRecomendado" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>  
        </div>
        <div class="row">  
            <div class="col-md-4">                     
                <div class="form-group m-10">
                    <label>Interesado en </label>
                    <asp:DropDownList ID="ddlInteres" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4" id="Div16">                     
                <div class="form-group m-10">
                    <label>Standard </label>
                    <asp:DropDownList ID="ddlStandard" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>  
            <div class="col-md-4" >                     
                <div class="form-group m-10">
                    <label>Course</label>
                    <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">  
            <div class="col-md-4">    
            </div>
        </div>
        <div class="row">
            <div class="col-md-10">                     
                <div class="form-group m-10">
                    <h3>Dirección Fiscal</h3>
                </div>
            </div>
        </div>
        <div class="row">    
            <div class="col-md-6" id="Div1">
                    <div class="form-group m-10">
                        <label>Calle</label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <div class="col-md-6" id="Div3"> 
                <div class="form-group m-10">
                    <label>Colonia</label>
                    <asp:TextBox ID="txtColonia" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">    
            <div class="col-md-2" id="Div4">                     
                    <div class="form-group m-10">
                        <label>Numero Exterior</label>
                        <asp:TextBox ID="txtNumExt" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <div class="col-md-2" id="Div5">                     
                    <div class="form-group m-10">
                        <label>Numero Interior</label>
                        <asp:TextBox ID="txtNumInt" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <div class="col-md-2" id="Div7">                     
                    <div class="form-group m-10">
                        <label>Codigo Postal</label>
                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <div class="col-md-6" id="Div6">                     
                    <div class="form-group m-10">
                        <label>Delegación</label>
                        <asp:TextBox ID="txtDelegacion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
        </div>
        <div class="row">    
            <div class="col-md-4" id="Div8">                     
                    <div class="form-group m-10">
                        <label>Ciudad</label>
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updatePanelStates">
            <ContentTemplate>
            <div class="col-md-4" id="Div9">                     
                    <div class="form-group m-10">
                        <label>Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
            </div>
            <div class="col-md-4" id="Div10">                     
                    <div class="form-group m-10">
                        <label>Pais</label>
                        <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-control input-sm" 
                            onselectedindexchanged="ddlPais_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            
        </div>
        <div class="row">
            <div class="col-md-10">                     
                <div class="form-group m-10">
                    <h3>Contacto Principal</h3>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="form-group m-10">
                    <label>Tipo de Contacto</label>
                    <asp:DropDownList ID="ddlTypeContact" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">                     
                <div class="form-group m-10">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group m-10">
                    <label>Apellido Paterno</label>
                    <asp:TextBox ID="txtContactApPAterno" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group m-10">
                    <label>Apellido Materno</label>
                    <asp:TextBox ID="txtContactApMaterno" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">                     
                <div class="form-group m-10">
                    <label>Telefono</label>
                    <asp:TextBox ID="txtContactTelefono" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group m-10" runat="server" ID="divContactMail">
                    <label>Correo</label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group m-10">
                    <label>Posición</label>
                    <asp:TextBox ID="txtPosicion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" id="Div2"> 
                    
                    <div class="form-group m-10">
                        <label>Descripción (Alcance)</label>
                        <asp:TextBox class="wysiwye-editor" runat="server" ID="txtAlcance"></asp:TextBox>
                    </div>
                    <div class="form-group m-10">
                        <asp:Button ID="btnAceptar" runat="server" Text="<%$ Resources:Idioma, lblProspectAdd %>" CssClass="btn btn-sm"/>
                        <asp:Button ID="btnAceptarNuevo" runat="server" Text="<%$ Resources:Idioma, lblProspectAddAndNew %>" CssClass="btn btn-sm" />
                        <asp:Button ID="btnCancelar" runat="server" Text="<%$ Resources:Idioma, lblProspectCancel %>" CssClass="btn btn-sm"/>
                    </div>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
