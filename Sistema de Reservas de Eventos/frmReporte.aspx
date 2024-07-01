<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporte.aspx.cs" Inherits="Sistema_de_Reservas_de_Eventos.frmReporte" %>
<%@ Register Assembly="System.Web.DataVisualization" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Eventos Populares</h2>
    <asp:Chart ID="chartEventosPopulares" runat="server" Width="600px" Height="400px">
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
        </ChartAreas>
        <Legends>
            <asp:Legend Name="Legend1"></asp:Legend>
        </Legends>
    </asp:Chart>

    <h2>Estadísticas de Participación</h2>
    <asp:Chart ID="chartEstadisticasParticipacion" runat="server" Width="600px" Height="400px">
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
        </ChartAreas>
        <Legends>
            <asp:Legend Name="Legend1"></asp:Legend>
        </Legends>
    </asp:Chart>

    <iframe id="pdfPreview" runat="server" style="width: 100%; height: 600px;"></iframe>
</asp:Content>
