using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiFlower.Service.User.Dto;

namespace FlowerService.User
{
	public interface IUserService
	{
		UserRes RegisterUser(RegisterReq req,ref string msg); //注册接口
		UserRes GetUsers(UserReq req);  //登录接口

		string GetNowName(string NowId); //查询当前Id
	}
}
