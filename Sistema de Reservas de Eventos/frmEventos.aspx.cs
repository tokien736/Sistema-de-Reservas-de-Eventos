using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmEventos : System.Web.UI.Page
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;
        private EventosDataContext db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new EventosDataContext(cadena);

            if (!IsPostBack)
            {
                CargarEventos();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string titulo = txtTitulo.Text;
            string descripcion = txtDescripcion.Text;
            DateTime fechaEvento;
            TimeSpan horaInicio, horaFin;
            int ubicacionId;
            string creadoPor = txtCreadoPor.Text;

            // Parsing and validation
            if (!DateTime.TryParse(txtFechaEvento.Text, out fechaEvento))
            {
                lblMensaje.Text = "Formato de fecha inválido.";
                lblMensaje.CssClass = "alert alert-danger";
                return;
            }
            if (!TimeSpan.TryParse(txtHoraInicio.Text, out horaInicio))
            {
                lblMensaje.Text = "Formato de hora de inicio inválido.";
                lblMensaje.CssClass = "alert alert-danger";
                return;
            }
            if (!TimeSpan.TryParse(txtHoraFin.Text, out horaFin))
            {
                lblMensaje.Text = "Formato de hora de fin inválido.";
                lblMensaje.CssClass = "alert alert-danger";
                return;
            }
            if (!int.TryParse(txtUbicacionID.Text, out ubicacionId))
            {
                lblMensaje.Text = "Formato de ID de ubicación inválido.";
                lblMensaje.CssClass = "alert alert-danger";
                return;
            }

            if (string.IsNullOrEmpty(HiddenFieldEventoID.Value))
            {
                // Crear nuevo evento
                db.sp_CrearEvento(titulo, descripcion, fechaEvento, horaInicio, horaFin, ubicacionId, creadoPor);
                lblMensaje.Text = "Evento creado exitosamente.";
                lblMensaje.CssClass = "alert alert-success";
            }
            else
            {
                // Actualizar evento existente
                int eventoId = int.Parse(HiddenFieldEventoID.Value);
                db.sp_ActualizarEvento(eventoId, titulo, descripcion, fechaEvento, horaInicio, horaFin, ubicacionId, creadoPor);
                lblMensaje.Text = "Evento actualizado exitosamente.";
                lblMensaje.CssClass = "alert alert-success";
                HiddenFieldEventoID.Value = string.Empty;
            }

            CargarEventos();
        }

        private void CargarEventos()
        {
            var eventos = db.sp_LeerEventos().ToList();

            var eventosFormateados = eventos.Select(e => new
            {
                e.evento_id,
                e.titulo,
                e.descripcion,
                fecha_evento = e.fecha_evento.ToString("yyyy-MM-dd"),
                hora_inicio = e.hora_inicio.ToString(@"hh\:mm\:ss"),
                hora_fin = e.hora_fin.ToString(@"hh\:mm\:ss"),
                e.ubicacion_id,
                e.creado_por
            }).ToList();

            GridView1.DataSource = eventosFormateados;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            CargarEventos();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            CargarEventos();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int eventoId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string titulo = ((TextBox)row.Cells[1].Controls[0]).Text;
            string descripcion = ((TextBox)row.Cells[2].Controls[0]).Text;
            DateTime fechaEvento = DateTime.Parse(((TextBox)row.Cells[3].Controls[0]).Text);
            TimeSpan horaInicio = TimeSpan.Parse(((TextBox)row.Cells[4].Controls[0]).Text);
            TimeSpan horaFin = TimeSpan.Parse(((TextBox)row.Cells[5].Controls[0]).Text);
            int ubicacionId = int.Parse(((TextBox)row.Cells[6].Controls[0]).Text);
            string creadoPor = ((TextBox)row.Cells[7].Controls[0]).Text;

            db.sp_ActualizarEvento(eventoId, titulo, descripcion, fechaEvento, horaInicio, horaFin, ubicacionId, creadoPor);
            lblMensaje.Text = "Evento actualizado exitosamente.";
            lblMensaje.CssClass = "alert alert-success";

            GridView1.EditIndex = -1;
            CargarEventos();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int eventoId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            db.sp_EliminarEvento(eventoId);
            lblMensaje.Text = "Evento eliminado exitosamente.";
            lblMensaje.CssClass = "alert alert-success";

            CargarEventos();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (db != null)
            {
                db.Dispose();
            }
        }
    }
}
