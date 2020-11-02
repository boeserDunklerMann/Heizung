using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heizung.ReST.Controllers
{
	[Route("api/heizung/[controller]")]
	[ApiController]
	public class WohnungController : ControllerBase
	{
		private readonly Heizung.DBAccess.MySql _database = DBAccess.MySql.Instance;
		private readonly ILogger _logger;
		private readonly IConfiguration _cfg;

		public WohnungController(ILogger<WohnungController> logger, IConfiguration cfg)
		{
			_logger = logger;
			_cfg = cfg;
			_database.SetConnection(_cfg.GetValue<string>("DBSettings:User"),
				_cfg.GetValue<string>("DBSettings:Pass"),
				_cfg.GetValue<string>("DBSettings:Host"),
				_cfg.GetValue<string>("DBSettings:DB"));
		}

		// GET: /api/heizung/Wohnung/all
		[HttpGet]
		[Route("api/heizung/[controller]/all")]
		public IEnumerable<Model.Wohnung> GetAll()
		{
			_logger.LogInformation($"HttpGet at {DateTime.UtcNow} UTC for {nameof(WohnungController)}");
			return _database.LoadAll();
		}
	}
}
