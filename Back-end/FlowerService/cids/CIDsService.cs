using FlowerCommon;
using FlowerModels.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.cids
{
	public class CIDsService:ICIDsService
	{
		public CIDsService() {
		
		}
		/// <summary>
		/// 创建关系 & 添加
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="connectionId"></param>
		/// <returns></returns>
		public bool SaveConntionId(string userId,string connectionId)
		{
			var cids = DBContext.db.Queryable<CIDs>().First(p => p.UserId == userId);
			try
			{
				
				//判断有这个关系就修改
				if (cids != null)
				{
					try
					{
						cids.ConnectionId = connectionId;
						//更新数据，更新ConnectionId列，条件为it.UserId == userId
						bool a = DBContext.db.Updateable(cids).UpdateColumns(it => it.ConnectionId).Where(it => it.UserId == userId).ExecuteCommand() > 0;
						return a;
					}
					catch (Exception)
					{
						return false;
					}
				}
				else //没有则新增
				{
					CIDs cidss = new CIDs
					{
						UserId = userId,
						ConnectionId = connectionId
					};
					return DBContext.db.Insertable(cidss).ExecuteCommand() > 0;
				}
			}
			catch (Exception)
			{
				//记录日志
				throw;
			}
		}

		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public string GetConnectionId(string userId)
		{
			//查询是否存在关系
			var cids = DBContext.db.Queryable<CIDs>().First(p => p.UserId == userId);
			if (cids != null)
			{
				return cids.ConnectionId;
			}
			else
			{
				return "false";
			}
		}

		/// <summary>
		/// 移除连接字符串
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public int RemoveConnectionId(string userId)
		{
			try
			{
				var cids = DBContext.db.Queryable<CIDs>().First(p => p.UserId == userId);
				if (cids != null)
				{
					cids.ConnectionId = "";
					bool a = DBContext.db.Updateable(cids).UpdateColumns(it => it.ConnectionId).Where(it => it.UserId == userId).ExecuteCommand() > 0;
					return a ? 1 : 0;
				}
				else
				{
					return 2;
				}
			}
			catch (Exception)
			{
				return -1;
				throw;
			}
		}

	}
}
