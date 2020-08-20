using MyMathSheets.CommonLib.Plugin;
using MyMathSheets.WebApi.Filters;
using System.Web.Http;
using System.Web.Mvc;

namespace MyMathSheets.WebApi
{
	/// <summary>
	///
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		///
		/// </summary>
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			// ˜H—Rİ’è
			GlobalConfiguration.Configure(WebApiConfig.Register);
			// ˆÙíâ¿‘IŠí
			GlobalConfiguration.Configuration.Filters.Add(new WebApiExceptionFilterAttribute());

			// Še‘èŒ^Assamly“±“ü
			PluginHelper.GetManager().Initialize();
		}
	}
}