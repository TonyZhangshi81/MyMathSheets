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

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 運算符對象生產工廠
	/// </summary>
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export(typeof(ICalculateFactory))]
	public class CalculateFactory : ICalculateFactory
	{
		/// <summary>
		/// 以防止重複注入（減少損耗）
		/// </summary>
		private bool _composed = false;

		/// <summary>
		/// 運算符檢索用的composer
		/// </summary>
		private readonly Composer _composer;

		/// <summary>
		/// 運算符處理類型緩存區
		/// </summary>
		private static readonly ConcurrentDictionary<Composer, ConcurrentDictionary<SignOfOperation, ICalculate>> ComposerCache = new ConcurrentDictionary<Composer, ConcurrentDictionary<SignOfOperation, ICalculate>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <remarks>
		/// 使用<see cref="ComposerAttribute"/>依賴導入計算式策略模塊並取得相應的<see cref="Composer"/>對象
		/// </remarks>
		[ImportingConstructor]
		public CalculateFactory()
		{
			// 獲取計算式策略模塊Composer
			_composer = ComposerFactory.GetComporser(GetType().Assembly);
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
			// 從MEF容器中注入本類的屬性信息（注入運算符屬性）
			_composer.Compose(this);
			// 以防止重複注入（減少損耗）
			_composed = true;
		}

		/// <summary>
		/// 運算符屬性注入點
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public IEnumerable<Lazy<CalculateBase, ICalculateMetaDataView>> Calculates { get; set; }

		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		public ICalculate CreateCalculateInstance(SignOfOperation sign)
		{
			// 運算符對象緩存區管理
			ConcurrentDictionary<SignOfOperation, ICalculate> cacheStrategy = ComposerCache.GetOrAdd(_composer, _ => new ConcurrentDictionary<SignOfOperation, ICalculate>());
			// 返回緩衝區中的運算符對象
			ICalculate calculater = cacheStrategy.GetOrAdd(sign, (c) =>
			{
				// 在MEF容器中收集本類的屬性信息（實際情況屬性只注入一次）
				ComposeThis();

				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0001L));

				// 指定運算符并獲取處理類型
				IEnumerable<Lazy<CalculateBase, ICalculateMetaDataView>> calculates = Calculates.Where(d => { return d.Metadata.Sign == sign; });
				if (!calculates.Any())
				{
					// 指定的題型參數對象未找到
					throw new CalculateNotFoundException(MessageUtil.GetMessage(() => MsgResources.E0020L, sign.ToString()));
				}
				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0002L, sign.ToString()));

				return calculates.First().Value;
			});

			// 返回該運算符處理類型的實例
			return calculater;
		}
	}
}