using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
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
		/// <summary>
		/// 以防止重複注入（減少損耗）
		/// </summary>
		private bool _composed = false;

		/// <summary>
		/// HTML支援類檢索用的composer
		/// </summary>
		private Composer _composer;

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
		}

		/// <summary>
		/// 在MEF容器中收集本類的屬性信息
		/// </summary>
		private void ComposeThis()
		{
			// 工廠實例后只需要收集一次
			if (_composed)
			{
				return;
			}
			// 從MEF容器中注入本類的屬性信息（注入HTML支援類屬性）
			_composer.Compose(this);
			// 以防止重複注入（減少損耗）
			_composed = true;
		}

		/// <summary>
		/// HTML支援類屬性注入點
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public IEnumerable<Lazy<HtmlSupportBase, IHtmlSupportMetaDataView>> Supports { get; set; }

		/// <summary>
		/// 題型指定獲取HTML支援類實例
		/// </summary>
		/// <param name="preview">題型類型</param>
		/// <returns>HTML支援類實例</returns>
		public IHtmlSupport CreateHtmlSupportInstance(LayoutSetting.Preview preview)
		{
			// 獲取HTML支援類Composer
			_composer = ComposerFactory.GetComporser(SystemModel.TheFormulaShows, preview);

			// HTML支援類對象緩存區管理
			ConcurrentDictionary<LayoutSetting.Preview, IHtmlSupport> cache = HtmlSupportCache.GetOrAdd(_composer, _ => new ConcurrentDictionary<LayoutSetting.Preview, IHtmlSupport>());
			// HTML支援模塊是否已經注入 <- 初次注入允許MEF容器注入本類的屬性信息（注入HTML支援類屬性）
			_composed = cache.ContainsKey(preview);
			// 返回緩衝區中的支援類對象
			IHtmlSupport instance = cache.GetOrAdd(preview, (o) =>
			{
				// 在MEF容器中收集本類的屬性信息（實際情況屬性只注入一次）
				ComposeThis();

				// 取得指定類型下的支援類類型參數
				IEnumerable<Lazy<HtmlSupportBase, IHtmlSupportMetaDataView>> supports = Supports.Where(d => d.Metadata.Layout == preview);
				if (!supports.Any())
				{
					throw new HtmlSupportNotFoundException(MessageUtil.GetException(() => MsgResources.E0021L, preview.ToString()));
				}

				LogUtil.LogDebug(MessageUtil.GetException(() => MsgResources.I0008L));

				return supports.First().Value;
			});

			// 返回該運算符處理類型的實例
			return instance;
		}
	}
}