using FlowerCommon;
using FlowerModels;
using FlowerModels.Entitys;
using FlowerService.cids;
using FlowerService.Friends;
using FlowerService.User;
using FlowerWebAPI.Controllers;
using Microsoft.AspNetCore.Authorization;//
using Microsoft.AspNetCore.SignalR;
using NetTaste;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace FlowerWebAPI.Hubs
{
	public class ChatHub:Hub
	{
		private IFriendService _friendService;
		private ICIDsService _cidsService;
		private IUserService _userService;
		private readonly ILogger<FriendsController> _logger;
		public ChatHub(IFriendService friendService,ICIDsService cIDsService,IUserService userService, ILogger<FriendsController> logger)
		{
			_friendService = friendService;
			_cidsService = cIDsService;
			_userService = userService;
			_logger = logger;
		}

		//给指定用户发送消息
		public async Task SendMessage(string token,string toUserId,string message)
		{
			var time = DateTime.Now;
			//获取到当前Id
			var nowId = GetUserId(token);
			var nowName = _userService.GetNowName(nowId);
			//获取到好友连接对象
			string toConnectionId = _cidsService.GetConnectionId(toUserId);
			var client = Clients.Client(toConnectionId);
			//想好友发送消息
			await client.SendAsync("ReceiveMessage",nowName,message);

			//await Clients.All.SendAsync("ReceiveMessage", user, time.ToString());
		}

		//初始化用户列表
		public async Task ReaderFriends(string tonken)
		{
			//获取到当前Id
			string Id = GetUserId(tonken);
			try
			{
				//存储当前的连接对象
				string connectionId = Context.ConnectionId;
				bool aa = _cidsService.SaveConntionId(Id, connectionId);
				//刷新好友列表
				await FlushList(Id);
				//await Clients.All.SendAsync("ReceiveFriendList", a);//向所有用户发送
			}
			catch (Exception)
			{
				//存储日志
				throw;
			}
			
		}

		//添加好友
		public async Task AgreeFriend(string tonken,string toUserId)
		{
			string Id = GetUserId(tonken);
			long userId = long.Parse(Id);
			long toId =	long.Parse(toUserId);
			try
			{
				//添加到关系表中
				bool a = _friendService.RequestAdd(userId, toId);
				if (a)
				{
					await FlushList(Id);
					await FlushList(toUserId);
				}
				else{}
			}
			catch (Exception){}
		}

		//同意添加
		public async Task agreeReq(string token ,string toUserId)
		{
			string Id = GetUserId(token);
			long NowId = long.Parse(Id);
			long ToId = long.Parse(toUserId);
			try
			{
				bool a = _friendService.RequestAdd(NowId, ToId);
				if (a)
				{
					await FlushList(Id);
					await FlushList(toUserId);
				}
				else
				{
					
				}
			}
			catch (Exception)
			{
				//写入日志
				throw;
			}
		}

		//刷新好友列表
		public async Task FlushList(string Id)
		{
			//获取连接对象
			string toConnectionId = _cidsService.GetConnectionId(Id);
			var Client = Clients.Client(toConnectionId);
			//查询当前好友列表
			var a = _friendService.ReaderFrientList(Id);
			_logger.LogInformation("aaaaa"+a.ToString());
			await Client.SendAsync("ReceiveFriendList", a);
		}

		/// <summary>
		/// 提取当前用户Id
		/// </summary>
		/// <param name="tonken"></param>
		/// <returns></returns>
		public string GetUserId(string tonken)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var claims = tokenHandler.ReadJwtToken(tonken).Claims;
			var id = claims.Where(c => c.Type == "Id").FirstOrDefault().Value;
			
			return id;
		}
	}
}
