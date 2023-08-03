using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.WebApi.Properties;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace MyMathSheets.WebApi.Filters
{
	/// <summary>
	/// WebApi異常篩選器
	/// </summary>
	public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		/// <summary>
		/// 異常處理
		/// </summary>
		/// <param name="actionExecutedContext"></param>
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			LogUtil.LogError(MessageUtil.GetMessage(() => MsgResources.E0001A), actionExecutedContext.Exception);

			if (actionExecutedContext.Exception is NotImplementedException)
			{
				actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
			}
			else if (actionExecutedContext.Exception is TimeoutException)
			{
				actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
			}
			else
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
				response.Content = new StringContent(actionExecutedContext.Exception.Message, Encoding.UTF8);
				actionExecutedContext.Response = response;
			}

			base.OnException(actionExecutedContext);
		}
	}
}