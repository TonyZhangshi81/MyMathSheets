using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
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
		private static Log log = Log.LogReady(typeof(HtmlSupportBase));

		/// <summary>
		/// HTML模板信息作成并返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板信息</returns>
		public virtual Dictionary<string, string> Make(ParameterBase parameter)
		{
			log.Debug(MessageUtil.GetException(() => MsgResources.I0009L));

			// 題型識別子取得
			string identifier = parameter.Identifier.Split(':')[0];
			Dictionary<string, string> htmlMaps = new Dictionary<string, string>()
			{
				// 題型HTML信息作成并對指定的HTML模板標識進行替換
				{ string.Format("<!--{0}-->", identifier.ToUpper()), MakeHtmlStatement(parameter) }
			};
			// JS模板內容替換
			MarkJavaScriptReplaceContent(htmlMaps, identifier);

			log.Debug(MessageUtil.GetException(() => MsgResources.I0010L));

			return htmlMaps;
		}

		/// <summary>
		/// JS模板內容替換
		/// </summary>
		/// <param name="htmlMaps">替換標籤以及內容</param>
		/// <param name="identifier">題型識別子</param>
		protected virtual void MarkJavaScriptReplaceContent(Dictionary<string, string> htmlMaps, string identifier)
		{
			var attrs = GetType().GetCustomAttributes(typeof(SubstituteAttribute), false).Cast<SubstituteAttribute>();
			if (attrs == null)
			{
				throw new NotImplementedException(MessageUtil.GetException(() => MsgResources.E0022L, identifier));
			}
			attrs.ToList().ForEach(d =>
			{
				htmlMaps.Add(d.Source, d.Target);
			});
		}

		/// <summary>
		/// HTML模板信息作成并返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板信息</returns>
		protected abstract string MakeHtmlStatement(ParameterBase parameter);
	}
}
