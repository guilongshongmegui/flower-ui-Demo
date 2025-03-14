using FlowerModels.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.Friends.Dto
{
	public class FriendsRes
	{
		//已有好友
		public List<userFriendsRes> HaveBecome {  get; set; }
		//等待同意
		public List<userFriendsRes> ToBeAgreed { get; set; }
		//待回复
		public List<userFriendsRes> Pyydetty { get; set; }
	}
}
