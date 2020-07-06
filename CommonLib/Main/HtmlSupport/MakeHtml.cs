using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類
	/// </summary>
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export(typeof(IMakeHtml))]
	public class MakeHtml : IMakeHtml
	{
		/// <summary>
		///
		/// </summary>
		private HtmlSupprtHelper _supprtHelper;

		/// <summary>
		/// 用於HTML支援類實例取得的HEPLER類
		/// </summary>
		protected HtmlSupprtHelper SupprtHelper
		{
			get
			{
				if (_supprtHelper == null)
				{
					_supprtHelper = new HtmlSupprtHelper();
				}

				return _supprtHelper;
			}
		}

		/// <summary>
		/// HTML模板替換內容作成
		/// </summary>
		/// <param name="preview">題型類型</param>
		/// <param name="formulas">題型參數對象</param>
		/// <returns>模板替換內容</returns>
		public Dictionary<SubstituteType, string> GetHtmlStatement<T>(string preview, T formulas) where T : ParameterBase
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0012L, preview));

			// 指定题型大分类获得相应的题型HTML处理对象（实例）
			IHtmlSupport support = SupprtHelper.CreateHtmlSupportInstance(preview);

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0013L, preview));

			return support.Make(formulas);
		}
	}
}