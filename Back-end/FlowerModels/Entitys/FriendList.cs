using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerModels.Entitys
{
	public class FriendList
	{
		[SugarColumn(IsNullable = false)]
		public long UserId { get; set; }

		[SugarColumn(IsNullable = false)]
		public long FriendId { get; set;}
	}
}
