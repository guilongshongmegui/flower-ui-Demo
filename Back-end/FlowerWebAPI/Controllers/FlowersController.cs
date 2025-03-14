using AutoMapper;
using FlowerModels;
using FlowerModels.Entitys;
using FlowerService.Flowers;
using FlowerService.Flowers.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerWebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class FlowersController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;
		private IFlowersService _flowerService; 
		//通过构造函数将Service层的获取鲜花数据的方法注入进来
		public FlowersController(IFlowersService flowerService,ILogger<OrderController> _logger)  
		{ 
			this._flowerService = flowerService;
			this._logger = _logger;
		}


		[HttpPost]  //因为此处时需要传一个FlowerReq对象，所以需要使用Post
		public ApiResult GetFlowers(FlowerReq req)//再在接口中使用它
		{
			try
			{
				ApiResult result = new ApiResult() { IsSuccess = true };
				result.Result = _flowerService.GetFlowers(req);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogInformation("this is 获取鲜花详情 GetFlowers run ......………………………………........." + ex.Message);
				return new ApiResult();
			}
		}

		[HttpPost]
		public ApiResult GetFlowerType(FlowerReq req)//再在接口中使用它
		{
			try
			{
				ApiResult result = new ApiResult() { IsSuccess = true };
				result.Result = _flowerService.GetFlowersType(req);
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogInformation("this is 获取鲜花列表 GetFlowerType run ......………………………………........." + ex.Message);
				return new ApiResult();
			}
		}
		
	}
}
