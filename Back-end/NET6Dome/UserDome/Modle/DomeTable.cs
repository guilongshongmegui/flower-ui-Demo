using SqlSugar;

namespace UserDome.Modle
{
	[SugarTable("DomeTable")]
	public class DomeTable
	{
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public long Id { get; set; }

		[SugarColumn(IsNullable = true)]
		public int number { get; set; }
	}
}
