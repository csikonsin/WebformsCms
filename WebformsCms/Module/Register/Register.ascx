<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="WebformsCms.Module.Register.Register" %>
<fieldset>
    <div class="form-group">
    </div>
    <div class="form-group">
        <p class="error">
            <asp:Literal runat="server" ID="ErrorMessage" />
        </p>
    </div>  
    <div class="form-group">
        <asp:label AssociatedControlID="Username" runat="server" Text="Username" />
        <div class="col">
            <asp:TextBox ID="Username" runat="server" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
            CssClass="error" ErrorMessage="The username field is required." />
        </div>
    </div>
    <div class="form-group">
        <asp:label AssociatedControlID="Password" runat="server" Text="Password" />
        <div class="col">
            <asp:TextBox ID="Password" runat="server" />
             <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
            CssClass="error" ErrorMessage="The pssword field is required." />
        </div>
    </div>
    <div class="form-group">
        <asp:label AssociatedControlID="ConfirmPassword" runat="server" Text="Password" />
        <div class="col">
            <asp:TextBox ID="ConfirmPassword" runat="server" />
             <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
            CssClass="error" ErrorMessage="The confirmation field is required." />
            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
            CssClass="error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
        </div>
    </div>
    <div class="form-group">
        <asp:label AssociatedControlID="Email" runat="server" Text="Email" />
        <div class="col">
            <asp:TextBox ID="Email" runat="server" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="error" ErrorMessage="The email field is required" />
        </div>
    </div>
    <div class="form-group">
        <asp:label AssociatedControlID="EmailConfirmation" runat="server" Text="Email confirmation" />
        <div class="col">
            <asp:TextBox ID="EmailConfirmation" runat="server" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailConfirmation" CssClass="error" ErrorMessage="The email confirmation field is required" />
            <asp:CompareValidator runat="server" ControlToCompare="Email" ControlToValidate="EmailConfirmation"
            CssClass="error" Display="Dynamic" ErrorMessage="The email and confirmation email do not match." />
        </div>
    </div>
    <div class="form-group">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
    </div>
    <div class="form-group">
        <div class="col">
            <asp:Button runat="server" OnClick="Register_Click" Text="Register" CssClass="button" />
        </div>
    </div>
</fieldset>