using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Util;
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
			// Helper實例后只需要收集一次
			if (this._composed)
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
			_composer = ComposerFactory.GetComporser(SystemModel.Common);
		}

		/// <summary>
		/// 對指定計算式策略實例化
		/// </summary>
		/// <param name="preview">策略種類</param>
		/// <returns>策略實例</returns>
		public IOperation CreateOperationInstance(LayoutSetting.Preview preview)
		{
			// 本類中的屬性注入執行
			ComposeThis();

			// 計算式策略工廠實例化
			return OperationFactory.CreateOperationInstance(preview);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifiers"></param>
		/// <returns></returns>
		public ParameterBase CreateParameterInstance(string identifier)
		{
			return OperationFactory.CreateParameterInstance(identifier);
		}
	}
}
