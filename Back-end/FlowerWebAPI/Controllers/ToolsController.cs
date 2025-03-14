using FlowerCommon;
using FlowerModels;
using FlowerService.Flowers;
using FlowerService.Friends;
using FlowerService.Orders;
using FlowerWebAPI.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace FlowerWebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ToolsController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;
		private IFlowersService _flowerService;
		private IFriendService _friendServer;
		public ToolsController(IFlowersService flowerService, ILogger<OrderController> logger,IFriendService friendService)
		{
			this._flowerService = flowerService;
			_logger = logger;
			_friendServer = friendService;
		}

		[HttpGet]
		public int InitDatabase()
		{
			try
			{
				int a = DBContext.InitDataBase();
				return a;
			}
			catch (Exception ex)
			{
				_logger.LogInformation("this is InitDataBase run ......………………………………........." + ex.Message);
				return -1;
			}
			
		}

		//创建表
		[HttpGet]
		public int CreateTables()
		{
			try
			{
				int a = DBContext.CreaterTable();//创建需要的表
				return a;
			}
			catch (Exception ex)
			{
				_logger.LogInformation("this is CreaterTable run ......………………………………........." + ex.Message);
				return -1;
			}

		}

		[HttpGet]
		public int TryInt()
		{
			/*ChatHub a = new ChatHub();
			a.ReaderFriends("1");*/

			_logger.LogInformation("this is TryInt run ......……………………………….........");
			return 200;
		}

		[HttpGet]
		public ApiResult TryDB()
		{
			ApiResult result = new ApiResult() { IsSuccess = true };
			result.Result = _flowerService.GetDome();
			_logger.LogInformation("this is TryDB run ......………………………………........." + result.Result);
			return result;
		}
	}
}
