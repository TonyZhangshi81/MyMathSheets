using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
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
	[Export(typeof(IHtmlSupportFactory)), PartCreationPolicy(CreationPolicy.Shared)]
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
		private static readonly ConcurrentDictionary<Composer, ConcurrentDictionary<string, IHtmlSupport>> HtmlSupportCache = new ConcurrentDictionary<Composer, ConcurrentDictionary<string, IHtmlSupport>>();

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
		/// <param name="topicIdentifier">題型類型</param>
		/// <returns>HTML支援類實例</returns>
		public IHtmlSupport CreateHtmlSupportInstance(string topicIdentifier)
		{
			// 獲取HTML支援類Composer
			_composer = ComposerFactory.GetComporser(topicIdentifier);

			// HTML支援類對象緩存區管理
			ConcurrentDictionary<string, IHtmlSupport> cache = HtmlSupportCache.GetOrAdd(_composer, _ => new ConcurrentDictionary<string, IHtmlSupport>());
			// HTML支援模塊是否已經注入 <- 初次注入允許MEF容器注入本類的屬性信息（注入HTML支援類屬性）
			_composed = cache.ContainsKey(topicIdentifier);
			// 返回緩衝區中的支援類對象
			IHtmlSupport instance = cache.GetOrAdd(topicIdentifier, (o) =>
			{
				// 在MEF容器中收集本類的屬性信息（實際情況屬性只注入一次）
				ComposeThis();

				// 取得指定類型下的支援類類型參數
				IEnumerable<Lazy<HtmlSupportBase, IHtmlSupportMetaDataView>> supports = Supports.Where(d => d.Metadata.Layout.Equals(topicIdentifier, StringComparison.CurrentCultureIgnoreCase));
				if (!supports.Any())
				{
					throw new HtmlSupportNotFoundException(MessageUtil.GetMessage(() => MsgResources.E0021L, topicIdentifier));
				}

				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0008L));

				return supports.First().Value;
			});

			// 返回該運算符處理類型的實例
			return instance;
		}
	}
}