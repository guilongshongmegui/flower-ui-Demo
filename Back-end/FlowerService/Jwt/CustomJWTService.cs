using FlowerModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiFlower.Service.User.Dto;

namespace FlowerService.Jwt
{
	public class CustomJWTService : ICustomJWTService
	{
		private readonly JWTTokenOptions _JWTTokenOptions;
		public CustomJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
		{
			_JWTTokenOptions = jwtTokenOptions.CurrentValue;
		}


		/// <summary>
		/// 获取token
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public string GetToken(UserRes user)
		{
			#region 有效载荷，大家可以自己写，爱写多少写多少；尽量避免敏感信息
			var claims = new[]
			{
				new Claim("Id",user.Id.ToString()),
				new Claim("NickName",user.NickName),
				new Claim("UserName",user.UserName),
				new Claim("UserType",user.UserType.ToString()),
			};

			//需要加密：需要加密key:
			//Nuget引入：Microsoft.IdentityModel.Tokens
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

			SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			//Nuget引入：System.IdentityModel.Tokens.Jwt
			JwtSecurityToken token = new JwtSecurityToken(
			 issuer: _JWTTokenOptions.Issuer,
			 audience: _JWTTokenOptions.Audience,
			 claims: claims,
			 expires: DateTime.Now.AddDays(1),//1天有效期
			 signingCredentials: creds);

			string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
			return returnToken;
			#endregion
		}
	}
}
