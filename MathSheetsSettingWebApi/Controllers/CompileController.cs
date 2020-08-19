using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.CommonLib.Main.FromProcess.Support;
using MyMathSheets.WebApi.Main.FromProcess;
using MyMathSheets.WebApi.Models.Request;
using MyMathSheets.WebApi.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Web.Http;

namespace MyMathSheets.WebApi.Controllers
{
	/// <summary>
	///
	/// </summary>
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
		/// 
		/// </summary>
		public CompileController()
		{
			// 獲取HTML支援類Composer
			Composer composer = ComposerFactory.GetComporser(this.GetType().Assembly);
			composer.Compose(this);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		[HttpPost]
		public IHttpActionResult Do(List<TopicManagementReq> list)
		{
			LogUtil.LogDebug(JsonConvert.SerializeObject(list));

			list.ForEach(d => {
				Process.TopicManagementList.Add(new TopicManagement() { TopicIdentifier = d.Id, Number = d.Number });
			});

			FileInfo exerciseFile = Process.Compile();

			var result = new TopicRes()
			{
				Url = ConfigurationUtil.GetIISUrl() + exerciseFile.Name,
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