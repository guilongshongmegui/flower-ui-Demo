using AutoMapper;
using FlowerCommon;
using FlowerModels.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiFlower.Model.Enum;
using ZhaoxiFlower.Service.User.Dto;

namespace FlowerService.User
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		public UserService(IMapper mapper)
		{
			this._mapper = mapper;
		}

		/// <summary>
		/// 登录接口实现
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public UserRes GetUsers(UserReq req)
		{
			//查询登录名和密码
			var user = DBContext.db.Queryable<Users>().First(p => p.UserName == req.UserName && p.Password == req.Password);
			if (user != null) //查询到了就返回一个Res的实体对象
			{
				return _mapper.Map<UserRes>(user);
			}
			return new UserRes(); //否则返回空的对象
		}

		/// <summary>
		/// 注册接口实现
		/// </summary>
		/// <param name="req"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public UserRes RegisterUser(RegisterReq req, ref string msg)
		{
			//查询用户名是否已存在
			var user = DBContext.db.Queryable<Users>().First(p => p.UserName == req.UserName);
			if (user != null)
			{
				msg = "账号以存在";
				return _mapper.Map<UserRes>(user);
			}
			else
			{
				try
				{
					Users users = _mapper.Map<Users>(req);
					users.CreateTime = DateTime.Now;
					users.UserType = (int)EnumUserType.普通用户;
					//将上面三个属性存入到数据库中，返回受影响的行数，大于0就创建成功
					bool res = DBContext.db.Insertable(users).ExecuteCommand() > 0;
					if (res)
					{
						//注册成功后，查询一下，转换为前端需要用到的数据
						user = DBContext.db.Queryable<Users>().First(p => p.UserName == req.UserName && p.Password == req.Password);
						return _mapper.Map<UserRes>(user);
					}
				}
				catch (Exception ex)
				{
					msg = ex.Message;
				}
			}
			return new UserRes();
		}

		//查询NickName
		public string GetNowName(string NowId)
		{
			var NowObj = DBContext.db.Queryable<Users>().First(p => p.Id == long.Parse(NowId));
			return NowObj.NickName;
		}
	}
}
