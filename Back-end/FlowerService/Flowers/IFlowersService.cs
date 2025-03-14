using FlowerModels.Entitys;
using FlowerService.Flowers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.Flowers
{
	public interface IFlowersService
	{
		List<FlowerRes> GetFlowers(FlowerReq req);

		List<FlowerTypeRes> GetFlowersType(FlowerReq req);

		List<DomeTable> GetDome();
	}
}
