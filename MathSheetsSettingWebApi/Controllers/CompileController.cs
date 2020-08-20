using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.CommonLib.Main.FromProcess.Support;
using MyMathSheets.WebApi.Filters;
using MyMathSheets.WebApi.Main.FromProcess;
using MyMathSheets.WebApi.Models.Request;
using MyMathSheets.WebApi.Models.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.ServiceModel.Activation;
using System.Web.Http;

namespace MyMathSheets.WebApi.Controllers
{
	/// <summary>
	/// API控制類
	/// </summary>
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class CompileController : ApiController
	{
		/// <summary>
		/// API處理類
		/// </summary>
		[Import("Api", typeof(IMainProcess))]
		private ApiProcess Process
		{
			get;
			set;
		}

		/// <summary>
		/// <see cref="CompileController"/>構造	構築依賴組合並導入<see cref="ApiProcess"/>API處理類
		/// </summary>
		public CompileController()
		{
			// 獲取HTML支援類Composer
			Composer composer = ComposerFactory.GetComporser(this.GetType().Assembly);
			composer.Compose(this);
		}

		/// <summary>
		/// 題型作成API訪問接口
		/// </summary>
		/// <param name="list">輸入參數(題型設定參數集合)</param>
		/// <returns>接口應答結果</returns>
		[HttpPost]
		[WaitResponse]
		public IHttpActionResult Do(List<TopicManagementReq> list)
		{
			LogUtil.LogDebug(JsonConvert.SerializeObject(list));
			// 題型參數設置
			list.ForEach(d => Process.TopicManagementList.Add(new TopicManagement() { TopicIdentifier = d.Id, Number = d.Number }));
			// 題型作成
			FileInfo exerciseFile = Process.Compile();
			// 應答作成
			var result = new TopicRes()
			{
				// 頁面地址返回
				Url = ConfigurationUtil.GetIISUrl() + exerciseFile.Name
			};

			return Json(result);
		}

		/// <summary>
		/// 默認測試頁
		/// </summary>
		/// <returns>應答結果</returns>
		[HttpGet]
		public string Index()
		{
			return "Request OK!";
		}
	}
}