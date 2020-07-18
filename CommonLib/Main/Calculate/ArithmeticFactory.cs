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

namespace MyMathSheets.CommonLib.Main.Calculate
{
	/// <summary>
	/// 運算符對象生產工廠
	/// </summary>
	[Export(typeof(IArithmeticFactory)), PartCreationPolicy(CreationPolicy.Shared)]
	public class ArithmeticFactory : IArithmeticFactory
	{
		/// <summary>
		/// 運算符檢索用的composer
		/// </summary>
		private readonly Composer _composer;

		/// <summary>
		/// 運算符處理類型緩存區
		/// </summary>
		private static readonly ConcurrentDictionary<SignOfOperation, Lazy<ArithmeticBase, IArithmeticMetaDataView>> ArithmeticCache
												= new ConcurrentDictionary<SignOfOperation, Lazy<ArithmeticBase, IArithmeticMetaDataView>>();

		/// <summary>
		/// 構造函數
		/// </summary>
		/// <remarks>
		/// 使用<see cref="ComposerAttribute"/>依賴導入計算式策略模塊並取得相應的<see cref="Composer"/>對象
		/// </remarks>
		[ImportingConstructor]
		public ArithmeticFactory()
		{
			// 獲取計算式策略模塊Composer
			_composer = ComposerFactory.GetComporser(GetType().Assembly);
		}

		/// <summary>
		/// 運算符屬性注入點
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public IEnumerable<Lazy<ArithmeticBase, IArithmeticMetaDataView>> Arithmetics { get; set; }

		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		public IArithmetic GetFormulaOperator(SignOfOperation sign)
		{
			// 返回緩衝區中的運算符對象
			Lazy<ArithmeticBase, IArithmeticMetaDataView> lazyArithmetic = ArithmeticCache.GetOrAdd(sign, (c) =>
			 {
				 // 從MEF容器中注入本類的屬性信息（注入運算符屬性）
				 _composer.Compose(this);

				 LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0001L));

				 // 指定運算符并獲取處理類型
				 IEnumerable<Lazy<ArithmeticBase, IArithmeticMetaDataView>> arithmetics = Arithmetics.Where(d => { return d.Metadata.Sign == sign; });
				 if (!arithmetics.Any())
				 {
					 // 指定的題型參數對象未找到
					 throw new ArithmeticNotFoundException(MessageUtil.GetMessage(() => MsgResources.E0020L, sign.ToString()));
				 }
				 LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0002L, sign.ToString()));

				 return arithmetics.First();
			 });

			ArithmeticBase arithmetic = lazyArithmetic.Value;
			// 內部部件組合
			_composer.Compose(arithmetic);
			// 返回該運算符處理類型的實例
			return arithmetic;
		}
	}
}