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
			// �H�R�ݒ�
			GlobalConfiguration.Configure(WebApiConfig.Register);
			// �ُ�⿑I��
			GlobalConfiguration.Configuration.Filters.Add(new WebApiExceptionFilterAttribute());

			// �e��^Assamly����
			PluginHelper.GetManager().Initialize();
		}
	}
}