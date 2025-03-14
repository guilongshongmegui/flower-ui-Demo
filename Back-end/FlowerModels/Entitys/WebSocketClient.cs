using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FlowerModels.Entitys
{
	public class WebSocketClient
	{
		
		public string Uid { get; set; }

		public WebSocket websocket { get; set; }
	}
}
