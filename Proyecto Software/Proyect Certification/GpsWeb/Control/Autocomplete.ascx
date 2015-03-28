﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Autocomplete.ascx.cs" Inherits="GpsWeb.Control.Autocomplete" %>
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
<script src="//code.jquery.com/jquery-1.10.2.js"></script>
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
 <script>
     $(function () {
         var availableTags = <%=cadena%> ;
         $("#<%=id %>").autocomplete({
             source: availableTags
         });
     });
</script>
<asp:TextBox ID="txtAutoComplete" runat="server" CssClass="form-control input-sm" ></asp:TextBox>