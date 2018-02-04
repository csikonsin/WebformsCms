<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Module.ascx.cs" Inherits="WebformsCms.Module.Module" %>
<%@ Register Src="~/Module/SingleModule.ascx" TagName="singlemodule" TagPrefix="cms" %>
<asp:PlaceHolder runat="server" id="Editor"/>
<asp:repeater ID="repModules" runat="server">
    <ItemTemplate>
        <cms:singlemodule runat="server" id="singlemodule" />
    </ItemTemplate>
</asp:repeater>
<asp:PlaceHolder runat="server" id="Add"/>
