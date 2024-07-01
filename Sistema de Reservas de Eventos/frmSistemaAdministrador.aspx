<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSistemaAdministrador.aspx.cs" Inherits="Sistema_de_Reservas_de_Eventos.frmSistemaAdministrador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Panel de Administración</h2>

        <div class="mt-3">
            <a href="frmUsuarios.aspx" target="_blank" class="btn btn-primary">Gestionar Usuarios</a>
        </div>
        <div class="mt-3">
            <a href="frmPerfilUsuario.aspx" target="_blank" class="btn btn-primary">Gestionar Perfiles de Usuarios</a>
        </div>
        <div class="mt-3">
            <a href="frmUbicaciones.aspx" target="_blank" class="btn btn-primary">Gestionar Ubicaciones</a>
        </div>
        <div class="mt-3">
            <a href="frmEventos.aspx" target="_blank" class="btn btn-primary">Gestionar Eventos</a>
        </div>
        <div class="mt-3">
            <a href="frmReservas.aspx" target="_blank" class="btn btn-primary">Gestionar Reservas</a>
        
        </div>
            <div class="mt-3">
            <a href="frmEstadisticas.aspx" target="_blank" class="btn btn-primary">Estadisticas</a>
        </div>

        </div>
            <div class="mt-3">
            <a href="frmReporte.aspx" target="_blank" class="btn btn-primary">Reportes</a>
        </div>

        <div class="mt-5">
            <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-danger" OnClick="btnCerrarSesion_Click" />
        </div>
    </div>
</asp:Content>
