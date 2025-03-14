using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerModels
{
	public class ApiResult
	{
		/// <summary>
		/// 这是API的返回实体
		/// </summary>
		public bool IsSuccess { get; set; } //是否成功
		public object Result { get; set; }  //返回的对象
		public string Msg {  get; set; }   //返回的信息，或者报错信息
	}
}
