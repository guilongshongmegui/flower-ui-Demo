using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.cids
{
	public interface ICIDsService
	{
		bool SaveConntionId(string userId, string connectionId);
		string GetConnectionId(string userId);
		int RemoveConnectionId(string userId);
	}
}
