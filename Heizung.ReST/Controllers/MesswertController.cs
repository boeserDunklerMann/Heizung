using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Heizung.ReST.Controllers
{
	[Route("api/heizung/[controller]")]
	[ApiController]
	public class MesswertController : BaseController
	{
		public MesswertController(ILogger<MesswertController> logger, IConfiguration cfg) : base(logger, cfg)
		{
		}

		// Getter brauchen wir nicht, weil die Daten alle schon aus dem WohnungController kommen.

		[HttpPut]
		public void WriteMesspunkt(Model.MessWert messWert)
		{
			_logger.LogInformation($"HttpPut at {DateTime.UtcNow} UTC for {nameof(MesswertController)}");
			_database.WriteModel(messWert);
		}
	}
}