using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using UserDome.Modle;

namespace UserDome.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class DomeController : ControllerBase
	{
		private ISqlSugarClient Db { get; }
		public DomeController(ISqlSugarClient db)
		{
			this.Db = db;
		}

		[HttpGet]
		public bool AddPeolpe()
		{
			DomeTable domeTable = new();
			domeTable.Id = 4;
			domeTable.number = 225;

			return Db.Insertable(domeTable).ExecuteCommand() > 0;
		}
	}
}
