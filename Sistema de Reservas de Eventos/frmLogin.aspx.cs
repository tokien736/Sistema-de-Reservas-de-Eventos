using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmLogin : System.Web.UI.Page
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string username = Login1.UserName;
            string password = Login1.Password;

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_usuario", username);
                    cmd.Parameters.AddWithValue("@contrasena", password);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string rol = reader["Rol"].ToString();
                        int codError = Convert.ToInt32(reader["CodError"]);
                        string mensaje = reader["Mensaje"].ToString();

                        if (codError == 0)
                        {
                            // Establecer la sesión del usuario
                            Session["CodUsuario"] = username;
                            Session["Rol"] = rol;

                            // Redirigir según el rol obtenido
                            switch (rol)
                            {
                                case "admin":
                                    FormsAuthentication.RedirectFromLoginPage(username, false);
                                    Response.Redirect("frmSistemaAdministrador.aspx");
                                    break;
                                case "user":
                                    FormsAuthentication.RedirectFromLoginPage(username, false);
                                    Response.Redirect("frmSistemaUsuario.aspx");
                                    break;
                                default:
                                    Login1.FailureText = "Rol de usuario no reconocido.";
                                    break;
                            }
                        }
                        else
                        {
                            // Mostrar mensaje de error de autenticación
                            Login1.FailureText = mensaje;
                        }
                    }
                    else
                    {
                        // Mostrar mensaje de error de autenticación genérico
                        Login1.FailureText = "Error en la autenticación.";
                    }
                }
            }
        }
    }
}
