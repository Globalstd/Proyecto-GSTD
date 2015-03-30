<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SalesAddProspect.aspx.cs" Inherits="GpsWeb.SalesAddProspect" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<%@ Register src="Control/Autocomplete.ascx" tagName="AutoComplete" tagPrefix="uc"%>
<%@ Register src="Control/AutoCompleteWithValidate.ascx" tagName="AutoCompleteWithValidate" tagPrefix="uc"%>
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
                    <h4>
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Idioma, lblEjecutivoVentas%>"></asp:Label>
                        <b>Karla Ruiz</b>
                    </h4>
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
                <asp:Panel ID="pnlClientName" runat="server" CssClass="form-group m-10">
                    <asp:Label ID="lblClientName" runat="server" Text="<%$ Resources:Idioma, lblClientName %>"></asp:Label> 
                </asp:Panel>
            </div>
            <div class="col-md-2" >
                <div class="form-group m-10">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Idioma, lblStatus %>"></asp:Label>
                    <asp:RequiredFieldValidator InitialValue="00000000-0000-0000-0000-000000000000" ID="RequiredFieldValidator6"  ForeColor="red"
                    Display="Dynamic" runat="server" ControlToValidate="ddlStatusCliente"
                    Text="<%$ Resources:Idioma, lblRequerido %>" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlStatusCliente" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
                <div class="col-md-8" >
                    <div class="form-group m-10" runat="server" ID="divClientRazon">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Idioma, lblRazon %>"></asp:Label>
                    </div>
                </div>  
                <div class="col-md-4" >                     
                <div class="form-group m-10" runat="server" ID="divClientRfc">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Idioma, lblRFC %>"></asp:Label>
                </div>
            </div> 
        </div>
        <div class="row">    
            <div class="col-md-4" >                     
                <div class="form-group m-10">
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Idioma, lblWeb %>"></asp:Label>
                    <asp:TextBox ID="txtSitioWeb" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4" >                     
                <div class="form-group m-10">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Idioma, lblFuente %>"></asp:Label>
                    <asp:RequiredFieldValidator InitialValue="00000000-0000-0000-0000-000000000000" ID="RequiredFieldValidator1"  ForeColor="red"
                    Display="Dynamic" runat="server" ControlToValidate="ddlFuente"
                    Text="<%$ Resources:Idioma, lblRequerido %>" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlFuente" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">                     
                <div class="form-group m-10">
                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Idioma, lblRecomendado %>"></asp:Label>
                    <asp:RequiredFieldValidator ID="ReqVtxtRecomendado" runat="server" ErrorMessage="<%$ Resources:Idioma, lblRequerido %>" ForeColor="red" ControlToValidate="txtRecomendado" ></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtRecomendado" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>  
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanelInteresado" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">  
            <div class="col-md-4">                     
                <div class="form-group m-10">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Idioma, lblinteres %>"></asp:Label>
                    <asp:RequiredFieldValidator InitialValue="00000000-0000-0000-0000-000000000000" ID="Req_ID"  ForeColor="red"
                    Display="Dynamic" runat="server" ControlToValidate="ddlInteres"
                    Text="<%$ Resources:Idioma, lblRequerido %>" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlInteres" runat="server" 
                        CssClass="form-control input-sm" AutoPostBack="true"
                        onselectedindexchanged="ddlInteres_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4" id="Div16">                     
                <div class="form-group m-10">
                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Idioma, lblStandard %>"></asp:Label>
                    <asp:DropDownList ID="ddlStandard" runat="server" 
                        CssClass="form-control input-sm" AutoPostBack="true"
                        onselectedindexchanged="ddlStandard_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>  
            <div class="col-md-4" >                     
                <div class="form-group m-10">
                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Idioma, lblCourse %>"></asp:Label>
                    <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">  
            <div class="col-md-4">    
            </div>
        </div>
        <div class="row">
            <div class="col-md-10">                     
                <div class="form-group m-10">
                    <h3><asp:Label ID="Label11" runat="server" Text="<%$ Resources:Idioma, lblDirecFis %>"></asp:Label></h3>
                </div>
            </div>
        </div>
        <div class="row">    
            <div class="col-md-6" id="Div1">
                    <div class="form-group m-10">
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Idioma, lblCalle %>"></asp:Label>
                        <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <div class="col-md-6" id="Div3"> 
                <div class="form-group m-10">
                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Idioma, lblColonia %>"></asp:Label>
                    <asp:TextBox ID="txtColonia" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">    
            <div class="col-md-2" id="Div4">                     
                    <div class="form-group m-10">
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Idioma, lblNumExterior %>"></asp:Label>
                        <asp:TextBox ID="txtNumExt" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <div class="col-md-2" id="Div5">                     
                    <div class="form-group m-10">
                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Idioma, lblNumInterior %>"></asp:Label>
                        <asp:TextBox ID="txtNumInt" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <div class="col-md-2" id="Div7">                     
                    <div class="form-group m-10">
                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Idioma, lblCodigo %>"></asp:Label>
                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <div class="col-md-6" id="Div6">                     
                    <div class="form-group m-10">
                        <asp:Label ID="Label17" runat="server" Text="<%$ Resources:Idioma, lblDelegacion %>"></asp:Label>
                        <asp:TextBox ID="txtDelegacion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
        </div>
        <div class="row">    
            <div class="col-md-4" id="Div8">                     
                    <div class="form-group m-10">
                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:Idioma, lblCiudad %>"></asp:Label>
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
            </div>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updatePanelStates">
            <ContentTemplate>
            <div class="col-md-4" id="Div9">                     
                    <div class="form-group m-10">
                        <asp:Label ID="Label19" runat="server" Text="<%$ Resources:Idioma, lblEstado %>"></asp:Label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
            </div>
            <div class="col-md-4" id="Div10">                     
                    <div class="form-group m-10">
                        <asp:Label ID="Label20" runat="server" Text="<%$ Resources:Idioma, lblPais %>"></asp:Label>
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
                    <h3><asp:Label ID="Label21" runat="server" Text="<%$ Resources:Idioma, lblContPrincipal %>"></asp:Label></h3>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="form-group m-10">
                    <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Idioma, lblArea %>"></asp:Label>
                    <asp:DropDownList ID="ddlTypeContact" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">                     
                <div class="form-group m-10">
                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Idioma, lblNombre %>"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:Idioma, lblRequerido %>" ForeColor="red" ControlToValidate="txtContactName" ></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group m-10">
                    <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Idioma, lblApPaterno %>"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="<%$ Resources:Idioma, lblRequerido %>" ForeColor="red" ControlToValidate="txtContactApPaterno" ></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtContactApPaterno" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group m-10">
                    <asp:Label ID="Label25" runat="server" Text="<%$ Resources:Idioma, lblApMaterno %>"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<%$ Resources:Idioma, lblRequerido %>" ForeColor="red" ControlToValidate="txtContactApMaterno" ></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtContactApMaterno" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">                     
                <div class="form-group m-10">
                    <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Idioma, lblTelefono %>"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:Idioma, lblRequerido %>" ForeColor="red" ControlToValidate="txtContactTelefono" ></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtContactTelefono" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group m-10" runat="server" ID="divContactMail">
                    <asp:Label ID="Label27" runat="server" Text="<%$ Resources:Idioma, lblCorreo %>"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group m-10">
                    <asp:Label ID="Label28" runat="server" Text="<%$ Resources:Idioma, lblCargo %>"></asp:Label>
                    <asp:TextBox ID="txtPosicion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" id="Div2"> 
                    
                    <div class="form-group m-10">
                        <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Idioma, lblDescripcion %>"></asp:Label>
                        <asp:TextBox class="wysiwye-editor" runat="server" ID="txtAlcance"></asp:TextBox>
                    </div>
                    <div class="form-group m-10">
                        <asp:Button ID="btnAceptar" runat="server" 
                            Text="<%$ Resources:Idioma, lblProspectAdd %>" CssClass="btn btn-sm" 
                            onclick="btnAceptar_Click"/>
                        <asp:Button ID="btnAceptarNuevo" runat="server" Text="<%$ Resources:Idioma, lblProspectAddAndNew %>" CssClass="btn btn-sm" />
                        <asp:Button ID="btnCancelar" runat="server" Text="<%$ Resources:Idioma, lblProspectCancel %>" CssClass="btn btn-sm"/>
                        <asp:Button ID="btnEnviarMail" runat="server" Text="Enviar Mail" 
                            onclick="btnEnviarMail_Click" />
                    </div>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
