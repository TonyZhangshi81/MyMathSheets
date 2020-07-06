using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Util;
using System.ComponentModel.Composition;

namespace MyMathSheets.CommonLib.Main.Arithmetic
{
	/// <summary>
	/// 用於HTML支援類實例取得的HEPLER類
	/// </summary>
	public class HtmlSupprtHelper
	{
		/// <summary>
		/// 以防止重複注入（減少損耗）
		/// </summary>
		private bool _composed = false;

		/// <summary>
		/// HTML支援類檢索用的composer
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
			// 從MEF容器中注入本類的屬性信息（注入工廠屬性）
			_composer.Compose(this);
			// 以防止重複注入（減少損耗）
			_composed = true;
		}

		/// <summary>
		/// 工廠注入點
		/// </summary>
		[Import(typeof(IHtmlSupportFactory))]
		private IHtmlSupportFactory HtmlSupportFactory
		{
			get;
			set;
		}

		/// <summary>
		/// 實例化
		/// </summary>
		public HtmlSupprtHelper()
		{
			// 獲取共通處理模塊Composer
			_composer = ComposerFactory.GetComporser(SystemModelType.Common);
		}

		/// <summary>
		/// 對指定HTML支援類實例化
		/// </summary>
		/// <param name="preview">題型種類</param>
		/// <returns>運算符實例</returns>
		public IHtmlSupport CreateHtmlSupportInstance(string preview)
		{
			// 本類中的屬性注入執行
			ComposeThis();
			// 運算符工廠實例化
			return HtmlSupportFactory.CreateHtmlSupportInstance(preview);
		}
	}
}