using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.TheFormulaShows.Properties;

namespace MyMathSheets.TheFormulaShows
{
	/// <summary>
	/// 
	/// </summary>
	public class MakeHtml<T> where T : ParameterBase
	{
		private static Log log = Log.LogReady(typeof(T));

		/// <summary>
		/// 
		/// </summary>
		private OperationHelper _operationHelper;

		/// <summary>
		/// 
		/// </summary>
		protected OperationHelper OptHelper => _operationHelper ?? (_operationHelper = new OperationHelper());

		/// <summary>
		/// 
		/// </summary>
		private HtmlSupprtHelper _supprtHelper;

		/// <summary>
		/// 
		/// </summary>
		protected HtmlSupprtHelper SupprtHelper => _supprtHelper ?? (_supprtHelper = new HtmlSupprtHelper());


		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameter"></param>
		public T Structure(LayoutSetting.Preview preview, string identifier)
		{
			return OptHelper.Structure(preview, identifier) as T;
		}

		/// <summary>
		/// HTML模板替換內容作成
		/// </summary>
		/// <returns>模板替換內容</returns>
		public string GetHtmlStatement(LayoutSetting.Preview preview, T formulas)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0002F, preview.ToString()));

			// 指定题型大分类获得相应的题型HTML处理对象（实例）
			IHtmlSupport support = SupprtHelper.CreateHtmlSupportInstance(preview);

			log.Debug(MessageUtil.GetException(() => MsgResources.I0003F, preview.ToString()));

			var html = support.Make(formulas);

			log.Debug(MessageUtil.GetException(() => MsgResources.I0004F));

			return html;
		}
	}
}
