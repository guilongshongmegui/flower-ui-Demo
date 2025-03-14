using AutoMapper;
using FlowerCommon;
using FlowerModels.Entitys;
using FlowerService.Flowers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.Flowers
{
	public class FlowersService : IFlowersService
	{
		private readonly IMapper _mapper;
		public FlowersService(IMapper mapper)
		{
			this._mapper = mapper;//将AutoMapper服务通过构造函数注入进来
		}

		/// <summary>
		/// 查询鲜花，返回全部属性
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public List<FlowerRes> GetFlowers(FlowerReq req)
		{
			//通过SqlSugar的对象去查询数据
			//WhereIF中当id或者type都不为0时，执行查询
			var res = DBContext.db.Queryable<Flower>().WhereIF(req.Id>0,p => p.Id == req.Id).WhereIF(req.Type>0,p => p.Type == req.Type).ToList();
			
			//将查到的数据通过AutoMapper转换为传输对象FlowerRes
			return _mapper.Map<List<FlowerRes>>(res); 
		}


		/// <summary>
		/// 返回部分属性
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public List<FlowerTypeRes> GetFlowersType(FlowerReq req)
		{
			//通过SqlSugar的对象去查询数据
			//WhereIF中当id或者type都不为0时，执行查询
			var res = DBContext.db.Queryable<Flower>().WhereIF(req.Id > 0, p => p.Id == req.Id).WhereIF(req.Type > 0, p => p.Type == req.Type).ToList();

			//将查到的数据通过AutoMapper转换为传输对象FlowerRes
			return _mapper.Map<List<FlowerTypeRes>>(res);
		}


		public List<DomeTable> GetDome()
		{
			try
			{
				List<DomeTable> aa = DBContext.db.Queryable<DomeTable>().ToList();

				return aa;
			}
			catch (Exception)
			{
				return new List<DomeTable>();
			}

			
		}
	}
}
