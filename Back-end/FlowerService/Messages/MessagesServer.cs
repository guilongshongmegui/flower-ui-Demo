using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FlowerService.Messages
{
	public class MessagesServer:IMessagesServer
	{
		public void AddConnection(int id, WebSocket webSocket)
		{

		}

		public void RemoveConnection(int id)
		{

		}

		/*public WebSocket GetWebSocket(int id)
		{
			return new WebSocket;
		}*/
	}
}
