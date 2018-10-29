using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類接口
	/// </summary>
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export(typeof(IMakeHtml))]
	public class MakeHtml : IMakeHtml
	{
		private static Log log = Log.LogReady(typeof(MakeHtml));

		/// <summary>
		/// 
		/// </summary>
		private HtmlSupprtHelper _supprtHelper;
		/// <summary>
		/// 用於HTML支援類實例取得的HEPLER類
		/// </summary>
		protected HtmlSupprtHelper SupprtHelper => _supprtHelper ?? (_supprtHelper = new HtmlSupprtHelper());

		/// <summary>
		/// HTML模板替換內容作成
		/// </summary>
		/// <param name="preview">題型類型</param>
		/// <param name="formulas">題型參數對象</param>
		/// <param name="supportType">HTML支援類對象類型</param>
		/// <returns>模板替換內容</returns>
		public string GetHtmlStatement<T>(LayoutSetting.Preview preview, T formulas, out Type supportType) where T : ParameterBase
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0012L, preview.ToString()));

			// 指定题型大分类获得相应的题型HTML处理对象（实例）
			IHtmlSupport support = SupprtHelper.CreateHtmlSupportInstance(preview);
			supportType = support.GetType();

			log.Debug(MessageUtil.GetException(() => MsgResources.I0013L, preview.ToString()));

			return support.Make(formulas);
		}
	}
}
