using FlowerService.Orders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.Orders
{
	public interface IOrderService
	{
		bool CreateOrder(OrderReq req, long userId, ref string msg);
		List<OrderRes> GetOrder(long userId);
	}
}
