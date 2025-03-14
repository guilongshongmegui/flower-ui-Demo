using AutoMapper;
using FlowerCommon;
using FlowerModels.Entitys;
using FlowerService.Orders.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.Orders
{
	public class OrderService : IOrderService
	{
		private readonly IMapper _mapper;
		public OrderService(IMapper mapper)
		{
			_mapper = mapper;
		}


		/// <summary>
		/// 创建订单
		/// </summary>
		/// <param name="req"></param>
		/// <param name="userId"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public bool CreateOrder(OrderReq req, long userId, ref string msg)
		{
			var flower = DBContext.db.Queryable<Flower>().First(p => p.Id == req.FlowerId);
			if (flower == null)
			{
				msg = "商品信息不存在！";
				return false;
			}
			Order order = new Order()
			{
				OrderNumber = DateTime.Now.ToString("yyyyMMddHHmmffff"),
				OrderDate = DateTime.Now,
				UserId = userId,
				FlowerId = req.FlowerId,
				Price = flower.Price
			};
			return DBContext.db.Insertable(order).ExecuteCommand() > 0;
		}

		/// <summary>
		/// 获取订单
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<OrderRes> GetOrder(long userId)
		{
			var order = DBContext.db.Queryable<Order>().Where(p => p.UserId == userId).Select(s => new OrderRes
			{
				Id = s.Id,
				OrderNumber = s.OrderNumber,
				OrderDate = s.OrderDate,
				Price= s.Price,
				FlowerTitle = SqlFunc.Subqueryable<Flower>().Where(f => f.Id == s.FlowerId).Select(f => f.Title)
			}).OrderBy(p => p.OrderDate, OrderByType.Desc).ToList();
			return order;
		}
	}
}
