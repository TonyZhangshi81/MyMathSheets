using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.WebApi.Loggers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace MyMathSheets.WebApi
{
	/// <summary>
	///
	/// </summary>
	public static class WebApiConfig
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="config"></param>
		public static void Register(HttpConfiguration config)
		{
			var allowedMethods = ConfigurationUtil.GetKeyValue("cors:allowedMethods");
			var allowedOrigin = ConfigurationUtil.GetKeyValue("cors:allowedOrigin");
			var allowedHeaders = ConfigurationUtil.GetKeyValue("cors:allowedHeaders");
			// 跨域設置
			var geduCors = new EnableCorsAttribute(allowedOrigin, allowedHeaders, allowedMethods)
			{
				SupportsCredentials = true
			};
			config.EnableCors(geduCors);

			// 路由設置
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				// 默認起始頁
				defaults: new { id = RouteParameter.Optional }
			);

			config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());
		}
	}
}