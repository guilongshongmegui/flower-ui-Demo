using FlowerModels;
using FlowerService.Orders;
using FlowerService.Orders.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerWebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	
	public class OrderController : ControllerBase
	{

		private readonly ILogger<OrderController> _logger;
		private IOrderService _orderService;
		public OrderController(IOrderService orderService, ILogger<OrderController> logger)
		{
			_orderService = orderService;
			_logger = logger;
		}

		/// <summary>
		/// 创建订单
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		[Authorize]  //登录验证特性
		[HttpPost]
		public ApiResult CreateOrder(OrderReq req)
		{
			ApiResult apiResult = new ApiResult() { IsSuccess = false };
			if (req.FlowerId == 0)
			{
				apiResult.Msg = "参数不可以为空！";
			}
			else 
			{
				string msg = string.Empty;
				long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
				bool res = _orderService.CreateOrder(req, userId, ref msg);
				if (!string.IsNullOrEmpty(msg))
				{
					apiResult.Msg = msg;
				}
				else
				{
					apiResult.IsSuccess = res;
				}
			}
			return apiResult;
		}

		/// <summary>
		/// 查询订单
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ApiResult GetOrder()
		{
			_logger.LogInformation("this is GetOrder ......");
			ApiResult apiResult = new ApiResult() { IsSuccess = true };
			try
			{
				//通过请求的上下文获取用户Id
				long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
				apiResult.Result = _orderService.GetOrder(userId);
				_logger.LogInformation("this is GetOrder ......");
			}
			catch (Exception ex)
			{
				_logger.LogInformation("this is GetOrder …… error: ......"+ex.Message);
				apiResult.IsSuccess = false;
				apiResult.Msg = ex.Message;
			}
			return apiResult;
		}

	}
}
