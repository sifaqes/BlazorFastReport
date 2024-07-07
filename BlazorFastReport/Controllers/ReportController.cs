using BlazorFastReport.Data;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFastReport.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly BlazorDbContext _context;
		private readonly IConfiguration _configuration;
		public ReportController(IWebHostEnvironment webHostEnvironment, BlazorDbContext context, IConfiguration configuration)
		{
			_webHostEnvironment = webHostEnvironment;
			_context = context;
			_configuration = configuration;
		}
		[HttpGet("Index")]
		public IActionResult Index()
		{
			WebReport web = new WebReport();
			var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\Product.frx";
			web.Report.Load(path);


			// Send ConnectionString to the report
			var connection = new MsSqlDataConnection();
			connection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
			var conn = connection.ConnectionString;
			web.Report.SetParameterValue("CONN", conn);

			Dictionary<string, string> parametersList = new Dictionary<string, string>();
			parametersList.Add("CONN", conn.ToString());

			foreach (var item in parametersList)
			{
				web.Report.SetParameterValue(item.Key, item.Value);
			}

			// Prepar the report
			web.Report.Prepare();

			Stream stream = new MemoryStream();
			web.Report.Export(new PDFSimpleExport(), stream);
			stream.Position = 0;
			return File(stream, "application/zip", "report.pdf"); // "Product.pdf" is the name of the file that will be downloaded

		}
	}
}
