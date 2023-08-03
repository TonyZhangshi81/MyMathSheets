using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.Arithmetic;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類
	/// </summary>
	[Export(typeof(IMakeHtml)), PartCreationPolicy(CreationPolicy.Shared)]
	public class MakeHtmlFactory : IMakeHtml
	{
		/// <summary>
		///
		/// </summary>
		private HtmlSupprtHelper _supprtHelper;

		/// <summary>
		/// 工廠注入點
		/// </summary>
		[Import(typeof(IHtmlSupportFactory), RequiredCreationPolicy = CreationPolicy.Shared)]
		private IHtmlSupportFactory HtmlSupportFactory
		{
			get;
			set;
		}

		/// <summary>
		/// 用於HTML支援類實例取得的HEPLER類
		/// </summary>
		protected HtmlSupprtHelper SupprtHelper
		{
			get
			{
				if (_supprtHelper == null)
				{
					_supprtHelper = new HtmlSupprtHelper(HtmlSupportFactory);
				}

				return _supprtHelper;
			}
		}

		/// <summary>
		/// HTML模板替換內容作成
		/// </summary>
		/// <param name="topicIdentifier">題型類型</param>
		/// <param name="parameter">題型參數</param>
		/// <returns>HTML模板屬性信息集合</returns>
		public ConcurrentDictionary<SubstituteType, string> GetHtmlReplaceTagDict<T>(string topicIdentifier, T parameter)
			where T : TopicParameterBase
		{
			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0012L, topicIdentifier));

			// HTML構築實例
			IHtmlSupport support = SupprtHelper.CreateHtmlSupportInstance(topicIdentifier);

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0013L, topicIdentifier));

			// 根據參數構建HTML屬性信息
			return support.MakeHtmlMaps(parameter);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="topicIdentifier"></param>
		public void ReleaseExportsHtmlSupport(string topicIdentifier)
		{
			SupprtHelper.ReleaseExportsHtmlSupport(topicIdentifier);
		}
	}
}