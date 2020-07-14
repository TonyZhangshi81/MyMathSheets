using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// 各題型HTML作成用的抽象類
	/// </summary>
	public abstract class HtmlSupportBase : IHtmlSupport
	{
		/// <summary>
		/// HTML模板信息作成并返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板信息</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameter"/>為NULL的情況</exception>
		public virtual Dictionary<SubstituteType, string> Make(ParameterBase parameter)
		{
			Guard.ArgumentNotNull(parameter, "parameter");

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0009L));

			Dictionary<SubstituteType, string> htmlMaps = new Dictionary<SubstituteType, string>()
			{
				// 題型HTML信息作成并對指定的HTML模板標識進行替換
				{ SubstituteType.Content, MakeHtmlStatement(parameter) }
			};

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0043L, SubstituteType.Content));

			// JS模板內容替換
			MarkJavaScriptReplaceContent(htmlMaps);

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0010L));

			return htmlMaps;
		}

		/// <summary>
		/// JS模板內容替換
		/// </summary>
		/// <param name="htmlMaps">替換標籤以及內容</param>
		protected virtual void MarkJavaScriptReplaceContent(Dictionary<SubstituteType, string> htmlMaps)
		{
			var attrs = GetType().GetCustomAttributes(typeof(SubstituteAttribute), false).Cast<SubstituteAttribute>();
			if (attrs != null)
			{
				attrs.ToList().ForEach(d =>
				{
					htmlMaps.Add(d.Source, d.Target);

					LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0043L, d.Source));
				});
			}
		}

		/// <summary>
		/// HTML模板信息作成并返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板信息</returns>
		protected abstract string MakeHtmlStatement(ParameterBase parameter);
	}
}