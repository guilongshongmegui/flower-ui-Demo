using FlowerModels;
using FlowerService.Friends;
using FlowerService.Friends.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerWebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class FriendsController : ControllerBase
	{
		private IFriendService _friendService;
		private readonly ILogger<FriendsController> _logger;
		public FriendsController(IFriendService friendServer, ILogger<FriendsController> logger)
		{
			_friendService = friendServer;
			_logger = logger;
		}

		//搜索用户
		[HttpPost]
		public ApiResult Getfriend(FriendReq gedddtId)
		{
			try
			{
				ApiResult results = new ApiResult() { IsSuccess = true };
				results.Result = _friendService.GetFriendRes(gedddtId.id);
				return results;
			}
			catch (Exception)
			{
				return new ApiResult() { IsSuccess = false };
			}
		}



		/// <summary>
		/// 添加好友
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public ApiResult AddFriend(FriendReq req)
		{
			ApiResult result = new ApiResult() { IsSuccess = false };
			try
			{
				long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);

				//添加到关系表中
				bool a = _friendService.RequestAdd(userId, req.id);
				if (a)
				{
					result.IsSuccess = true;
					result.Result = a;
					//刷新两个用户的数据
				}
				else
				{
					result.IsSuccess = true;
					result.Result = false;
				}
				return result;
			}
			catch (Exception)
			{
				return new ApiResult() { IsSuccess = false };
			}

			
		}
	}
}
