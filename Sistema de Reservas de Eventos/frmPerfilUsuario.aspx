<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPerfilUsuario.aspx.cs" Inherits="Sistema_de_Reservas_de_Eventos.frmPerfilUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        
        <!-- Grupo de tabla y edición -->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="perfil_id" CssClass="table table-striped"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="perfil_id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="usuario_id" HeaderText="ID Usuario" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <!-- Grupo de guardado -->
        <asp:ValidationSummary ID="ValidationSummaryGuardar" runat="server" CssClass="alert alert-danger" HeaderText="Errores de Guardado:" ValidationGroup="Guardar" />

        <div class="form-group mt-4">
            <label for="txtNombre">Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtApellido">Apellido:</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtTelefono">Teléfono:</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono" ErrorMessage="El teléfono es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtDireccion">Dirección:</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txtDireccion" ErrorMessage="La dirección es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <asp:HiddenField ID="HiddenFieldPerfilID" runat="server" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" ValidationGroup="Guardar" />

        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3"></asp:Label>
    </div>
</asp:Content>
