<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Import.aspx.vb" Inherits="DMSTask.Import" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .img {
            width: 100px;
            height: 100px;
        }
    </style>
    <div class="row">
        <div class="col-lg-12">
            <h2>Importar datos desde Excel</h2>
        </div>
        <div class="col-lg-4">
            <div class="card" style="width: 18rem;">
              <asp:Image CssClass="img" ID="imgTasks" runat="server" ImageUrl="~/Images/tasks.png" />
              <div class="card-body">
                <h5 class="card-title">Tareas</h5>
                  <asp:FileUpload ID="fileUploadTasks" runat="server" />
              </div>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="card" style="width: 18rem;">
              <asp:Image CssClass="img" ID="imgBudgets" runat="server" ImageUrl="~/Images/budgets.png" />
              <div class="card-body">
                <h5 class="card-title">Asignación Presupuestal</h5>
                  <asp:FileUpload ID="fileUploadBudgets" runat="server" />
              </div>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="card" style="width: 18rem;">
              <asp:Image CssClass="img" ID="imgUsers" runat="server" ImageUrl="~/Images/users.png" />
              <div class="card-body">
                <h5 class="card-title">Usuarios</h5>
                  <asp:FileUpload ID="fileUploadUsers" runat="server" />
              </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-lg-12">
            <asp:Button CssClass="btn btn-primary" ID="btnUpload" runat="server" Text="Subir Archivo" OnClick="UploadFile" />
        </div>
    </div>
</asp:Content>

