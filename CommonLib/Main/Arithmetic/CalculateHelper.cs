using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Util;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 用於運算符實例取得的HEPLER類
	/// </summary>
	public class CalculateHelper
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
		///
		/// </summary>
		private void ComposeThis()
		{
			// Helper 實例后只需要收集一次
			if (this._composed)
			{
				return;
			}
			// 從MEF容器中注入本類的屬性信息（注入運算符工廠屬性）
			_composer.Compose(this);
			// 以防止重複注入（減少損耗）
			_composed = true;
		}

		/// <summary>
		/// 運算符工廠注入點
		/// </summary>
		[Import(typeof(ICalculateFactory))]
		private ICalculateFactory CalculateFactory
		{
			get;
			set;
		}

		/// <summary>
		/// 實例化
		/// </summary>
		public CalculateHelper()
		{
			// 獲取共通處理模塊Composer
			_composer = ComposerFactory.GetComporser(SystemModel.Common);
		}

		/// <summary>
		/// 對指定運算符實例化
		/// </summary>
		/// <param name="sign">運算符</param>
		/// <returns>運算符實例</returns>
		public ICalculate CreateCalculateInstance(SignOfOperation sign)
		{
			// 本類中的屬性注入執行
			ComposeThis();
			// 運算符工廠實例化
			return CalculateFactory.CreateCalculateInstance(sign);
		}
	}
}