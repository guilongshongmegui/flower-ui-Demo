using FlowerModels;
using FlowerModels.Entitys;
using FlowerService.Jwt;
using FlowerService.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhaoxiFlower.Service.User.Dto;

namespace FlowerWebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;
		public IUserService _userService;
		public ICustomJWTService _customJWTService;
		public LoginController(IUserService userService, ICustomJWTService customJWTService, ILogger<OrderController> logger)
		{
			this._userService = userService;
			this._customJWTService = customJWTService;
			_logger = logger;
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		[HttpPost]
		public ApiResult Check(UserReq req)
		{
			ApiResult apiResult = new ApiResult() { IsSuccess = false };
			if (string.IsNullOrEmpty(req.UserName) || string.IsNullOrEmpty(req.Password))
			{
				apiResult.Msg = "参数不能为空！";
			}
			else
			{
				UserRes user = _userService.GetUsers(req);
				if (string.IsNullOrEmpty(user.UserName))
				{
					apiResult.Msg = "账号不存在，用户名或密码错误！";
				}
				else
				{
					apiResult.IsSuccess = true;
					apiResult.Result = _customJWTService.GetToken(user);
				}
			}
			return apiResult;
		}

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		[HttpPost]
		public ApiResult Register(RegisterReq req)
		{
			ApiResult apiResult = new ApiResult() { IsSuccess = false };
			if (string.IsNullOrEmpty(req.UserName) || string.IsNullOrEmpty(req.Password) || string.IsNullOrEmpty(req.NickName))
			{
				apiResult.Msg = "参数不能为空！";
			}
			else
			{
				try
				{
					string msg = string.Empty;
					var res = _userService.RegisterUser(req, ref msg);
					if (!string.IsNullOrEmpty(msg))
					{
						apiResult.Msg = msg;
					}
					else
					{
						apiResult.IsSuccess = true;
						apiResult.Result = _customJWTService.GetToken(res);
					}
				}
				catch (Exception ex)
				{
					_logger.LogInformation("this is Register run ......………………………………........."+ex.Message);

					throw;
				}
			}
			return apiResult;
		}


		//验证是否已登陆
		[HttpPost]
		[Authorize]
		public bool PinUser()
		{
			return true;
		}
	}
}
