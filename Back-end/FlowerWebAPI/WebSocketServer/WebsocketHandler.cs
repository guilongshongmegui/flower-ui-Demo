using FlowerService.Messages.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using JsonSerializer = System.Text.Json.JsonSerializer; //重命名，解决和命名空间Newtonsoft.Json;的冲突

namespace FlowerWebAPI.WebSocketServer
{

	//WebSocket中心
	public class WebsocketHandler
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public WebsocketHandler(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_next = next;
			_logger = loggerFactory.CreateLogger<WebsocketHandler>();
		}

		public async Task Invoke(HttpContext context)
		{
			//是获取当前HTTP请求的路径。在这个例子中，我们检查当前请求是否是针对WebSocket的请求，因此我们检查路径是否为/ws。
			if (context.Request.Path == "/ws")
			{
				//是检查当前HTTP请求是否是WebSocket请求的方法。
				if (context.WebSockets.IsWebSocketRequest)
				{
					//接受WebSocket连接并返回一个WebSocket对象。如果连接成功，我们将使用该对象来发送和接收WebSocket消息。
					

					//_logger.LogInformation($"接收到websocket连接.");
					try
					{
						WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
						var serializedWebSocket = JsonConvert.SerializeObject(webSocket);
						WebSocket deserializedWebSocket = JsonConvert.DeserializeObject<WebSocket>(serializedWebSocket);
						//判断是否传入了Id这个值
						if (context.Request.Query.TryGetValue("Id", out var idValues))
{
							string id = idValues.FirstOrDefault();
						}
						
						//这个类用来存储从WebSocket连接接收到消息
						//在do-while中使用result来检查接收到的消息类型和连接状态
						WebSocketReceiveResult result;
						do
						{
							//创建了一个字节数组（缓冲区）大小为1024*1字节(1KB)
							var buffer = new byte[1024 * 1];

							//ReceiveAsync方法接收消息；并存储到缓冲区
							//ArraySegment<T>是部分数组，CancellationToken.None是 None(不) 取消标记
							//返回一个WebSocketReceiveResult对象，该对象包含有关接收到的消息的信息，例如消息类型、接收的字节数、关闭状态等。
							//在这个例子中，我们将返回的WebSocketReceiveResult对象存储在result变量中，以便我们可以检查接收到的消息的信息。
							result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

							//判断接收到的消息是文本类型的吗 & 连接关闭了吗
							if (result.MessageType == WebSocketMessageType.Text && !result.CloseStatus.HasValue)
							{

								//将接收的二进制转换为字符串，
								var msgString = Encoding.UTF8.GetString(buffer,0, result.Count);

								var messageObject = JsonSerializer.Deserialize<MessageReq>(msgString);
								_logger.LogInformation("kh:");

								/*if (messageObject != null)
								{
									await messageObject.WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message.Content)), WebSocketMessageType.Text, true, CancellationToken.None);
								}*/


								//将信息返回客户端
								//await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
								await deserializedWebSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
							}
						}
						while (!result.CloseStatus.HasValue);
					}
					catch (Exception)
					{

					}
				}
				else
				{
					context.Response.StatusCode = 404;//设置响应码为404
				}
			}
			else
			{
				await _next(context);     //不是WebSocket请求则交给下一个中间件来处理
			}
		}
	

	}
}
