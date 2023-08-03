using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace MyMathSheets.CommonLib.Configurations
{
	/// <summary>
	/// 自定義Section對象類型
	/// </summary>
	internal class PluginManageProviderConfigurationSection : ConfigurationSection
	{
		private const string ImportTypeProperty = "importType";

		private const string SearchKeywordProperty = "searchKeyword";

		private const string ExcludeAssembliesProperty = "excludeAssemblies";

		/// <summary>
		/// Property 類型定義
		/// </summary>
		[ConfigurationProperty(ImportTypeProperty, IsRequired = true)]
		internal string ImportType => this["importType"] as string;

		/// <summary>
		/// 檢索關鍵字
		/// </summary>
		[ConfigurationProperty(SearchKeywordProperty, IsRequired = true)]
		internal string SearchKeyword => this["searchKeyword"] as string;

		/// <summary>
		/// 檢索關鍵字
		/// </summary>
		[TypeConverter(typeof(PluginSearchExcludeAssembliyMapsConverter))]
		[ConfigurationProperty(ExcludeAssembliesProperty, IsRequired = true)]
		internal List<string> ExcludeAssemblies => this["excludeAssemblies"] as List<string>;
	}
}