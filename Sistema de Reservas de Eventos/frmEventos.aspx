<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmEventos.aspx.cs" Inherits="Sistema_de_Reservas_de_Eventos.frmEventos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="evento_id" CssClass="table table-striped"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="evento_id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="titulo" HeaderText="Título" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="fecha_evento" HeaderText="Fecha del Evento" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="hora_inicio" HeaderText="Hora de Inicio" DataFormatString="{0:hh\\:mm\\:ss}" HtmlEncode="False" />
                <asp:BoundField DataField="hora_fin" HeaderText="Hora de Fin" DataFormatString="{0:hh\\:mm\\:ss}" HtmlEncode="False" />
                <asp:BoundField DataField="ubicacion_id" HeaderText="ID de Ubicación" />
                <asp:BoundField DataField="creado_por" HeaderText="Creado Por" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <asp:ValidationSummary ID="ValidationSummaryGuardar" runat="server" CssClass="alert alert-danger" HeaderText="Errores de Guardado:" ValidationGroup="Guardar" />

        <div class="form-group mt-4">
            <label for="txtTitulo">Título:</label>
            <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="El título es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtDescripcion">Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="La descripción es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtFechaEvento">Fecha del Evento:</label>
            <asp:TextBox ID="txtFechaEvento" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvFechaEvento" runat="server" ControlToValidate="txtFechaEvento" ErrorMessage="La fecha del evento es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtHoraInicio">Hora de Inicio:</label>
            <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvHoraInicio" runat="server" ControlToValidate="txtHoraInicio" ErrorMessage="La hora de inicio es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtHoraFin">Hora de Fin:</label>
            <asp:TextBox ID="txtHoraFin" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvHoraFin" runat="server" ControlToValidate="txtHoraFin" ErrorMessage="La hora de fin es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtUbicacionID">ID de Ubicación:</label>
            <asp:TextBox ID="txtUbicacionID" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvUbicacionID" runat="server" ControlToValidate="txtUbicacionID" ErrorMessage="El ID de ubicación es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtCreadoPor">Creado Por:</label>
            <asp:TextBox ID="txtCreadoPor" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvCreadoPor" runat="server" ControlToValidate="txtCreadoPor" ErrorMessage="El campo 'Creado por' es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <asp:HiddenField ID="HiddenFieldEventoID" runat="server" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" ValidationGroup="Guardar" />

        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3"></asp:Label>
    </div>
</asp:Content>
