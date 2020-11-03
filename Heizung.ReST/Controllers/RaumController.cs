using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Heizung.ReST.Controllers
{
	[Route("api/heizung/[controller]")]
	[ApiController]
	public class RaumController : BaseController
	{
		public RaumController(ILogger<RaumController> logger, IConfiguration cfg) :base(logger, cfg)
		{
		}

		/// <summary>
		/// Liefert die Räume einer Wohnung
		/// </summary>
		/// <param name="wohnung"></param>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Model.Raum> GetAll(Model.Wohnung wohnung)
		{
			_logger.LogInformation($"HttpGet at {DateTime.UtcNow} UTC for {nameof(RaumController)}");
			return _database.LoadRaeume(wohnung);
		}

		[HttpPut]
		public Model.Raum WriteRaum(Model.Raum raum)
		{
			_logger.LogInformation($"HttpPut at {DateTime.UtcNow} UTC for {nameof(RaumController)}");
			_database.WriteModel(raum);
			return raum;
		}
	}
}
