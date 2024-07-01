using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmUsuarios : System.Web.UI.Page
    {
        // Cadena de conexión global
        private static string cadena = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;
        private EventosDataContext db;

    

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new EventosDataContext(cadena);

            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string nombreUsuario = TextBox1.Text;
            string contrasena = TextBox2.Text;
            string correoElectronico = TextBox3.Text;
            string rol = DropDownList1.SelectedValue;

            if (string.IsNullOrEmpty(HiddenField1.Value))
            {
                // Crear nuevo usuario
                int resultado = db.sp_CrearUsuario(nombreUsuario, contrasena, correoElectronico, rol);
                if (resultado < 1)
                {
                    lblMensaje.Text = "Usuario creado exitosamente.";
                    lblMensaje.CssClass = "alert alert-success";
                }
                else
                {
                    lblMensaje.Text = "Error al crear el usuario.";
                    lblMensaje.CssClass = "alert alert-danger";
                }
            }
            else
            {
                // Actualizar usuario existente
                int usuarioId = int.Parse(HiddenField1.Value);
                int resultado = db.sp_ActualizarUsuario(usuarioId, nombreUsuario, contrasena, correoElectronico, rol);

                if (resultado < 1)
                {
                    lblMensaje.Text = "Usuario actualizado exitosamente.";
                    lblMensaje.CssClass = "alert alert-success";
                }
                else
                {
                    lblMensaje.Text = "Error al actualizar el usuario.";
                    lblMensaje.CssClass = "alert alert-danger";
                }

                HiddenField1.Value = string.Empty;
            }

            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            var usuarios = db.sp_LeerUsuarios().ToList();
            GridView1.DataSource = usuarios;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            CargarUsuarios();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            CargarUsuarios();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int usuarioId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string nombreUsuario = ((TextBox)row.Cells[1].Controls[0]).Text;
            string correoElectronico = ((TextBox)row.Cells[2].Controls[0]).Text;
            string rol = ((DropDownList)row.Cells[3].FindControl("ddlRol")).SelectedValue;
            string contrasena = ((TextBox)row.Cells[4].FindControl("txtContrasena")).Text;

            int resultado = db.sp_ActualizarUsuario(usuarioId, nombreUsuario, contrasena, correoElectronico, rol);

            if (resultado < 1)
            {
                lblMensaje.Text = "Usuario actualizado exitosamente.";
                lblMensaje.CssClass = "alert alert-success";
            }
            else
            {
                lblMensaje.Text = "Error al actualizar el usuario.";
                lblMensaje.CssClass = "alert alert-danger";
            }

            GridView1.EditIndex = -1;
            CargarUsuarios();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int usuarioId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            int resultado = db.sp_EliminarUsuario(usuarioId);
            if (resultado < 0)
            {
                lblMensaje.Text = "Usuario eliminado exitosamente.";
                lblMensaje.CssClass = "alert alert-success";
            }
            else
            {
                lblMensaje.Text = "Error al eliminar el usuario.";
                lblMensaje.CssClass = "alert alert-danger";
            }

            CargarUsuarios();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtBuscarNombre.Text.Trim();
            string correoElectronico = txtBuscarCorreo.Text.Trim();

            List<UsuarioPorNombre> usuariosPorNombre = new List<UsuarioPorNombre>();
            List<UsuarioPorCorreo> usuariosPorCorreo = new List<UsuarioPorCorreo>();

            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                usuariosPorNombre = db.sp_BuscarUsuariosPorNombre(nombreUsuario)
                    .Select(u => new UsuarioPorNombre
                    {
                        usuario_id = u.usuario_id,
                        nombre_usuario = u.nombre_usuario,
                        correo_electronico = u.correo_electronico,
                        rol = u.rol
                    }).ToList();
            }

            if (!string.IsNullOrEmpty(correoElectronico))
            {
                usuariosPorCorreo = db.sp_BuscarUsuariosPorCorreo(correoElectronico)
                    .Select(u => new UsuarioPorCorreo
                    {
                        usuario_id = u.usuario_id,
                        nombre_usuario = u.nombre_usuario,
                        correo_electronico = u.correo_electronico,
                        rol = u.rol
                    }).ToList();
            }

            var usuarios = usuariosPorNombre.Cast<object>().Union(usuariosPorCorreo.Cast<object>()).ToList();

            GridView1.DataSource = usuarios;
            GridView1.DataBind();
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Text = string.Empty;
            txtBuscarCorreo.Text = string.Empty;
            CargarUsuarios();
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