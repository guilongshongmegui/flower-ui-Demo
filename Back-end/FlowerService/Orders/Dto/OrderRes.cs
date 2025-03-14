﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.Orders.Dto
{
	public class OrderRes
	{
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 订单号
		/// </summary>
		public string OrderNumber { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// 订单日期
		/// </summary>
		public DateTime OrderDate { get; set; }
		/// <summary>
		/// 鲜花标题
		/// </summary>
		public string FlowerTitle { get; set; }
		/// <summary>
		/// 用户Id
		/// </summary>
		public long UserId { get; set; }
	}
}
