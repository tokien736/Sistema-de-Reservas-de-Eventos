using System;
using System.Web.Security;

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmSistemaAdministrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CodUsuario"] == null)
            {
                // Redirigir a la página de inicio de sesión si el usuario no está logueado
                Response.Redirect("frmLogin.aspx");
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("frmLogin.aspx");
        }
    }
}
