using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowerWebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		//获取图片列表的方法
		[HttpGet]
		public List<ImageModel> GetImages()
		{
			List<ImageModel> list = new List<ImageModel>()
			{
				new ImageModel { ImageUrl="http://119.91.231.224/images/flower/banners/21_birthday_banner_pc.jpg", CourseUrl="http://localhost:80/"},
				new ImageModel { ImageUrl="http://119.91.231.224//images/flower/banners/21_brand_banner_pc.jpg", CourseUrl="http://localhost:80/"},
				new ImageModel { ImageUrl="http://119.91.231.224//images/flower/banners/21_syz_banner_pc.jpg", CourseUrl="http://localhost:80/"}
			};

			return list;
		}
	}
}
