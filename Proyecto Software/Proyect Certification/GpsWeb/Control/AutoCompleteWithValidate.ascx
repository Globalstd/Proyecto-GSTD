<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoCompleteWithValidate.ascx.cs" Inherits="GpsWeb.Control.AutoCompleteWithValidate" %>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
<script src="//code.jquery.com/jquery-1.10.2.js"></script>
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
 <script>
     $(function () {
         var availableTags = <%=cadena%> ;
         $("#txtComplete").autocomplete({
             source: availableTags
         });
     });
</script>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Idioma, lblRequerido %>" ForeColor="red" ControlToValidate="txtComplete" ></asp:RequiredFieldValidator>
<asp:TextBox ID="txtComplete" runat="server" CssClass="form-control input-sm"></asp:TextBox>
