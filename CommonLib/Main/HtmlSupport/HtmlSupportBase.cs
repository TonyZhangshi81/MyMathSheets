using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// 各題型HTML作成用的抽象類
	/// </summary>
	public abstract class HtmlSupportBase<T> : ObjectBase, IHtmlSupport, IHtmlSupport<T>
		where T : TopicParameterBase
	{
		/// <summary>
		/// HTML模板屬性信息採集並返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板屬性信息集合</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameter"/>為NULL的情況</exception>
		public virtual ConcurrentDictionary<SubstituteType, string> MakeHtmlMaps(TopicParameterBase parameter)
		{
			Guard.ArgumentNotNull(parameter, "parameter");
			if (!typeof(T).IsAssignableFrom(parameter.GetType()))
			{
				throw CreateInvalidParameterException(typeof(T));
			}

			var p = (T)parameter;
			var htmlMaps = new ConcurrentDictionary<SubstituteType, string>();
			try
			{
				// 題型HTML信息作成并對指定的HTML模板標識進行替換
				htmlMaps.GetOrAdd(SubstituteType.Content, (s) =>
				{
					var content = MakeHtmlContent(p);
					LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0043L, SubstituteType.Content));
					return content;
				});

				// JS模板標籤內容採集
				MarkJavaScriptReplaceContent(htmlMaps);
			}
			catch
			{
				throw;
			}
			finally
			{
				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0010L));
			}
			return htmlMaps;
		}

		/// <summary>
		/// JS模板標籤內容採集
		/// </summary>
		/// <param name="htmlMaps">HTML模板屬性信息集合</param>
		protected virtual void MarkJavaScriptReplaceContent(ConcurrentDictionary<SubstituteType, string> htmlMaps)
		{
			var attrs = GetType().GetCustomAttributes(typeof(SubstituteAttribute), false).Cast<SubstituteAttribute>();
			if (attrs != null)
			{
				attrs.ToList().ForEach(d =>
				{
					htmlMaps.GetOrAdd(d.Source, (s) =>
					{
						LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0043L, d.Source));
						return d.Target;
					});
				});
			}
		}

		/// <summary>
		/// 參數模板類型不正確
		/// </summary>
		/// <param name="expectedType">參數模板類型</param>
		/// <returns>異常對象</returns>
		protected Exception CreateInvalidParameterException(Type expectedType)
		{
			Guard.ArgumentNotNull(expectedType, "expectedType");
			throw new ArgumentException(MessageUtil.GetMessage(() => MsgResources.E0045L, expectedType.FullName));
		}

		/// <summary>
		/// HTML模板信息作成并返回
		/// </summary>
		/// <param name="parameter">計算式參數</param>
		/// <returns>HTML模板信息</returns>
		public abstract string MakeHtmlContent(T parameter);
	}
}