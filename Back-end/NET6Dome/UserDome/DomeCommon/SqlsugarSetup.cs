using SqlSugar;

namespace UserDome.DomeCommon
{
	/// <summary>
	/// 静态SqlSugar扩展类
	/// </summary>
	public static class SqlsugarSetup
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="service">服务</param>
		/// <param name="configuration">配置文件</param>
		public static void AppSqlsugarSetup(this IServiceCollection service, IConfiguration configuration)
		{
			SqlSugarScope sqlSugar = new SqlSugarScope(
				new ConnectionConfig()
				{
					DbType = SqlSugar.DbType.SqlServer,//数据库类型
					ConnectionString = configuration["DbDateSource"],//配置文件中的数据库链接key值
					IsAutoCloseConnection = true,//是否自动关闭连接
				},
				db => {

					//单例参数配置，所有上下文生效
					db.Aop.OnLogExecuting = (sql, pars) =>
					{
						Console.WriteLine(sql);//输出sql
					};
					//技巧：拿到非ORM注入对象
					//services.GetService<注入对象>();
				}
			);
			service.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
		}
	}
}
