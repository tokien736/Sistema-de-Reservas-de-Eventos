using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmSistemaUsuario : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CodUsuario"] != null)
            {
                string codUsuario = Session["CodUsuario"].ToString();

                if (!IsPostBack)
                {
                    lblNombreUsuario.Text = "Bienvenido Usuario: " + codUsuario;
                    CargarPerfilUsuario(codUsuario);
                    CargarEventosDisponibles();
                    CargarEventosRegistrados(codUsuario);
                }
            }
            else
            {
                // Redirigir a la página de inicio de sesión si el usuario no está logueado
                Response.Redirect("frmLogin.aspx");
            }
        }

        private void CargarPerfilUsuario(string codUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.nombre, p.apellido, u.correo_electronico 
                    FROM TPerfiles_usuarios p
                    JOIN TUsuarios u ON p.usuario_id = u.usuario_id
                    WHERE u.nombre_usuario = @nombre_usuario";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@nombre_usuario", codUsuario);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblNombre.Text = reader["nombre"].ToString();
                        lblApellido.Text = reader["apellido"].ToString();
                        lblCorreoElectronico.Text = reader["correo_electronico"].ToString();
                    }
                    else
                    {
                        lblNombre.Text = "Usuario no encontrado.";
                        lblApellido.Text = "";
                        lblCorreoElectronico.Text = "";
                    }
                }
            }
        }

        private void CargarEventosDisponibles()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT evento_id, titulo FROM TEventos";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlEventos.DataSource = reader;
                    ddlEventos.DataTextField = "titulo";
                    ddlEventos.DataValueField = "evento_id";
                    ddlEventos.DataBind();
                }
            }
        }

        private void CargarEventosRegistrados(string codUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT e.titulo, e.fecha_evento, 
                           CONVERT(VARCHAR(8), e.hora_inicio, 108) AS hora_inicio, 
                           CONVERT(VARCHAR(8), e.hora_fin, 108) AS hora_fin
                    FROM TReservas r
                    JOIN TEventos e ON r.evento_id = e.evento_id
                    JOIN TUsuarios u ON r.usuario = u.nombre_usuario
                    WHERE u.nombre_usuario = @nombre_usuario";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@nombre_usuario", codUsuario);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    GridViewEventosRegistrados.DataSource = reader;
                    GridViewEventosRegistrados.DataBind();
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string username = Session["CodUsuario"].ToString();
            int eventoId = int.Parse(ddlEventos.SelectedValue);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TReservas (usuario, evento_id) VALUES (@usuario, @evento_id)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@usuario", username);
                    cmd.Parameters.AddWithValue("@evento_id", eventoId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMensajeRegistro.Text = "Registro exitoso.";
            lblMensajeRegistro.CssClass = "alert alert-success";
            CargarEventosRegistrados(username);
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("frmLogin.aspx");
        }
    }
}
