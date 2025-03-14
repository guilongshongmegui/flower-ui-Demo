using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerModels.Entitys
{
	public class DomeTable
	{
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public long Id { get; set; }

		[SugarColumn(IsNullable = false)]
		public decimal Price { get; set; }
	}
}
