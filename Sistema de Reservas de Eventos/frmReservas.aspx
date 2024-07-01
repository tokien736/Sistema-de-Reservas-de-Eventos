<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReservas.aspx.cs" Inherits="Sistema_de_Reservas_de_Eventos.frmReservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <!-- Grupo de tabla y edición -->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="reserva_id" CssClass="table table-striped"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="reserva_id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                <asp:BoundField DataField="evento_id" HeaderText="Evento ID" />
                <asp:BoundField DataField="fecha_reserva" HeaderText="Fecha de Reserva" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="Estado">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                            <asp:ListItem Text="Confirmada" Value="Confirmada"></asp:ListItem>
                            <asp:ListItem Text="Cancelada" Value="Cancelada"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("estado") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <!-- Grupo de guardado -->
        <asp:ValidationSummary ID="ValidationSummaryGuardar" runat="server" CssClass="alert alert-danger" HeaderText="Errores de Guardado:" ValidationGroup="Guardar" />

        <div class="form-group mt-4">
            <label for="txtUsuario">Usuario:</label>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="El usuario es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <div class="form-group">
            <label for="txtEventoID">Evento ID:</label>
            <asp:TextBox ID="txtEventoID" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvEventoID" runat="server" ControlToValidate="txtEventoID" ErrorMessage="El ID del evento es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="Guardar" />
        </div>
        <asp:HiddenField ID="HiddenFieldReservaID" runat="server" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" ValidationGroup="Guardar" />

        <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3"></asp:Label>
    </div>
</asp:Content>
