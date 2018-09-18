using System.Configuration;

namespace MyMathSheets.CommonLib.Provider
{
	/// <summary>
	/// 自定義Section對象類型
	/// </summary>
	public class OperationProviderConfigurationSection : ConfigurationSection
	{
		/// <summary>
		/// 只讀設定
		/// </summary>
		/// <returns></returns>
		public override bool IsReadOnly()
		{
			return true;
		}

		/// <summary>
		/// Property類型定義
		/// </summary>
		[ConfigurationProperty("type", IsRequired = true)]
		public string ProviderType => this["type"] as string;

		/// <summary>
		/// 指定Property類型下所需要的參數（可以省略）
		/// </summary>
		[ConfigurationProperty("argument", IsRequired = false)]
		public string Argument
		{
			get { return this["argument"] as string; }
		}
	}
}
