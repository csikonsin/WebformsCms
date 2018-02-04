<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Module.ascx.cs" Inherits="WebformsCms.Module.Module" %>
<asp:PlaceHolder runat="server" id="Editor"/>
<asp:repeater ID="repModules" runat="server">
    <ItemTemplate>
        <div class="module"><div class="module-inner"><asp:PlaceHolder ID="ph" runat="server" /></div><asp:PlaceHolder ID="commands" runat="server" /></div>
    </ItemTemplate>
</asp:repeater>
<asp:PlaceHolder runat="server" id="Add"/>
