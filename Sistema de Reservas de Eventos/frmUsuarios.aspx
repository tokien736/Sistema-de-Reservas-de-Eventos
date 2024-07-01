<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmUsuarios.aspx.cs" Inherits="Sistema_de_Reservas_de_Eventos.frmUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <!-- Grupo de búsqueda -->
        <asp:ValidationSummary ID="ValidationSummaryBuscar" runat="server" CssClass="alert alert-danger" HeaderText="Errores de Búsqueda:" ValidationGroup="Buscar" />

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtBuscarNombre">Buscar por Nombre de Usuario:</label>
                    <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtBuscarCorreo">Buscar por Correo Electrónico:</label>
                    <asp:TextBox ID="txtBuscarCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary mb-3" OnClick="btnBuscar_Click" ValidationGroup="Buscar" />
        <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" CssClass="btn btn-secondary mb-3" OnClick="btnRestablecer_Click" />

        <!-- Grupo de tabla y edición -->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="usuario_id" CssClass="table table-striped"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="usuario_id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="nombre_usuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="correo_electronico" HeaderText="Correo Electrónico" />
                <asp:TemplateField HeaderText="Rol">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                            <asp:ListItem Text="admin" Value="admin"></asp:ListItem>
                            <asp:ListItem Text="user" Value="user"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRol" runat="server" Text='<%# Eval("rol") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contraseña">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" Text='<%# Eval("contrasena") %>' TextMode="Password"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblContrasena" runat="server" Text="********"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <!-- Grupo de guardado -->
        <asp:ValidationSummary ID="ValidationSummaryGuardar" runat="server" CssClass="alert alert-danger" HeaderText="Errores de Guardado:" ValidationGroup="Guardar" />

        <div class="form-group mt-4">
            <label for="TextBox1">Nombre de Usuario:</label>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNombreUsuario" runat="server" ControlToValidate="TextBox1" ErrorMessage="El nombre de usuario es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="TextBox2">Contraseña:</label>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Password" />
            <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" ControlToValidate="TextBox2" ErrorMessage="La contraseña es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="TextBox3">Correo Electrónico:</label>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvCorreoElectronico" runat="server" ControlToValidate="TextBox3" ErrorMessage="El correo electrónico es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
            <asp:RegularExpressionValidator ID="revCorreoElectronico" runat="server" ControlToValidate="TextBox3" ErrorMessage="Formato de correo electrónico no válido." CssClass="text-danger" Display="Dynamic" ValidationExpression="\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="DropDownList1">Rol:</label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                <asp:ListItem Text="admin" Value="admin"></asp:ListItem>
                <asp:ListItem Text="user" Value="user"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvRol" runat="server" ControlToValidate="DropDownList1" InitialValue="" ErrorMessage="El rol es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="Button1_Click" ValidationGroup="Guardar" />

        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3"></asp:Label>
    </div>
</asp:Content>
