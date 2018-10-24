using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MyMathSheets.CommonLib.Main.HtmlSupport
{
	/// <summary>
	/// HTML支援類實例生產工廠
	/// </summary>
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export(typeof(IHtmlSupportFactory))]
	public class HtmlSupportFactory : IHtmlSupportFactory
	{
		private static Log log = Log.LogReady(typeof(HtmlSupportFactory));

		/// <summary>
		/// 以防止重複注入（減少損耗）
		/// </summary>
		private bool _composed = false;
		/// <summary>
		/// HTML支援類檢索用的composer
		/// </summary>
		private readonly Composer _composer;

		/// <summary>
		/// HTML支援類對象緩存區
		/// </summary>
		private static readonly ConcurrentDictionary<Composer, ConcurrentDictionary<LayoutSetting.Preview, IHtmlSupport>> HtmlSupportCache = new ConcurrentDictionary<Composer, ConcurrentDictionary<LayoutSetting.Preview, IHtmlSupport>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		[ImportingConstructor]
		public HtmlSupportFactory()
		{
			// 獲取HTML支援類Composer
			_composer = ComposerFactory.GetComporser(SystemModel.TheFormulaShows);
		}

		/// <summary>
		/// 在MEF容器中收集本類的屬性信息
		/// </summary>
		private void ComposeThis()
		{
			// 工廠實例后只需要收集一次
			if (this._composed)
			{
				return;
			}
			// 從MEF容器中注入本類的屬性信息（注入HTML支援類屬性）
			this._composer.Compose(this);
			// 以防止重複注入（減少損耗）
			this._composed = true;
		}

		/// <summary>
		/// HTML支援類屬性注入點
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public IEnumerable<Lazy<HtmlSupportBase, IHtmlSupportMetaDataView>> _supports { get; set; }

		/// <summary>
		/// 題型指定獲取HTML支援類實例
		/// </summary>
		/// <param name="preview">題型類型</param>
		/// <returns>HTML支援類實例</returns>
		public IHtmlSupport CreateHtmlSupportInstance(LayoutSetting.Preview preview)
		{
			// HTML支援類對象緩存區管理
			ConcurrentDictionary<LayoutSetting.Preview, IHtmlSupport> cache = HtmlSupportCache.GetOrAdd(_composer, _ => new ConcurrentDictionary<LayoutSetting.Preview, IHtmlSupport>());
			// 返回緩衝區中的支援類對象
			return cache.GetOrAdd(preview, (o) =>
			{
				// 在MEF容器中收集本類的屬性信息（實際情況屬性只注入一次）
				ComposeThis();

				// 取得指定類型下的支援類類型參數
				HtmlSupportBase support = _supports.Where(d => d.Metadata.Layout == preview).First().Value;

				log.Debug(MessageUtil.GetException(() => MsgResources.I0008L));

				// 返回該運算符處理類型的實例
				return (IHtmlSupport)Activator.CreateInstance(support.GetType());
			});
		}
	}
}
