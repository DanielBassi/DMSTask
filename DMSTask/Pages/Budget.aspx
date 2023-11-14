<%@ Page Title="Budgets" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Budget.aspx.vb" Inherits="DMSTask.Budget" %>
<% @OutputCache Location="None" NoStore="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-3">
            <div class="card">
                <h3 class="card-title">Crear Budget</h3>
                <div class="card-body">
                    <div class="form-group">
                        <label for="txtName">Nombre</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtAmount">Presupuesto</label>
                        <asp:TextBox ID="txtAmount" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtCategory">Categoría</label>
                        <asp:DropDownList ID="txtCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" OnClick="SaveBudget" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
        <div class="col-lg-9">
            <div class="card">
                <h3 class="card-title">Listado de Asignaciones Presupeustales</h3>
                <div class="card-body">
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Buscar..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Buscar" OnClick="btnSearch_Click" />
                    <br />
                    <br />
                    <asp:GridView
                        class="mt-4"
                        ID="gridView" 
                        runat="server" 
                        AutoGenerateColumns="false" 
                        Width="900px" 
                        DataKeyNames="Id"
                        OnRowDeleting="rowDeleteEvent"
                        OnRowUpdating="rowUpdteEvent" 
                        OnRowEditing="rowEditEvent"
                        AllowPaging="True"
                        OnRowCancelingEdit="rowCancelEvent"
                        OnPageIndexChanging="GridView_PageIndexChanging"
                        PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="Nombre" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<% # Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox Id="txtTitle" runat="server" Text='<% # Bind("Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Presupuesto" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<% # Bind("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox Id="txtDescription" runat="server" Text='<% # Bind("Amount") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Categoría" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="txtCategory" runat="server" Text='<% # Bind("CategoryDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="dropDownCategory" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            <asp:CommandField  ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"/>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <selectedrowstyle backcolor="LightBlue" forecolor="DarkBlue"/> 
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:Label ID="lblEmptyData" runat="server" Text="No hay datos para mostrar" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>     
</asp:Content>
