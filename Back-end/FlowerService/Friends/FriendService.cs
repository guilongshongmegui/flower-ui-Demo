using AutoMapper;
using FlowerCommon;
using FlowerModels.Entitys;
using FlowerService.Friends.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FlowerService.Friends
{
	public class FriendService : IFriendService
	{
		private readonly IMapper _mapper;
		public FriendService(IMapper mapper)
		{
			_mapper = mapper;	
		}

		/// <summary>
		/// 查询指定Id的用户
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public FriendRes GetFriendRes(int Id)
		{
			var res = DBContext.db.Queryable<Users>().First(p => p.Id == Id);
			if (res != null)
			{
				return _mapper.Map<FriendRes>(res);
			}
			return new FriendRes();
		}

		/// <summary>
		/// 添加用户之间的关系
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="friendId"></param>
		/// <returns></returns>
		public bool RequestAdd(long userId,long friendId)
		{
			FriendList friendList = new FriendList();
			friendList = DBContext.db.Queryable<FriendList>().First(p => p.UserId == userId && p.FriendId == friendId);
			if (friendList != null)
			{
				return true;
			}
			else
			{
				try
				{
					FriendList friendlists = new FriendList()
					{
						UserId = userId,
						FriendId = friendId
					};
					bool a = DBContext.db.Insertable(friendlists).ExecuteCommand() > 0;//执行插入
					return true;
				}
				catch (Exception)
				{

					return false;
				}
			}
		}
	

		/// <summary>
		/// 查询三组数据
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public FriendsRes ReaderFrientList(string Id)
		{
			//执行查询
			string hbString = @"proc_GetHBUserId_Info";
			string tbaString = @"proc_GetTABUserId_Info";
			string pdString = @"proc_GetPDUserId_Info";
			List<userFriendsRes> haveBecome = GetUserFriends(hbString,Id);
			List<userFriendsRes> toBeAgreed = GetUserFriends(tbaString, Id);
			List<userFriendsRes> pyydetty = GetUserFriends(pdString, Id);

			//var a = result.Rows[0]["id"].ToString();
			FriendsRes fedRes = new()
			{
				HaveBecome = haveBecome,
				ToBeAgreed = toBeAgreed,
				Pyydetty = pyydetty
			};

			return fedRes;
		}
	

		//执行存储过程
		public static List<userFriendsRes> GetUserFriends(string SqlString,string Id)
		{
			var result = DBContext.db.Ado.UseStoredProcedure().GetDataTable(SqlString, new { num = Id });
			List<userFriendsRes> userFriends = new();
			if (result != null)
			{
				for (var i = 0; i < result.Rows.Count; i++)
				{
					userFriendsRes friend = new()
					{
						Id = result.Rows[i]["id"].ToString(),
						Name = result.Rows[i]["NickName"].ToString()
					};
					userFriends.Add(friend);
				}
			}
			return userFriends;
		}


	}
}
