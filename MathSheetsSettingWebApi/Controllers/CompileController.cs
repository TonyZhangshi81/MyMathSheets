using MyMathSheets.CommonLib.Logging;
using MyMathSheets.WebApi.Models.Request;
using MyMathSheets.WebApi.Models.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace MyMathSheets.WebApi.Controllers
{
	/// <summary>
	///
	/// </summary>
	public class CompileController : ApiController
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		[HttpPost]
		public IHttpActionResult Do(List<TopicManagement> item)
		{
			LogUtil.LogDebug(JsonConvert.SerializeObject(item));

			var result = new TopicRes()
			{
				Result = "http://localhost:8080/AppData/Work/Page/20200814111036118.html",
				StatusCode = 200
			};

			return Json(result);
		}

		/// <summary>
		/// 默認起始頁
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public string Index()
		{
			return "Request OK!";
		}
	}
}