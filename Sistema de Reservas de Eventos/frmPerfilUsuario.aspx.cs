using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmPerfilUsuario : System.Web.UI.Page
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;
        private EventosDataContext db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new EventosDataContext(cadena);

            if (!IsPostBack)
            {
                CargarPerfiles();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string telefono = txtTelefono.Text;
            string direccion = txtDireccion.Text;

            if (string.IsNullOrEmpty(HiddenFieldPerfilID.Value))
            {
                // Crear nuevo perfil
                int resultado = db.sp_CrearPerfilUsuario(0, nombre, apellido, telefono, direccion); // Ajusta usuario_id según sea necesario
                if (resultado == 1)
                {
                    lblMensaje.Text = "Perfil creado exitosamente.";
                    lblMensaje.CssClass = "alert alert-success";
                }
                else
                {
                    lblMensaje.Text = "Error al crear el perfil.";
                    lblMensaje.CssClass = "alert alert-danger";
                }
            }
            else
            {
                // Actualizar perfil existente
                int perfilId = int.Parse(HiddenFieldPerfilID.Value);
                int resultado = db.sp_ActualizarPerfilUsuario(perfilId, nombre, apellido, telefono, direccion);

                if (resultado == 1)
                {
                    lblMensaje.Text = "Perfil actualizado exitosamente.";
                    lblMensaje.CssClass = "alert alert-success";
                }
                else
                {
                    lblMensaje.Text = "Error al actualizar el perfil.";
                    lblMensaje.CssClass = "alert alert-danger";
                }

                HiddenFieldPerfilID.Value = string.Empty;
            }

            CargarPerfiles();
        }

        private void CargarPerfiles()
        {
            var perfiles = db.sp_LeerPerfilesUsuarios().ToList();
            GridView1.DataSource = perfiles;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            CargarPerfiles();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            CargarPerfiles();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int perfilId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string nombre = ((TextBox)row.Cells[2].Controls[0]).Text;
            string apellido = ((TextBox)row.Cells[3].Controls[0]).Text;
            string telefono = ((TextBox)row.Cells[4].Controls[0]).Text;
            string direccion = ((TextBox)row.Cells[5].Controls[0]).Text;

            int resultado = db.sp_ActualizarPerfilUsuario(perfilId, nombre, apellido, telefono, direccion);

            if (resultado == 1)
            {
                lblMensaje.Text = "Perfil actualizado exitosamente.";
                lblMensaje.CssClass = "alert alert-success";
            }
            else
            {
                lblMensaje.Text = "Error al actualizar el perfil.";
                lblMensaje.CssClass = "alert alert-danger";
            }

            GridView1.EditIndex = -1;
            CargarPerfiles();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int perfilId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            int resultado = db.sp_EliminarPerfilUsuario(perfilId);
            if (resultado == 1)
            {
                lblMensaje.Text = "Perfil eliminado exitosamente.";
                lblMensaje.CssClass = "alert alert-success";
            }
            else
            {
                lblMensaje.Text = "Error al eliminar el perfil.";
                lblMensaje.CssClass = "alert alert-danger";
            }

            CargarPerfiles();
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
