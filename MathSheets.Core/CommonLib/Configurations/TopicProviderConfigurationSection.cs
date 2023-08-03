using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace MyMathSheets.CommonLib.Configurations
{
	/// <summary>
	/// 自定義Section對象類型
	/// </summary>
	internal class TopicProviderConfigurationSection : ConfigurationSection
	{
		private const string ImportTypeProperty = "importType";

		private const string ArgumentProperty = "argument";

		private const string ReplenishProperty = "replenish";

		/// <summary>
		/// Property 類型定義
		/// </summary>
		[ConfigurationProperty(ImportTypeProperty, IsRequired = true)]
		internal string ImportType => this["importType"] as string;

		/// <summary>
		/// 指定Property類型下所需要的參數（可以省略）
		/// </summary>
		[ConfigurationProperty(ArgumentProperty, IsRequired = false)]
		internal string Argument => this["argument"] as string;

		/// <summary>
		/// 配置參數補充
		/// </summary>
		[TypeConverter(typeof(ReplenishArgumentsMapsConverter))]
		[ConfigurationProperty(ReplenishProperty, IsRequired = false)]
		internal Dictionary<string, Dictionary<string, string>> ReplenishArgument => this["replenish"] as Dictionary<string, Dictionary<string, string>>;
	}
}