using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Heizung.ReST.Controllers
{
	[Route("api/heizung/[controller]")]
	[ApiController]
	public class MesspunktController : BaseController
	{
		public MesspunktController(ILogger<MesspunktController> logger, IConfiguration cfg) :base(logger, cfg)
		{
		}

		// Getter brauchen wir nicht, weil die Daten alle schon aus dem WohnungController kommen.

		[HttpPut]
		public Model.Messpunkt WriteMesspunkt(Model.Messpunkt messpunkt)
		{
			_logger.LogInformation($"HttpPut at {DateTime.UtcNow} UTC for {nameof(MesspunktController)}");
			_database.WriteModel(messpunkt);
			return messpunkt;
		}
	}
}
