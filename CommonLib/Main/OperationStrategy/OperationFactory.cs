using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 運算符對象生產工廠
	/// </summary>
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export(typeof(IOperationFactory))]
	public class OperationFactory : IOperationFactory
	{
		/// <summary>
		/// 以防止重複注入（減少損耗）
		/// </summary>
		private bool _composed = false;

		/// <summary>
		/// 運算符檢索用的composer
		/// </summary>
		private Composer _composer;

		/// <summary>
		/// 類型對象緩存
		/// </summary>
		private static readonly ConcurrentDictionary<Composer, ConcurrentDictionary<string, Type>> OperationCache = new ConcurrentDictionary<Composer, ConcurrentDictionary<string, Type>>();

		/// <summary>
		/// 類型參數對象緩存
		/// </summary>
		private static readonly ConcurrentDictionary<string, Type> ParameterCache = new ConcurrentDictionary<string, Type>();

		/// <summary>
		/// 構造函數
		/// </summary>
		[ImportingConstructor]
		public OperationFactory()
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
			// 從MEF容器中注入本類的屬性信息（注入運算符屬性）
			_composer.Compose(this);
			// 以防止重複注入（減少損耗）
			_composed = true;
		}

		/// <summary>
		/// 運算符屬性注入點
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public IEnumerable<Lazy<OperationBase, IOperationMetaDataView>> Operations { get; set; }

		/// <summary>
		/// 對指定計算式策略實例化
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>策略實例</returns>
		public virtual IOperation CreateOperationInstance(string topicIdentifier)
		{
			// 獲取計算式策略模塊Compsero
			_composer = ComposerFactory.GetComporser(topicIdentifier);

			// 運算符對象緩存區管理
			ConcurrentDictionary<string, Type> cache = OperationCache.GetOrAdd(_composer, _ => new ConcurrentDictionary<string, Type>());
			// 題型模塊是否已經注入 <- 初次注入允許MEF容器注入本類的屬性信息（注入運算符屬性）
			_composed = cache.ContainsKey(topicIdentifier);
			// 返回緩衝區中的運算符對象
			Type type = cache.GetOrAdd(topicIdentifier, (o) =>
			{
				// 在MEF容器中收集本類的屬性信息（實際情況屬性只注入一次）
				ComposeThis();

				// 指定運算符并獲取處理類型
				IEnumerable<Lazy<OperationBase, IOperationMetaDataView>> operations = Operations.Where(d => d.Metadata.Layout.Equals(topicIdentifier, StringComparison.CurrentCultureIgnoreCase));
				if (!operations.Any())
				{
					// 指定的題型策略對象未找到
					throw new OperationNotFoundException(MessageUtil.GetMessage(() => MsgResources.E0018L, topicIdentifier));
				}

				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0003L));

				// 運算符處理類型返回
				return operations.First().Value.GetType();
			});

			// 返回該運算符處理類型的實例
			var instance = (IOperation)Activator.CreateInstance(type);
			_composer.Compose(instance);
			return instance;
		}

		/// <summary>
		/// 對指定計算式策略所需參數的對象實例化
		/// </summary>
		/// <param name="topicIdentifier">題型種類</param>
		/// <param name="topicNumber">參數識別ID</param>
		/// <returns>對象實例</returns>
		public virtual ParameterBase CreateOperationParameterInstance(string topicIdentifier, string topicNumber)
		{
			// 參數對象緩存區管理
			string key = $"{topicIdentifier}::{topicNumber}";

			Type type = ParameterCache.GetOrAdd(key, (o) =>
			{
				// 注入運算符參數對象
				IEnumerable<Lazy<ParameterBase, IOperationMetaDataView>> parameters = _composer.GetExports<ParameterBase, IOperationMetaDataView>();
				if (!parameters.Any())
				{
					// 指定的題型參數對象未找到
					throw new OperationNotFoundException(MessageUtil.GetMessage(() => MsgResources.E0019L, topicIdentifier));
				}
				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0004L));

				return parameters.First().Value.GetType();
			});

			var parameter = (ParameterBase)Activator.CreateInstance(type);
			// 基礎參數初期化處理（依據Provider配置）
			parameter.InitParameterBase(key);
			// 派生類參數初期化（各子類實現）
			parameter.InitParameter();

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0005L, key));

			return parameter;
		}
	}
}