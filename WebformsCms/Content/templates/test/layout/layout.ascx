<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="layout.ascx.cs" Inherits="WebformsCms.Content.templates.layout.layout" %>
<%@ Register Src="~/Module/Module.ascx" TagName="module" TagPrefix="cms" %>
<%@ Register Src="~/Module/Menu/Menu.ascx" TagName="menu" TagPrefix="cms" %>
<div class="page">
    <header>
        <a class="lg">LOGO</a>
        <nav><cms:menu runat="server" Id="main"/></nav>
    </header>
    <main><cms:module runat="server" /></main>
</div>