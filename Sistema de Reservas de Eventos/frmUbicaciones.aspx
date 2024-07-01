<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmUbicaciones.aspx.cs" Inherits="Sistema_de_Reservas_de_Eventos.frmUbicaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        
        <!-- Grupo de tabla y edición -->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ubicacion_id" CssClass="table table-striped"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="ubicacion_id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="nombre_ubicacion" HeaderText="Nombre de Ubicación" />
                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                <asp:BoundField DataField="estado" HeaderText="Estado" />
                <asp:BoundField DataField="codigo_postal" HeaderText="Código Postal" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <!-- Grupo de guardado -->
        <asp:ValidationSummary ID="ValidationSummaryGuardar" runat="server" CssClass="alert alert-danger" HeaderText="Errores de Guardado:" ValidationGroup="Guardar" />

        <div class="form-group mt-4">
            <label for="txtNombreUbicacion">Nombre de Ubicación:</label>
            <asp:TextBox ID="txtNombreUbicacion" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNombreUbicacion" runat="server" ControlToValidate="txtNombreUbicacion" ErrorMessage="El nombre de la ubicación es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtDireccion">Dirección:</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txtDireccion" ErrorMessage="La dirección es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtCiudad">Ciudad:</label>
            <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" ControlToValidate="txtCiudad" ErrorMessage="La ciudad es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtEstado">Estado:</label>
            <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="txtEstado" ErrorMessage="El estado es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtCodigoPostal">Código Postal:</label>
            <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvCodigoPostal" runat="server" ControlToValidate="txtCodigoPostal" ErrorMessage="El código postal es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <asp:HiddenField ID="HiddenFieldUbicacionID" runat="server" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" ValidationGroup="Guardar" />

        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3"></asp:Label>
    </div>
</asp:Content>
