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
		/// HTML支援類檢索用的composer
		/// </summary>
		private Composer _composer;

		/// <summary>
		/// HTML支援類對象緩存區
		/// </summary>
		private static readonly ConcurrentDictionary<Composer, Lazy<IHtmlSupport, IHtmlSupportMetaDataView>> HtmlSupportCache
												= new ConcurrentDictionary<Composer, Lazy<IHtmlSupport, IHtmlSupportMetaDataView>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		[ImportingConstructor]
		public HtmlSupportFactory()
		{
		}

		/// <summary>
		/// HTML支援類屬性注入點
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public IEnumerable<Lazy<IHtmlSupport, IHtmlSupportMetaDataView>> Supports { get; set; }

		/// <summary>
		/// 題型指定獲取HTML支援類實例
		/// </summary>
		/// <param name="topicIdentifier">題型類型</param>
		/// <returns>HTML支援類實例</returns>
		public IHtmlSupport CreateHtmlSupportInstance(string topicIdentifier)
		{
			// 獲取HTML支援類Composer
			_composer = ComposerFactory.GetComporser(topicIdentifier);

			// 返回緩衝區中的支援類對象
			Lazy<IHtmlSupport, IHtmlSupportMetaDataView> lazyHtmlSupport = HtmlSupportCache.GetOrAdd(_composer, (o) =>
			{
				// 從MEF容器中注入本類的屬性信息（注入HTML支援類屬性）
				_composer.Compose(this);

				// 取得指定類型下的支援類類型參數
				IEnumerable<Lazy<IHtmlSupport, IHtmlSupportMetaDataView>> supports = Supports.Where(d => d.Metadata.TopicIdentifier.Equals(topicIdentifier, StringComparison.CurrentCultureIgnoreCase));
				if (!supports.Any())
				{
					throw new HtmlSupportNotFoundException(MessageUtil.GetMessage(() => MsgResources.E0021L, topicIdentifier));
				}

				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0008L));

				return supports.First();
			});

			// 該題型HTML支持類實例（實例化）
			var htmlSupport = lazyHtmlSupport.Value;
			// 內部部件組合
			_composer.Compose(htmlSupport);
			// 返回該題型HTML支持類實例
			return htmlSupport;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="topicIdentifier"></param>
		public void ReleaseExportsHtmlSupport(string topicIdentifier)
		{
			// 獲取HTML支援類Composer
			_composer = ComposerFactory.GetComporser(topicIdentifier);

			HtmlSupportCache.TryRemove(_composer, out Lazy<IHtmlSupport, IHtmlSupportMetaDataView> support);
			_composer.ReleaseExport(support);
		}
	}
}