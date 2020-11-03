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
		public WohnungController(ILogger<WohnungController> logger, IConfiguration cfg) : base(logger, cfg)
		{
		}

		// GET: /api/heizung/Wohnung/
		[HttpGet]
		/// <summary>
		/// Liefert die Wohnungen, incl. aller Räume, Messpunkte und Werte
		/// </summary>
		public IEnumerable<Model.Wohnung> GetAll()
		{
			_logger.LogInformation($"HttpGet at {DateTime.UtcNow} UTC for {nameof(WohnungController)}");
			return _database.LoadAll();
		}

		/// <summary>
		/// Schreibt eine Wohnung, (incl. aller Räume, Messpunkte, Werte - leider funktioniert das aber nicht)
		/// </summary>
		/// <param name="wohnung"></param>
		//PUT: api/heizung/Wohnung/
		[HttpPut]
		public void WriteWohnung(Model.Wohnung wohnung)
		{
			_logger.LogInformation($"HttpPut at {DateTime.UtcNow} UTC for {nameof(WohnungController)}");
			_logger.LogInformation($"Found {wohnung.Raeume.Count} raeume");
			_database.WriteWohnung(wohnung);
		}
	}
}