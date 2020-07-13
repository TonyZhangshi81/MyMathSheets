using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.OperationStrategy
{
	/// <summary>
	/// 用於計算式策略實例取得的HEPLER類
	/// </summary>
	public class OperationHelper
	{
		/// <summary>
		/// 以防止重複注入（減少損耗）
		/// </summary>
		private bool _composed = false;

		/// <summary>
		/// 計算式策略檢索用的composer
		/// </summary>
		private readonly Composer _composer;

		/// <summary>
		///
		/// </summary>
		private void ComposeThis()
		{
			// Helper 實例后只需要收集一次
			if (_composed)
			{
				return;
			}
			// 從MEF容器中注入本類的屬性信息（注入計算式策略工廠屬性）
			_composer.Compose(this);
			// 以防止重複注入（減少損耗）
			_composed = true;
		}

		/// <summary>
		/// 計算式工廠注入點
		/// </summary>
		[Import(typeof(IOperationFactory))]
		public IOperationFactory OperationFactory
		{
			get;
			set;
		}

		/// <summary>
		/// 實例化
		/// </summary>
		public OperationHelper()
		{
			// 獲取共通處理模塊Composer
			_composer = ComposerFactory.GetComporser(this.GetType().Assembly);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <param name="topicNumber">參數編號（如果沒有指定參數編號，則默認返回當前參數序列的第一個參數項目）</param>
		/// <returns></returns>
		public ParameterBase Structure(string topicIdentifier, string topicNumber = "")
		{
			// 題型實例
			IOperation instance = CreateOperationInstance(topicIdentifier);
			// 計算式所需參數
			ParameterBase parameter = OperationFactory.CreateOperationParameterInstance(topicIdentifier, topicNumber);

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0006L));

			// 根據參數構建題型
			instance.Build(parameter);

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0007L));

			return parameter;
		}

		/// <summary>
		/// 對指定題型實例化並返回
		/// </summary>
		/// <param name="topicIdentifier">題型識別ID</param>
		/// <returns>題型實例</returns>
		private IOperation CreateOperationInstance(string topicIdentifier)
		{
			// 本類中的屬性注入執行
			ComposeThis();

			// 計算式策略工廠實例化
			return OperationFactory.CreateOperationInstance(topicIdentifier);
		}
	}
}