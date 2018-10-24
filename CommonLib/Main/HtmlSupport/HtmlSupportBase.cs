using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// 各題型HTML作成用的抽象類
	/// </summary>
	public abstract class HtmlSupportBase : IHtmlSupport
	{
		private static Log log = Log.LogReady(typeof(HtmlSupportBase));

		/// <summary>
		/// HTML模板信息作成并返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板信息</returns>
		public virtual string Make(ParameterBase parameter)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0009L));

			var html = MakeHtmlStatement(parameter);

			log.Debug(MessageUtil.GetException(() => MsgResources.I0010L));

			return html;
		}

		/// <summary>
		/// HTML模板信息作成并返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板信息</returns>
		protected abstract string MakeHtmlStatement(ParameterBase parameter);
	}
}
