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

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 運算符對象生產工廠
	/// </summary>
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export(typeof(IOperationFactory))]
	public class OperationFactory : IOperationFactory
	{
		private static Log log = Log.LogReady(typeof(OperationFactory));

		/// <summary>
		/// 以防止重複注入（減少損耗）
		/// </summary>
		private bool _composed = false;
		/// <summary>
		/// 計算式參數
		/// </summary>
		private ParameterBase _operationParameter;
		/// <summary>
		/// 當前計算式策略名
		/// </summary>
		private LayoutSetting.Preview _currentPreview;

		/// <summary>
		/// 運算符檢索用的composer
		/// </summary>
		private Composer _composer;

		/// <summary>
		/// 運算符處理類型緩存區
		/// </summary>
		private static readonly ConcurrentDictionary<Composer, ConcurrentDictionary<LayoutSetting.Preview, IOperation>> OperationCache = new ConcurrentDictionary<Composer, ConcurrentDictionary<LayoutSetting.Preview, IOperation>>();
		/// <summary>
		/// 運算符參數對象類型緩存區
		/// </summary>
		private static readonly ConcurrentDictionary<LayoutSetting.Preview, ConcurrentDictionary<string, ParameterBase>> ParameterCache = new ConcurrentDictionary<LayoutSetting.Preview, ConcurrentDictionary<string, ParameterBase>>();

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
		public IEnumerable<Lazy<OperationBase, IOperationMetaDataView>> _operations { get; set; }

		/// <summary>
		/// 運算符參數屬性注入點
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public IEnumerable<Lazy<ParameterBase, IOperationMetaDataView>> _parameters { get; set; }

		/// <summary>
		/// 對指定計算式策略實例化
		/// </summary>
		/// <param name="preview">策略種類</param>
		/// <param name="identifier">計算式參數識別ID</param>
		/// <returns>策略實例</returns>
		public IOperation CreateOperationInstance(LayoutSetting.Preview preview)
		{
			// 獲取計算式策略模塊Compsero
			_composer = ComposerFactory.GetComporser(SystemModel.ComputationalStrategy, preview);

			_currentPreview = preview;
			// 運算符對象緩存區管理
			ConcurrentDictionary<LayoutSetting.Preview, IOperation> cache = OperationCache.GetOrAdd(_composer, _ => new ConcurrentDictionary<LayoutSetting.Preview, IOperation>());
			// 題型模塊是否已經注入
			if (cache.ContainsKey(preview))
			{
				// 初次注入允許MEF容器注入本類的屬性信息（注入運算符屬性）
				_composed = false;
			}
			// 返回緩衝區中的運算符對象
			return cache.GetOrAdd(preview, (o) =>
			{
				// 在MEF容器中收集本類的屬性信息（實際情況屬性只注入一次）
				ComposeThis();

				// 指定運算符并獲取處理類型
				OperationBase operation = _operations.Where(d => d.Metadata.Layout == preview).First().Value;

				log.Debug(MessageUtil.GetException(() => MsgResources.I0003L));

				// 返回該運算符處理類型的實例
				return (IOperation)Activator.CreateInstance(operation.GetType());
			});
		}

		/// <summary>
		/// 對指定計算式策略所需參數的對象實例化
		/// </summary>
		/// <param name="preview"></param>
		/// <param name="identifier">參數識別ID</param>
		/// <returns>對象實例</returns>
		public ParameterBase CreateOperationParameterInstance(LayoutSetting.Preview preview, string identifier)
		{
			// 運算符參數對象緩存區管理
			ConcurrentDictionary<string, ParameterBase> cache = ParameterCache.GetOrAdd(preview, _ => new ConcurrentDictionary<string, ParameterBase>());

			string key = string.Format(preview.ToString() + "::" + identifier);
			// 返回緩衝區中的運算符參數對象
			_operationParameter = cache.GetOrAdd(key, (o) =>
			{
				// 以運算符處理類型和參數識別ID取得相應的參數對象實例
				ParameterBase parameter = _parameters.Where(d => d.Metadata.Layout == preview && d.Metadata.Identifiers.IndexOf(identifier) >= 0).First().Value;

				log.Debug(MessageUtil.GetException(() => MsgResources.I0004L));

				// 返回該運算符處理類型的實例
				return (ParameterBase)Activator.CreateInstance(parameter.GetType());
			});

			_operationParameter.Identifier = identifier;
			// 參數初期化處理（依據Provider配置）
			_operationParameter.InitParameter();

			log.Debug(MessageUtil.GetException(() => MsgResources.I0005L, key));

			return _operationParameter;
		}
	}
}
