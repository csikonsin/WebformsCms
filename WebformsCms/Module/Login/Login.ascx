<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="WebformsCms.Module.Login.Login" %>
<fieldset>
    <div class="form-group">
        <asp:Label AssociatedControlID="Username" Text="Username" runat="server" />
        <div class="col">
            <asp:textbox ID="Username" runat="server" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Username" CssClass="error" ErrorMessage="The username field is required" />
        </div>        
    </div>
    <div class="form-group">
        <asp:Label AssociatedControlID="Password" runat="server" Text="Password" />
        <div class="col">
            <asp:textbox TextMode="Password" runat="server" ID="Password" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="error" ErrorMessage="The password field is required" />
        </div>
    </div>
    <div class="form-group">
        <div class="col">
            <div class="checkbox">
                <asp:CheckBox runat="server" ID="RememberMe" />
                <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group">
        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
            <p class="error">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
        </asp:PlaceHolder>
    </div>
    <div class="form-group">
        <div class="col">
            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="button" />
        </div>
    </div>
</fieldset>