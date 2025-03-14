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
//ԭ����^^^
//���ڶ�����˸�JSON�����л�����
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
	//����ѭ�����ö��󣬺���ѭ�����ã��������޵ݹ����л�������ΪIgnoreΪ����ѭ������
	options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
	//���ø�ʽ ��ϵͳ���¼���ϢͳһΪ���¸�ʽ
	options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//���-------------------�������
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});

//SignaIR�м�� --------------------��˫ͨ�У�
builder.Services.AddSignalR();

builder.Services.AddHttpContextAccessor();

//ע����־����------------Log4Net
builder.Logging.AddLog4Net("Config/log4net.Config");

//ע��--------------AutoMapperʵ����֮���ת��
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//ע��--------------JWT  ���ɷ���
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

//ע��--------------Service��
//builder.Services.AddTransient<IFlowersService,FlowersService>();
//builder.Services.AddTransient<IUserService,UserService>();
//builder.Services.AddTransient<IOrderService,OrderService>();
//builder.Services.AddTransient<ICustomJWTService, CustomJWTService>();

//Autofac��IOC , �滻�������ֱ��ע���Ӧ��ϵ
builder.Host
	.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureContainer<ContainerBuilder>(builder =>
	{
		builder.RegisterModule(new AutofacModuleRegister());
	});

#region JWTУ��
//�ڶ��������Ӽ�Ȩ�߼�
JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("JWTTokenOptions",tokenOptions);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => //�������ü�Ȩ�߼�
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			//JWT��һЩĬ�����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
			ValidateIssuer = true,                //�Ƿ���֤Issuer
			ValidateAudience = true,				//�Ƿ���֤Audience
			ValidateLifetime = true,				//�Ƿ���֤ʧЧʱ��
			ValidateIssuerSigningKey = true,		//�Ƿ���֤SecurityKey
			ValidAudience = tokenOptions.Audience,	//
			ValidIssuer = tokenOptions.Issuer,	//Issuer���������ǰ��ǩ��jwt������һ��
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

#region ��Ȩ��Ȩ-----����
app.UseAuthentication();
app.UseAuthorization();
#endregion

//#region ����WebSocket---------�м��
////app.UseWebSockets();
////app.UseMiddleware<WebsocketHandler>();//���Զ����м��WebsocketHandler��ӵ�Ӧ�ó���ܵ��С�����м��������WebSocket���󲢴������Կͻ��˵���Ϣ��
//#endregion

//ʹ��SignaIR�м��
app.MapHub<ChatHub>("/chathubs");

app.MapControllers();

//ʹ��----------------����
app.UseCors("CorsPolicy");

app.Run();
