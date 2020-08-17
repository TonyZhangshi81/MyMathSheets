using MyMathSheets.CommonLib.Logging;
using System;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

namespace MyMathSheets.WebApi
{
	/// <summary>
	///
	/// </summary>
	public class WebApiApplication : System.Web.HttpApplication
	{
		/// <summary>
		///
		/// </summary>
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Application_Error(object sender, EventArgs e)
		{
			var message = new StringBuilder();
			if (Server != null)
			{
				Exception ex;

				// ó·äOèÓïÒÇéÊìæÇ∑ÇÈ
				for (ex = Server.GetLastError(); ex != null; ex = ex.InnerException)
				{
					message.AppendFormat("{0}: {1}{2}", ex.GetType().FullName, ex.Message, ex.StackTrace);
				}

				LogUtil.LogError(message.ToString());

				Server.ClearError();
			}
		}
	}
}