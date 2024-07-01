<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSistemaUsuario.aspx.cs" Inherits="Sistema_de_Reservas_de_Eventos.frmSistemaUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Perfil del Usuario</h2>
        <div>
            <label for="lblNombreUsuario">Nombre de Usuario:</label>
            <asp:Label ID="lblNombreUsuario" runat="server" CssClass="form-control" />
        </div>
        <div>
            <label for="lblNombre">Nombre:</label>
            <asp:Label ID="lblNombre" runat="server" CssClass="form-control" />
        </div>
        <div>
            <label for="lblApellido">Apellido:</label>
            <asp:Label ID="lblApellido" runat="server" CssClass="form-control" />
        </div>
        <div>
            <label for="lblCorreoElectronico">Correo Electrónico:</label>
            <asp:Label ID="lblCorreoElectronico" runat="server" CssClass="form-control" />
        </div>

        <h2>Registrar en Evento</h2>
        <div>
            <label for="ddlEventos">Seleccione un Evento:</label>
            <asp:DropDownList ID="ddlEventos" runat="server" CssClass="form-control" />
        </div>
        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-success mt-2" OnClick="btnRegistrar_Click" />
        <asp:Label ID="lblMensajeRegistro" runat="server" CssClass="d-block mt-2"></asp:Label>

        <h2>Eventos Registrados</h2>
        <asp:GridView ID="GridViewEventosRegistrados" runat="server" AutoGenerateColumns="True" CssClass="table table-striped mt-3" />

        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-danger mt-3" OnClick="btnCerrarSesion_Click" />
    </div>
</asp:Content>
