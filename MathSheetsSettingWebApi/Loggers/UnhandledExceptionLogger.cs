using MyMathSheets.CommonLib.Logging;
using System.Web.Http.ExceptionHandling;

namespace MyMathSheets.WebApi.Loggers
{
	public class UnhandledExceptionLogger : ExceptionLogger
	{
		public override void Log(ExceptionLoggerContext context)
		{
			var log = context.Exception.ToString();

			LogUtil.LogError(log);
		}
	}
}