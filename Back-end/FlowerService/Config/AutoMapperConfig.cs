using AutoMapper;
using FlowerModels.Entitys;
using FlowerService.Flowers.Dto;
using FlowerService.Friends.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoxiFlower.Service.User.Dto;

namespace FlowerService.Config
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig() 
		{
			//创建一个映射关系，从A到(给)B
			CreateMap<Flower, FlowerRes>();
			CreateMap<Users,UserRes>();
			CreateMap<RegisterReq,Users>();

			CreateMap<Flower,FlowerTypeRes>();

			CreateMap<Users,FriendRes>();


		}
	}
}
