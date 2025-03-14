using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiFlower.Service.User.Dto;

namespace FlowerService.Jwt
{
	public interface ICustomJWTService
	{
		//获取Token
		string GetToken(UserRes user);
	}
}
