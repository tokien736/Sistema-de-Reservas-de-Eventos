using System; 
using System.IO; 
using System.Linq; 
using System.Web.UI.DataVisualization.Charting; // Importa el espacio de nombres para trabajar con gráficos en una aplicación web
using System.Configuration;
using iTextSharp.text; // Importa el espacio de nombres para trabajar con la biblioteca iTextSharp para manipulación de documentos PDF
using iTextSharp.text.pdf; // Importa el espacio de nombres para trabajar con la biblioteca iTextSharp para manipulación de documentos PDF

namespace Sistema_de_Reservas_de_Eventos
{
    public partial class frmReporte : System.Web.UI.Page
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["BDEventosConnectionString"].ConnectionString;
        private EventosDataContext db;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            db = new EventosDataContext(cadena);
            if (!IsPostBack)
            {
                // Carga los datos para los gráficos y muestra el PDF
                CargarDatosGraficoEventosPopulares();
                CargarDatosGraficoEstadisticasParticipacion();
                MostrarPDF();
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

        // Método para mostrar el PDF generado
        private void MostrarPDF()
        {
            // Genera el PDF y obtiene la ruta del archivo
            string pdfPath = GeneratePDF(preview: true);
            // Establece la ruta del PDF como fuente del control de vista previa
            pdfPreview.Src = pdfPath;
        }

        // Método para generar el PDF con los datos de los gráficos y las tablas
        private string GeneratePDF(bool preview = false)
        {
            // Obtiene la ruta del directorio temporal donde se guardará el PDF
            string reportesDir = Server.MapPath("~/Temp");
            // Crea el directorio si no existe
            if (!Directory.Exists(reportesDir))
            {
                Directory.CreateDirectory(reportesDir);
            }

            // Establece la ruta completa del archivo PDF
            string filePath = Path.Combine(reportesDir, "Reporte.pdf");

            // Obtiene los datos de eventos populares y estadísticas de participación desde la base de datos
            var eventosPopulares = db.eventos_populares.ToList();
            var estadisticasParticipacion = db.estadisticas_participacion_evento.ToList();

            // Crea y abre un flujo de archivo para escribir el PDF
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Crea un nuevo documento PDF con márgenes especificados
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                // Inicializa un escritor de PDF para escribir en el flujo de archivo
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                // Abre el documento para escribir contenido
                document.Open();

                // Añade título al documento
                document.Add(new Paragraph("Reporte de Eventos Populares y Estadísticas de Participación"));
                document.Add(new Paragraph(" "));

                // Añade tabla de eventos populares
                document.Add(new Paragraph("Eventos Populares"));
                PdfPTable tableEventos = new PdfPTable(2);
                tableEventos.AddCell("Evento");
                tableEventos.AddCell("Total Reservas");

                // Añade filas a la tabla de eventos populares
                foreach (var evento in eventosPopulares)
                {
                    tableEventos.AddCell(evento.titulo);
                    tableEventos.AddCell(evento.total_reservas.ToString());
                }
                document.Add(tableEventos);

                // Añade espacio y descripción del gráfico de eventos populares
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Gráfico de Eventos Populares"));
                document.Add(new Paragraph("Este gráfico muestra los eventos más populares en términos de reservas totales."));
                document.Add(new Paragraph(" "));

                // Guarda el gráfico de eventos populares como imagen y la añade al documento PDF
                using (MemoryStream chart1Stream = new MemoryStream())
                {
                    chartEventosPopulares.SaveImage(chart1Stream, ChartImageFormat.Png);
                    chart1Stream.Position = 0;
                    iTextSharp.text.Image chart1Image = iTextSharp.text.Image.GetInstance(chart1Stream.ToArray());
                    chart1Image.ScaleToFit(500f, 400f);
                    document.Add(chart1Image);
                }

                // Añade espacio y tabla de estadísticas de participación
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Estadísticas de Participación"));
                PdfPTable tableEstadisticas = new PdfPTable(2);
                tableEstadisticas.AddCell("Evento");
                tableEstadisticas.AddCell("Participación");

                // Añade filas a la tabla de estadísticas de participación
                foreach (var estadistica in estadisticasParticipacion)
                {
                    tableEstadisticas.AddCell(estadistica.titulo);
                    tableEstadisticas.AddCell(estadistica.total_participacion.ToString());
                }
                document.Add(tableEstadisticas);

                // Añade espacio y descripción del gráfico de estadísticas de participación
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Gráfico de Estadísticas de Participación"));
                document.Add(new Paragraph("Este gráfico muestra la participación total por evento en formato de gráfico de pastel."));
                document.Add(new Paragraph(" "));

                // Guarda el gráfico de estadísticas de participación como imagen y la añade al documento PDF
                using (MemoryStream chart2Stream = new MemoryStream())
                {
                    chartEstadisticasParticipacion.SaveImage(chart2Stream, ChartImageFormat.Png);
                    chart2Stream.Position = 0;
                    iTextSharp.text.Image chart2Image = iTextSharp.text.Image.GetInstance(chart2Stream.ToArray());
                    chart2Image.ScaleToFit(500f, 400f);
                    document.Add(chart2Image);
                }

                // Cierra el documento y el escritor de PDF
                document.Close();
                writer.Close();
                fs.Close();
            }

            // Retorna la ruta del PDF generado si es para previsualización
            if (preview)
            {
                return $"~/Temp/Reporte.pdf";
            }
            return null;
        }
    }
}
