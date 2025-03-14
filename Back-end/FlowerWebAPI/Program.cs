using Autofac;
using Autofac.Extensions.DependencyInjection;
using FlowerCommon;
using FlowerModels;
using FlowerService.Config;
using FlowerService.Flowers;
using FlowerService.Jwt;
using FlowerService.Orders;
using FlowerService.User;
using FlowerWebAPI.Hubs;
using FlowerWebAPI.WebSocketServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();
//原来的^^^
//现在多添加了个JSON的序列化工具
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
	//处理循环引用对象，忽略循环引用，避免无限递归序列化。设置为Ignore为忽略循环引用
	options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
	//设置格式 将系统的事件信息统一为以下格式
	options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//添加-------------------跨域策略
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});

//SignaIR中间件 --------------------（双通行）
builder.Services.AddSignalR();

builder.Services.AddHttpContextAccessor();

//注册日志服务------------Log4Net
builder.Logging.AddLog4Net("Config/log4net.Config");

//注册--------------AutoMapper实体类之间的转换
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//注册--------------JWT  生成服务
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

//注册--------------Service层
//builder.Services.AddTransient<IFlowersService,FlowersService>();
//builder.Services.AddTransient<IUserService,UserService>();
//builder.Services.AddTransient<IOrderService,OrderService>();
//builder.Services.AddTransient<ICustomJWTService, CustomJWTService>();

//Autofac的IOC , 替换掉上面的直接注册对应关系
builder.Host
	.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureContainer<ContainerBuilder>(builder =>
	{
		builder.RegisterModule(new AutofacModuleRegister());
	});

#region JWT校验
//第二步，增加鉴权逻辑
JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("JWTTokenOptions",tokenOptions);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => //这里配置鉴权逻辑
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			//JWT的一些默认属性，就是给鉴权时就可以筛选了
			ValidateIssuer = true,                //是否验证Issuer
			ValidateAudience = true,				//是否验证Audience
			ValidateLifetime = true,				//是否验证失效时间
			ValidateIssuerSigningKey = true,		//是否验证SecurityKey
			ValidAudience = tokenOptions.Audience,	//
			ValidIssuer = tokenOptions.Issuer,	//Issuer，这两项和前面签发jwt的设置一致
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
		};
	});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

#region 鉴权授权-----挂载
app.UseAuthentication();
app.UseAuthorization();
#endregion

//#region 启用WebSocket---------中间件
////app.UseWebSockets();
////app.UseMiddleware<WebsocketHandler>();//将自定义中间件WebsocketHandler添加到应用程序管道中。这个中间件将处理WebSocket请求并处理来自客户端的消息。
//#endregion

//使用SignaIR中间件
app.MapHub<ChatHub>("/chathubs");

app.MapControllers();

//使用----------------跨域
app.UseCors("CorsPolicy");

app.Run();
