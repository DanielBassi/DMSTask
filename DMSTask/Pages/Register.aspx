<%@ Page Title="Register" Language="vb" AutoEventWireup="false" CodeBehind="Register.aspx.vb" MasterPageFile="~/AuthMaster.Master" Inherits="DMSTask.Register" %>
<asp:Content ID="ContentRegister" ContentPlaceHolderID="Content" runat="server">
    <div class="wrapper fadeInDown">
      <div id="formContent">
        <asp:TextBox ID="txtFirstName" CssClass="fadeIn first" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtLastName" CssClass="fadeIn second" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtEmail" CssClass="fadeIn third" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtPassword" CssClass="fadeIn fourth" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="btn" runat="server" Text="Registrar" OnClick="Register" CssClass="fadeIn fourth" />
        <div id="formFooter">
          <a class="underlineHover" runat="server" href="/Pages/Authentication">Iniciar sesión</a>
        </div>
      </div>
    </div>
</asp:Content>
