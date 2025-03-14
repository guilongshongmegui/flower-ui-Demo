using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerModels.Entitys
{
	public class CIDs
	{
		[SugarColumn(IsNullable = false)]
		public string UserId { get; set; }

		[SugarColumn(IsNullable = true)]
		public string ConnectionId { get; set; }
	}
}
