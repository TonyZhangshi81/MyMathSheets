using MyMathSheets.CommonLib.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MyMathSheets.TestConsoleApp.Write.Main
{
	/// <summary>
	/// 各計算式輸出類的實例工廠
	/// </summary>
	public class FormulasConsolerFactory
	{
		/// <summary>
		/// 運算符屬性注入點
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public IEnumerable<Lazy<IConsoleWrite, ITogicWriteMetaDataView>> Writes { get; set; }

		/// <summary>
		/// 定義私有構造函數，使外界不能創建該類實例
		/// </summary>
		public FormulasConsolerFactory()
		{
			// 內部部件組合
			ComposerFactory.GetComporser(this.GetType().Assembly).Compose(this);
		}

		/// <summary>
		/// spring 對象工廠實例作成
		/// </summary>
		/// <param name="topicIdentifier">設定題型</param>
		public IConsoleWrite CreateConsoleWriter(string topicIdentifier)
		{
			var write = Writes.Where(d =>
			{
				return d.Metadata.TopicIdentifier.Equals(topicIdentifier, StringComparison.CurrentCultureIgnoreCase);
			}).First();

			return write.Value;
		}
	}
}