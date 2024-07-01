using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmUbicaciones : System.Web.UI.Page
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;
        private EventosDataContext db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new EventosDataContext(cadena);

            if (!IsPostBack)
            {
                CargarUbicaciones();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreUbicacion = txtNombreUbicacion.Text;
            string direccion = txtDireccion.Text;
            string ciudad = txtCiudad.Text;
            string estado = txtEstado.Text;
            string codigoPostal = txtCodigoPostal.Text;

            if (string.IsNullOrEmpty(HiddenFieldUbicacionID.Value))
            {
                // Crear nueva ubicación
                db.sp_CrearUbicacion(nombreUbicacion, direccion, ciudad, estado, codigoPostal);
                lblMensaje.Text = "Ubicación creada exitosamente.";
                lblMensaje.CssClass = "alert alert-success";
            }
            else
            {
                // Actualizar ubicación existente
                int ubicacionId = int.Parse(HiddenFieldUbicacionID.Value);
                db.sp_ActualizarUbicacion(ubicacionId, nombreUbicacion, direccion, ciudad, estado, codigoPostal);
                lblMensaje.Text = "Ubicación actualizada exitosamente.";
                lblMensaje.CssClass = "alert alert-success";

                HiddenFieldUbicacionID.Value = string.Empty;
            }

            CargarUbicaciones();
        }

        private void CargarUbicaciones()
        {
            var ubicaciones = db.sp_LeerUbicaciones().ToList();
            GridView1.DataSource = ubicaciones;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            CargarUbicaciones();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            CargarUbicaciones();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int ubicacionId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string nombreUbicacion = ((TextBox)row.Cells[1].Controls[0]).Text;
            string direccion = ((TextBox)row.Cells[2].Controls[0]).Text;
            string ciudad = ((TextBox)row.Cells[3].Controls[0]).Text;
            string estado = ((TextBox)row.Cells[4].Controls[0]).Text;
            string codigoPostal = ((TextBox)row.Cells[5].Controls[0]).Text;

            db.sp_ActualizarUbicacion(ubicacionId, nombreUbicacion, direccion, ciudad, estado, codigoPostal);
            lblMensaje.Text = "Ubicación actualizada exitosamente.";
            lblMensaje.CssClass = "alert alert-success";

            GridView1.EditIndex = -1;
            CargarUbicaciones();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ubicacionId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            db.sp_EliminarUbicacion(ubicacionId);
            lblMensaje.Text = "Ubicación eliminada exitosamente.";
            lblMensaje.CssClass = "alert alert-success";

            CargarUbicaciones();
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
