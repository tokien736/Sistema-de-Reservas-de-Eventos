using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmReservas : System.Web.UI.Page
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;
        private EventosDataContext db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new EventosDataContext(cadena);

            if (!IsPostBack)
            {
                CargarReservas();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            int eventoId = int.Parse(txtEventoID.Text);

            if (string.IsNullOrEmpty(HiddenFieldReservaID.Value))
            {
                // Crear nueva reserva
                db.sp_CrearReserva(usuario, eventoId);
                lblMensaje.Text = "Reserva creada exitosamente.";
                lblMensaje.CssClass = "alert alert-success";
            }
            else
            {
                // Actualizar reserva existente
                int reservaId = int.Parse(HiddenFieldReservaID.Value);
                string estado = ((DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("ddlEstado")).SelectedValue;

                db.sp_ActualizarReserva(reservaId, estado);
                lblMensaje.Text = "Reserva actualizada exitosamente.";
                lblMensaje.CssClass = "alert alert-success";

                HiddenFieldReservaID.Value = string.Empty;
            }

            CargarReservas();
        }

        private void CargarReservas()
        {
            var reservas = db.sp_LeerReservas().ToList();
            GridView1.DataSource = reservas;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            CargarReservas();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            CargarReservas();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int reservaId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string estado = ((DropDownList)row.FindControl("ddlEstado")).SelectedValue;

            db.sp_ActualizarReserva(reservaId, estado);
            lblMensaje.Text = "Reserva actualizada exitosamente.";
            lblMensaje.CssClass = "alert alert-success";

            GridView1.EditIndex = -1;
            CargarReservas();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int reservaId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            db.sp_EliminarReserva(reservaId);
            lblMensaje.Text = "Reserva eliminada exitosamente.";
            lblMensaje.CssClass = "alert alert-success";

            CargarReservas();
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
