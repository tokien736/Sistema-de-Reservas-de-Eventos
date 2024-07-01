using System;  
using System.Linq; 
using System.Web.UI.DataVisualization.Charting; // Importa el espacio de nombres para trabajar con gráficos en una aplicación web
using System.Configuration; 

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmEstadisticas : System.Web.UI.Page
    {
        
        private static string cadena = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;
        
        private EventosDataContext db;

        protected void Page_Load(object sender, EventArgs e)
        {      
            db = new EventosDataContext(cadena);
            if (!IsPostBack)
            {
                // Carga los datos para los gráficos
                CargarDatosGraficoEventosPopulares();
                CargarDatosGraficoEstadisticasParticipacion();
            }
        }

        // Método para cargar los datos del gráfico de eventos populares
        private void CargarDatosGraficoEventosPopulares()
        {
            // Obtiene los datos de eventos populares desde la base de datos
            var eventosPopulares = db.eventos_populares.ToList();

            // Limpia las series existentes en el gráfico
            chartEventosPopulares.Series.Clear();
            // Crea una nueva serie para los datos de eventos populares
            var series = new Series("Total");
            series.ChartType = SeriesChartType.Column;

            // Agrega los puntos de datos a la serie
            foreach (var evento in eventosPopulares)
            {
                series.Points.AddXY(evento.titulo, evento.total_reservas);
            }

            // Agrega la serie al gráfico
            chartEventosPopulares.Series.Add(series);
        }

        // Método para cargar los datos del gráfico de estadísticas de participación
        private void CargarDatosGraficoEstadisticasParticipacion()
        {
            // Obtiene los datos de estadísticas de participación desde la base de datos
            var estadisticasParticipacion = db.estadisticas_participacion_evento.ToList();

            // Limpia las series existentes en el gráfico
            chartEstadisticasParticipacion.Series.Clear();
            // Crea una nueva serie para los datos de participación
            var series = new Series("Participación");
            series.ChartType = SeriesChartType.Pie;

            // Agrega los puntos de datos a la serie
            foreach (var estadistica in estadisticasParticipacion)
            {
                series.Points.AddXY(estadistica.titulo, estadistica.total_participacion);
            }

            // Agrega la serie al gráfico
            chartEstadisticasParticipacion.Series.Add(series);
        }
    }
}
