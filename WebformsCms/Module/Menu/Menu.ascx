<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="WebformsCms.Module.Menu.Menu" %>
<asp:PlaceHolder ID="ph" runat="server" />
<asp:ListView runat="server" ID="Menus" Visible="false">
    <LayoutTemplate>
        <ul>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
        </ul>
    </LayoutTemplate>
    <ItemTemplate>
        <li><a href="<%#Eval("Url") %>"><%#Eval("Name") %></a><asp:PlaceHolder runat="server" ID="anchorPh" /></li>
    </ItemTemplate>
</asp:ListView>