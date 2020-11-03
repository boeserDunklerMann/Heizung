using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Heizung.ReST.Controllers
{
	[Route("api/heizung/[controller]")]
	[ApiController]
	public class WohnungController : BaseController
	{
		public WohnungController(ILogger<WohnungController> logger, IConfiguration cfg) :base(logger, cfg)
		{
		}

		// GET: /api/heizung/Wohnung/all
		[HttpGet]
		/// <summary>
		/// Liefert die Wohnungen, incl. aller Räume, Messpunkte und Werte
		/// </summary>
		public IEnumerable<Model.Wohnung> GetAll()
		{
			_logger.LogInformation($"HttpGet at {DateTime.UtcNow} UTC for {nameof(WohnungController)}");
			return _database.LoadAll();
		}
	}
}
