using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_de_Reservas_de_Eventos
{
    public class UsuarioPorCorreo
    {
        public int usuario_id { get; set; }
        public string nombre_usuario { get; set; }
        public string correo_electronico { get; set; }
        public string rol { get; set; }
    }
}