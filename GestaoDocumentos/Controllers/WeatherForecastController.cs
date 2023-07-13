using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace GestaoDocumentos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Gerar pdf a partir dos dados da requisicao
        /// </summary>
        [HttpGet("pdf")]
        public async Task<IActionResult> GetPdf(int id)
        {
            // Gerar o PDF

            var document = new PdfDocument();

            var invoice = new Cliente()
            {
                Date = DateTime.Now,
                Name = "Renanzinho Teste"
            };

            string htmlContent = "<div style='width:100%'>";
            htmlContent += "<h1>Bon de commande: Réaprovisionnement  </h1>";
            htmlContent += "<h1>Date: " + invoice.Date.ToString("MMMM dd, yyyy") + " </h1>";
            htmlContent += "<p> Furnisher: " + invoice.Name + "</p>";
            htmlContent += "</div>";
            htmlContent += "<h2> Produits </h2>";
            htmlContent += "<table style ='width:100%; border: 1px solid #000'>";
            htmlContent += "<thead style='font-weight:bold'>";
            htmlContent += "<tr>";
            htmlContent += "<td style='border:1px solid #000'> Product Name</td>";
            htmlContent += "<td style='border:1px solid #000'> Refrence </td>";
            htmlContent += "<td style='border:1px solid #000'>Qty</td>";
            htmlContent += "</tr>";
            htmlContent += "</thead >";
            htmlContent += "<tbody>";

            htmlContent += "<tbody>";

            // Gerando PDF a partir de HTML
            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);

            byte[] response = null;

            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            string Filename = "Invoice_" + invoice.Name + ".pdf";

            // Retorna o PDF como um arquivo
            return File(response, "application/pdf", Filename);
        }

    }
}