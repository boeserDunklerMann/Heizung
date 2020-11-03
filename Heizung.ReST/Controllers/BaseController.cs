using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heizung.ReST.Controllers
{
	public abstract class BaseController : ControllerBase
	{
		protected readonly Heizung.DBAccess.MySql _database = DBAccess.MySql.Instance;
		protected readonly ILogger _logger;
		protected readonly IConfiguration _cfg;

		public BaseController(ILogger<BaseController> logger, IConfiguration cfg)
		{
			_logger = logger;
			_cfg = cfg;
			_database.SetConnection(_cfg.GetValue<string>("DBSettings:User"),
				_cfg.GetValue<string>("DBSettings:Pass"),
				_cfg.GetValue<string>("DBSettings:Host"),
				_cfg.GetValue<string>("DBSettings:DB"));
		}
	}
}
