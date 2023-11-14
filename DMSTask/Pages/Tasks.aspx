<%@ Page Title="Tasks" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.vb" Inherits="DMSTask.Tasks" EnableEventValidation="true" %>
<% @OutputCache Location="None" NoStore="true" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-3">
            <div class="card">
                <h3 class="card-title">Crear tarea</h3>
                <div class="card-body">
                    <div class="form-group">
                        <label for="txtTitle">Titulo</label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="TextDescription">Descripción</label>
                        <asp:TextBox ID="TextDescription" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="TextFechaVencimiento">Fecha vencimiento</label>
                        <asp:TextBox ID="TextFechaVencimiento" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" OnClick="SaveTask" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
        <div class="col-lg-9">
            <div class="card">
                <h3 class="card-title">Listado de tareas</h3>
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
                        OnRowCancelingEdit="rowCancelEvent"
                        AllowPaging="True"
                        OnPageIndexChanging="GridView_PageIndexChanging"
                        PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="Titulo" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<% # Bind("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox Id="txtTitle" runat="server" Text='<% # Bind("Title") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<% # Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox Id="txtDescription" runat="server" Text='<% # Bind("Description") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha de vencimiento" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaVencimiento" runat="server" Text='<% # Bind("ExpiredDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox Id="txtFechaVencimiento" TextMode="Date" runat="server" Text='<% # Bind("ExpiredDate") %>'></asp:TextBox>
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