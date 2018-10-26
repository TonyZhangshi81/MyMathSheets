using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
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
		private HtmlSupprtHelper _supprtHelper;

		/// <summary>
		/// 
		/// </summary>
		protected HtmlSupprtHelper SupprtHelper => _supprtHelper ?? (_supprtHelper = new HtmlSupprtHelper());


		/// <summary>
		/// HTML模板替換內容作成
		/// </summary>
		/// <param name="formulas"></param>
		/// <param name="preview"></param>
		/// <param name="supportType"></param>
		/// <returns>模板替換內容</returns>
		public string GetHtmlStatement(LayoutSetting.Preview preview, T formulas, out Type supportType)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0012L, preview.ToString()));

			// 指定题型大分类获得相应的题型HTML处理对象（实例）
			IHtmlSupport support = SupprtHelper.CreateHtmlSupportInstance(preview);
			supportType = support.GetType();

			log.Debug(MessageUtil.GetException(() => MsgResources.I0013L, preview.ToString()));

			var html = support.Make(formulas);
			return html;
		}
	}
}
