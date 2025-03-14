using FlowerModels.Entitys;
using FlowerService.Friends.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.Friends
{
	public interface IFriendService
	{
		FriendRes GetFriendRes(int Id);
		bool RequestAdd(long userId, long friendId);
		FriendsRes ReaderFrientList(String Id);
	}
}
