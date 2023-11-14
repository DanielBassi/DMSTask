<%@ Page Title="Authentication" Language="vb" AutoEventWireup="false" CodeBehind="Authentication.aspx.vb" MasterPageFile="~/AuthMaster.Master" Inherits="DMSTask.Authentication" %>
<asp:Content ID="ContentAuthentication" ContentPlaceHolderID="Content" runat="server">
    <div class="wrapper fadeInDown">
      <div id="formContent">
        <asp:TextBox ID="_txtEmail" CssClass="fadeIn second" ValidateRequestMode="Enabled" runat="server"></asp:TextBox>
        <asp:TextBox ID="_txtPassword" CssClass="fadeIn third" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="btn" runat="server" Text="Entrar" OnClick="Auth" CssClass="fadeIn fourth" />
        <div id="formFooter">
          <a class="underlineHover" runat="server" href="/Pages/Register">Registrarse</a>
        </div>
      </div>
    </div>
</asp:Content>
